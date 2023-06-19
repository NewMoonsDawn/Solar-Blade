using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitResolution : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name=="Player")
        {
            playerMovement.takeDamage(25f);
        }
    }
}
