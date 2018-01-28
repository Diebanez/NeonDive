﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestoryerOnTime : MonoBehaviour {


    private float timer = 5.0f;
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this);
        }
	}
}
