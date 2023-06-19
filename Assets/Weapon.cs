using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public GameObject thrownBlade;
    public Transform firePoint;


    public float weaponDamage = 0.6f;
    [SerializeField]
    public float fireForce = 10f;
    [SerializeField]
    public bool isHeld = true;
    [SerializeField]
    public bool isReturning = false;
    public float spinForce = 1f;
    [SerializeField]
    public float returnSpeed = 5f;
    [SerializeField]
    
    public GameObject projectile;


    private Vector2 projectileVelocity;

    public void Fire()
    {
        if (isHeld == true)
        {
            isHeld = false;
            projectile = Instantiate(thrownBlade, firePoint.position, firePoint.rotation);
            projectile.GetComponent<SpriteRenderer>().enabled = true;
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
           
        }
            }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile != null)
        {
            projectileVelocity = projectile.GetComponent<Rigidbody2D>().velocity;
            projectile.GetComponent<Rigidbody2D>().rotation += Mathf.Clamp(spinForce * projectileVelocity.magnitude,0,33);
            if(isReturning== false)
            projectile.GetComponent<Rigidbody2D>().velocity = projectileVelocity * 0.995f;

            if (projectileVelocity.magnitude <= 1f)
            {
                isReturning= true;
            }

            if(isReturning == true)
            {
                projectile.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(projectile.GetComponent<Rigidbody2D>().position, player.position, returnSpeed * Time.deltaTime);
                if(Vector2.Distance(projectile.GetComponent<Rigidbody2D>().position,player.position)<2f)
                {
                    Destroy(projectile);
                    isHeld = true;
                    isReturning = false;
                }
            }
        }
        if(isHeld==false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        if(isHeld == true)
        {
            GetComponent<Renderer>().enabled = true;
        }
    }
}
