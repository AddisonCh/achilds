using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    public int health;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.transform.parent.tag)
        {
            case "Projectile":
                int hurt = other.gameObject.GetComponentInParent<Projectile>().damage;
                Destroy(other.gameObject.transform.parent.gameObject);
                TakeDamage(hurt);
                 break;
    }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
