using System;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class DiveController : MonoBehaviour {
    public float LockDiveTime = 1.0f;
    private EntityController Controller;
    private MovementController MoveController;
    private bool WasPlayer = false;
    private GameObject actualSelected;
    private LineRenderer AimRenderer;
    private bool IsLoadingDive = false;
    private float DiveCooldown = 5.0f;
    private float cooldownTimer = 0.0f;
    private float timer = 0.0f;
    private bool CanDive = true;
    public GameObject DiveRenderer;

    void Start () {
        Controller = GetComponent<EntityController>();
        MoveController = GetComponent<MovementController>();
        AimRenderer = GetComponent<LineRenderer>();
        if (Controller.IsPlayer)
        {
            InputHandler.instance.Dive += OnDive;
            InputHandler.instance.BaseShoot += OnBaseShoot;
            WasPlayer = true;
        }
    }

    private void Update()
    {
        if (!GameManager.instance.IsPaused)
        {
            if (!WasPlayer)
            {
                if (Controller.IsPlayer)
                {
                    InputHandler.instance.Dive += OnDive;
                    InputHandler.instance.BaseShoot += OnBaseShoot;
                    WasPlayer = true;
                }
            }
        }

        if (cooldownTimer <= 0)
        {
            CanDive = true;
            DiveRenderer.SetActive(true);
        }
        else
        {
            CanDive = false;
            DiveRenderer.SetActive(false);
            cooldownTimer -= Time.deltaTime;
        }

        if (Controller.IsPlayer)
        { 
            AimRenderer.SetPosition(0, transform.position);
            if (IsLoadingDive)
            {                
                if(timer <= 0)
                {
                    GameManager.instance.StartQteEvent(this.gameObject, actualSelected);
                    CanDive = false;
                    IsLoadingDive = false;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
            if (actualSelected != null)
            {
                AimRenderer.SetPosition(1, actualSelected.transform.position);
            }
            else
            {
                AimRenderer.SetPosition(1, transform.position);
                IsLoadingDive = false;
            }
        }
    }

    public void DestroyPlayer()
    {
        InputHandler.instance.Dive -= OnDive;
        InputHandler.instance.BaseShoot -= OnBaseShoot;
    }

    public void StarDiveCooldown()
    {
        CanDive = false;
        cooldownTimer = DiveCooldown;
    }

    private void OnDive(object source, EventArgs e)
    {
        if (CanDive)
        {
            if (!IsLoadingDive)
            {
                
                AimRenderer.SetPosition(0, transform.position);

                Vector3 direction = new Vector3(Input.GetAxis("RightHorizontal"), 0, Input.GetAxis("RightVertical"));
                RaycastHit2D hit = Physics2D.Raycast(transform.position + direction, new Vector2(direction.x, direction.z), 100);
                if (hit.collider != null)
                {
                    if (hit.transform.gameObject.tag == "Entity")
                    {
                        //GameManager.instance.StartQteEvent(this.gameObject, hit.transform.gameObject);
                        AimRenderer.SetPosition(1, hit.transform.position);
                        timer = LockDiveTime;
                        actualSelected = hit.transform.gameObject;
                        IsLoadingDive = true;


                    }
                }
                AimRenderer.enabled = true;
            }
        }

    }

    void OnBaseShoot(object source, EventArgs e)
    {
        actualSelected = null;
        IsLoadingDive = false;
        timer = LockDiveTime;
    }

}
