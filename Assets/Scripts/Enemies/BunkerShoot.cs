using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerShoot : Shoot {

    public override void OnBaseShoot(object source, EventArgs e)
    {
        if (!hasShooted)
        {
            if (Controller.IsPlayer)
            {
                GameObject blet = Instantiate(bullet, transform.position + new Vector3(1, 0, 0), transform.rotation);
                BulletMovement bmMovement = blet.GetComponent<BulletMovement>();
                bmMovement.target = "Entity";
            }
            else
            {
                GameObject blet = Instantiate(bullet, transform.position - new Vector3(1, 0, 0), transform.rotation);
                BulletMovement bmMovement = blet.GetComponent<BulletMovement>();
                bmMovement.target = "Player";
                bmMovement.direction = GameManager.instance.ActualPlayer.transform;
            }
            hasShooted = true;
        }
    }
}
