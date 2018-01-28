using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float bulletSpeed = 20.0f;
    public int damage = 0;
    public string target;
    public Transform direction;
    private bool directionSetted = false;
    public float LifeTime = 5.0f;

    // Update is called once per frame
    protected virtual void Update () {
        if (!GameManager.instance.IsPaused)
        {
            if (target == "Player")
            {/*
                if (direction == null)
                {
                    return;
                }
                if (directionSetted == false)
                {
                    this.transform.rotation = Quaternion.LookRotation(direction.position - transform.position);
                    directionSetted = true;
                }*/
                //transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
                transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
            }
            Destroy(gameObject, LifeTime);
        }

	}

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == target)
        {
            collision.gameObject.GetComponent<EntityController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
