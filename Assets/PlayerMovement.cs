using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;

    public float moveSpeed;
    [SerializeField]
    public Rigidbody2D rb;

    public Weapon weapon;

    private Vector2 moveDirection;

    private Vector2 mousePosition;

    public float health = 100f;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
        if (Input.GetMouseButtonDown(1))
        {
            weapon.isReturning= true;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }

}
