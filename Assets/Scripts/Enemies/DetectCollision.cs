using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour {


    public int explosionDamage = 10;

    public GameObject DeathAnimation;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(DeathAnimation, transform.position, transform.rotation);
            collision.gameObject.GetComponent<EntityController>().TakeDamage(explosionDamage);
           
        }
    }
}
