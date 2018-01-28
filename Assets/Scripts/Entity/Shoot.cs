using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LineRenderer))]
public class Shoot : MonoBehaviour {

    public GameObject bullet;
    public GameObject aimObject;
    public float rayLenght = 10.0f;
    public int Damage = 10;
    public float ShootDelay = .5f;

    protected LineRenderer AimRenderer;
    protected EntityController Controller;
    protected bool WasPlayer = false;
    protected bool hasShooted = false;
    protected float timer = 0.0f;
    protected GameObject actualSelected;

    // Use this for initialization
    void Start() {
        Controller = GetComponent<EntityController>();
        if (Controller.IsPlayer)
        {
            InputHandler.instance.BaseShoot += OnBaseShoot;
            WasPlayer = true;
        }
        AimRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.instance.IsPaused)
        {
            if (!WasPlayer)
            {
                if (Controller.IsPlayer)
                {
                    InputHandler.instance.BaseShoot += OnBaseShoot;
                    WasPlayer = true;
                }
            }
        }
        if (!Controller.IsPlayer)
        {
            Quaternion rotation = Quaternion.LookRotation(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - aimObject.transform.position, aimObject.transform.TransformDirection(Vector3.up));
            aimObject.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
        else
        {
            aimObject.transform.rotation = Quaternion.identity;
        }
        if (timer <= 0)
        {
            hasShooted = false;
            timer = ShootDelay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public virtual void OnBaseShoot(object source, EventArgs e)
    {
        if (!hasShooted)
        {
            actualSelected = null;
            if (Controller.IsPlayer)
            {
                GameObject blet = Instantiate(bullet, transform.position + new Vector3(1, 0, 10), aimObject.transform.rotation);
                BulletMovement bmMovement = blet.GetComponent<BulletMovement>();
                bmMovement.damage = this.Damage;
                bmMovement.target = "Entity";
            }
            else
            {
                GameObject blet = Instantiate(bullet, transform.position, aimObject.transform.rotation);
                BulletMovement bmMovement = blet.GetComponent<BulletMovement>();
                bmMovement.damage = this.Damage;
                bmMovement.target = "Player";
                bmMovement.direction = GameManager.instance.ActualPlayer.transform;
            }
            hasShooted = true;
        }
    }
    

    public void DestroyPlayer()
    {
        InputHandler.instance.BaseShoot -= OnBaseShoot;
        Destroy(this.gameObject);
    }
}
