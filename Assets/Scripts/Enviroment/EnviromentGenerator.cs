using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentGenerator : MonoBehaviour {

    public Sprite[] sprites;
    public float SpawnDelay = 1.0f;
    private float timer = 0.0f;
    public float MovementSpeed = 2.0f;

    private void Update()
    {
        if(timer <= 0.0f)
        {
            GameObject NewImage = new GameObject();
            
            NewImage.AddComponent<ImageTraslator>();
            NewImage.GetComponent<ImageTraslator>().MovementSpeed = MovementSpeed;
            NewImage.AddComponent<SpriteRenderer>();
            NewImage.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
            NewImage.transform.position = this.transform.position + new Vector3(0, NewImage.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            //Instantiate(NewImage, transform.position, transform.rotation);
            timer = SpawnDelay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
