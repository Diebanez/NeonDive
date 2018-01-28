using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedShoot : Shoot {
    public float offset = .25f;

    public override void OnBaseShoot(object source, EventArgs e)
    {
        if (!hasShooted)
        {
            if (Controller.IsPlayer)
            {
                GameObject blet = Instantiate(bullet, transform.position + new Vector3(1, offset, 0), aimObject.transform.rotation);
                BulletMovement bmMovement = blet.GetComponent<BulletMovement>();
                bmMovement.damage = this.Damage;
                bmMovement.target = "Entity";
                GameObject blet2 = Instantiate(bullet, transform.position + new Vector3(1, -offset, 0), aimObject.transform.rotation);
                BulletMovement bmMovement2 = blet2.GetComponent<BulletMovement>();
                bmMovement2.damage = this.Damage;
                bmMovement2.target = "Entity";
            }
            else
            {
                GameObject blet = Instantiate(bullet, transform.position + new Vector3(0, offset, 0), aimObject.transform.rotation);
                BulletMovement bmMovement = blet.GetComponent<BulletMovement>();
                bmMovement.damage = this.Damage;
                bmMovement.target = "Player";
                bmMovement.direction = GameObject.Find("Player").transform;
                GameObject blet2 = Instantiate(bullet, transform.position + new Vector3(0, -offset, 0), aimObject.transform.rotation);
                BulletMovement bmMovement2 = blet2.GetComponent<BulletMovement>();
                bmMovement2.damage = this.Damage;
                bmMovement2.target = "Player";
                bmMovement2.direction = GameObject.Find("Player").transform;
            }
            hasShooted = true;
        }
    }
}
