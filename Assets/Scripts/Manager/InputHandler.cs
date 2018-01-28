using System;
using UnityEngine;

public class MovementEventArgs : EventArgs
{
    public Vector2 Direction;
}

public class AimEventArgs : EventArgs
{
    public Vector2 Direction;
}
public class InputHandler : MonoBehaviour {
    
    public bool IsPadMode = false;

    public static InputHandler instance;

    public delegate void MovementEventHandler(object source, MovementEventArgs args);
    public delegate void ShootEventHandler(object source, AimEventArgs args);
    public delegate void DiveEventHandler(object source, EventArgs args);


    public event MovementEventHandler UpMovement;
    public event MovementEventHandler DownMovement;    
    public event MovementEventHandler LeftMovement;
    public event MovementEventHandler RightMovement;
    public event ShootEventHandler BaseShoot;
    public event ShootEventHandler DrawAim;
    public event DiveEventHandler Dive;

    //Inspector Properties
    public KeyCode UpMovementKey;
    public KeyCode DownMovementKey;
    public KeyCode LeftMovementKey;
    public KeyCode RightMovementKey;
    public KeyCode BaseShootKey;
    public KeyCode DiveKey;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (!GameManager.instance.IsPaused)
        {
            if (IsPadMode)
            {
                Vector2 movement = new Vector2(Input.GetAxis("LeftHorizontal"), Input.GetAxis("LeftVertical"));
                OnMovement(movement);
                OnDrawAim(new Vector2(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical")));
                if (Input.GetAxis("Fire1") != 0)
                {
                    OnBaseShoot();
                }
            }
            else
            {
                if (Input.GetKey(UpMovementKey))
                {
                    OnUpMovement();
                }
                if (Input.GetKey(DownMovementKey))
                {
                    OnDownMovement();
                }
                if (Input.GetKey(LeftMovementKey))
                {
                    OnLeftMovement();
                }
                if (Input.GetKey(RightMovementKey))
                {
                    OnRightMovement();
                }
                if (Input.GetKeyDown(BaseShootKey))
                {
                    OnBaseShoot();
                }
                OnDrawAim(new Vector2(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical")));
            }
            OnDive();
        }
    }
    protected virtual void OnUpMovement()
    {
        if (UpMovement != null)
        {
            UpMovement(this, new MovementEventArgs { Direction = Vector2.up });
        }
    }

    protected virtual void OnDownMovement()
    {
        if (DownMovement != null)
        {
            DownMovement(this, new MovementEventArgs { Direction = -Vector2.up });
        }
    }

    protected virtual void OnLeftMovement()
    {
        if (LeftMovement != null)
        {
            LeftMovement(this, new MovementEventArgs { Direction = Vector2.left });
        }
    }


    protected virtual void OnRightMovement()
    {
        if (RightMovement != null)
        {
            RightMovement(this, new MovementEventArgs { Direction = -Vector2.left });
        }
    }

    protected virtual void OnBaseShoot()
    {
        if (BaseShoot != null)
        {
            BaseShoot(this, new AimEventArgs());
        }
    }

    protected virtual void OnDive()
    {
        if (Dive != null)
        {
            Dive(this, EventArgs.Empty);
        }
    }

    protected virtual void OnMovement(Vector2 movement)
    {
        if (UpMovement != null)
        {
            UpMovement(this, new MovementEventArgs { Direction = movement });
        }
    }

    protected virtual void OnDrawAim(Vector2 direction)
    {
        if (DrawAim != null)
        {
            DrawAim(this, new AimEventArgs { Direction = direction });
        }
    }
}
