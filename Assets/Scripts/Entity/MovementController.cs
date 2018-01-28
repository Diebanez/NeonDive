using System;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class MovementController : MonoBehaviour {

    public float MovementSpeed = 1.0f;

    private EntityController Controller;
    private bool WasPlayer = false;
    private Shoot ShootController;
    private DiveController DiController;

    void Start () {
        Controller = GetComponent<EntityController>();
        ShootController = GetComponent<Shoot>();
        DiController = GetComponent<DiveController>();
        if (Controller.IsPlayer)
        {
            InputHandler.instance.UpMovement += OnUpMovement;
            InputHandler.instance.DownMovement += OnDownMovement;
            InputHandler.instance.LeftMovement += OnLeftMovement;
            InputHandler.instance.RightMovement += OnRightMovement;
            WasPlayer = true;
        }
	}
	
	void Update () {
        if (!GameManager.instance.IsPaused)
        {
            if (!WasPlayer)
            {
                if (Controller.IsPlayer)
                {
                    InputHandler.instance.UpMovement += OnUpMovement;
                    InputHandler.instance.DownMovement += OnDownMovement;
                    InputHandler.instance.LeftMovement += OnLeftMovement;
                    InputHandler.instance.RightMovement += OnRightMovement;
                    WasPlayer = true;
                }
            }
        }
	}

    public void DestroyPlayer()
    {
        InputHandler.instance.UpMovement -= OnUpMovement;
        InputHandler.instance.DownMovement -= OnDownMovement;
        InputHandler.instance.LeftMovement -= OnLeftMovement;
        InputHandler.instance.RightMovement -= OnRightMovement;
        DiController.DestroyPlayer();
        ShootController.DestroyPlayer();
    }

    public void OnUpMovement(object source, MovementEventArgs e)
    {
        if ((transform.position.x <= -13f && e.Direction.x < 0) || (transform.position.x > 13f && e.Direction.x > 0) || (transform.position.y < -7.5f && e.Direction.y < 0) || (transform.position.y > 7.5f && e.Direction.y > 0))
        {

        }
        else
        {
            transform.Translate(e.Direction * MovementSpeed * Time.deltaTime);
        }
    }

    public void OnDownMovement(object source, MovementEventArgs e)
    {
        if ((transform.position.x <= -13f && e.Direction.x < 0) || (transform.position.x > 13f && e.Direction.x > 0) || (transform.position.y < -7.5f && e.Direction.y < 0) || (transform.position.y > 7.5f && e.Direction.y > 0))
        {

        }
        else
        {
            transform.Translate(e.Direction * MovementSpeed * Time.deltaTime);
        }
    }

    public void OnLeftMovement(object source, MovementEventArgs e)
    {
        if ((transform.position.x <= -13f && e.Direction.x < 0) || (transform.position.x > 13f && e.Direction.x > 0) || (transform.position.y < -7.5f && e.Direction.y < 0) || (transform.position.y > 7.5f && e.Direction.y > 0))
        {

        }
        else
        {
            transform.Translate(e.Direction * MovementSpeed * Time.deltaTime);
        }
    }


    public void OnRightMovement(object source, MovementEventArgs e)
    {
        if ((transform.position.x <= -13f && e.Direction.x < 0) || (transform.position.x > 13f && e.Direction.x > 0) || (transform.position.y < -7.5f && e.Direction.y < 0) || (transform.position.y > 7.5f && e.Direction.y > 0))
        {

            }
            else
            {
                transform.Translate(e.Direction * MovementSpeed * Time.deltaTime);
            }
        }
}
