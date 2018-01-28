using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerBulletMovement : BulletMovement {
    private int actualScale = 0;
    public float ExtendingSpeed = 1.0f;
    public float MaxExtensionLenght = 3.0f;
    protected override void Update()
    {
        if (!GameManager.instance.IsPaused)
        {
            if (target == "Player")
            {
                transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);              
            }
            else
            {
                transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
            }
            /*if (transform.localScale.y < 2.5f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2.0f * Time.del, transform.localScale.z);
            }*/
            //transform.Rotate(Vector3.left * -.9f);

            switch (actualScale)
            {
                case 0:
                    {
                        if (transform.localScale.y < MaxExtensionLenght)
                        {
                            transform.localScale += new Vector3(0, ExtendingSpeed, 0) * Time.deltaTime;
                        }
                        else
                        {
                            actualScale = 1;
                        }
                        break;
                    }
                case 1:
                    {
                        if (transform.localScale.y > -MaxExtensionLenght)
                        {
                            transform.localScale += new Vector3(0, -ExtendingSpeed, 0) * Time.deltaTime;
                        }
                        else
                        {
                            actualScale = 0;
                        }
                        break;
                    }
            }
            Destroy(gameObject, 20.0f);
        }
    }
}
