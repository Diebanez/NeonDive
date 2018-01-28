using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTraslator : MonoBehaviour {

    public float MovementSpeed = 2.0f;
    public float TimeToLive = 20.0f;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = TimeToLive;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            transform.Translate(Vector2.left * MovementSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
