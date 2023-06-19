using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public GameObject player;
    public Weapon weapon;
    public Animation anim;

    PlayerMovement playerMovement;

    public float moveSpeed = 8f;

    public float health = 100f;

    public float distance;

    public float maxDistance = 6f;

    public float attackDuration = 0.15f;

    public float attackDamage = 20f;

    private bool attacking = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) + Mathf.Rad2Deg;
    

        if(distance>maxDistance && attacking == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if(distance<maxDistance && attacking == false)
        { 
            StartCoroutine(Attack());
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "ThrownBlade(Clone)")
        {
            Debug.Log(weapon.projectile.GetComponent<Rigidbody2D>().velocity.magnitude * weapon.weaponDamage);
            Debug.Log(health);
            health -= weapon.projectile.GetComponent<Rigidbody2D>().velocity.magnitude * weapon.weaponDamage;
            Debug.Log(health);
        }

        
    }
    IEnumerator Attack()
    {
        attacking = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        GameObject hitbox = new GameObject("Hitbox");

        hitbox.transform.position = transform.position + (Vector3)directionToPlayer * 3.0f;
        hitbox.AddComponent<BoxCollider2D>();
        hitbox.AddComponent<HitResolution>();

        Destroy(hitbox, attackDuration);
        



        yield return new WaitForSeconds(attackDuration);

        attacking = false;
    }
}
