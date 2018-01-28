using System;
using UnityEngine;

public enum Patterns { None, Ellipse, Rectangular, Oblique}

[RequireComponent(typeof(MovementController))]
public class EnemyController : MonoBehaviour {

    public float ShootDelay = 2.0f;
    public float EnemyCenterActivePatternX = 6.0f;
    public float TopBorderPosition;
    public float BotBorderPosition;

    public Patterns ActualPattern;
    private MovementController MoveController;
    private EntityController Controller;
    private Shoot ShootController;
    private float timer = 0.0f;
    private Vector2 StartPosition;
    private int ActualEllipseState = 0;
    private int ActualRectangularState = 0;
    private int ActualObliqueState = 0;
    
	void Start () {  
        MoveController = GetComponent<MovementController>();
        Controller = GetComponent<EntityController>();
        ShootController = GetComponent<Shoot>();
        StartPosition = transform.position;
	}
	
	void Update () {
        if (!GameManager.instance.IsPaused)
        {
            if (Controller.IsPlayer)
            {
                DestroyImmediate(this);
            }
            ExecuteMovement();
            if (timer <= 0)
            {
                ShootController.OnBaseShoot(this, EventArgs.Empty);
                timer = ShootDelay;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
	}

    protected virtual void ExecuteMovement()
    {
        switch (ActualPattern){
            case Patterns.Ellipse:
                {

                    if (transform.position.x > EnemyCenterActivePatternX)
                    {
                        MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = Vector2.left });
                    }
                    else
                    {
                        switch (ActualEllipseState)
                        {
                            case 0:

                                if (transform.position.x <= StartPosition.x + 3.0f && transform.position.y <= TopBorderPosition - 0.88f)
                                {
                                    MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = new Vector2(-1, 1) });
                                }
                                else
                                {
                                    ActualEllipseState = 1;
                                }

                                break;

                            case 1:
                                if (transform.position.x <= StartPosition.x + 6.0f && transform.position.y >= 0)
                                {

                                    MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = new Vector2(-1, -1) });
                                }
                                else
                                {
                                    ActualEllipseState = 2;
                                }

                                break;
                            case 2:
                                if (transform.position.x <= StartPosition.x + 3.0f && transform.position.y >= BotBorderPosition + 0.88f)

                                {
                                    MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = new Vector2(1, -1) });
                                }
                                else
                                {
                                    ActualEllipseState = 3;
                                }

                                break;
                            case 3:
                                if (transform.position.x <= StartPosition.x && transform.position.y <= 0)
                                {
                                    MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = new Vector2(1, 1) });
                                }
                                else
                                {
                                    ActualEllipseState = 0;
                                }
                                break;

                        }

                    }
                }
                break;
            case Patterns.Rectangular:
                {
                    if(transform.position.x > EnemyCenterActivePatternX)
                    {
                        MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = Vector2.left });
                    }
                    else
                    {
                        switch (ActualRectangularState)
                        {
                            case 0:
                                {
                                    if(transform.position.y < StartPosition.y + 2.90f)
                                    {
                                        MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = Vector2.up });
                                    }
                                    else
                                    {
                                        ActualRectangularState = 1;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (transform.position.y > StartPosition.y - 2.90f)
                                    {
                                        MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = Vector2.down });
                                    }
                                    else
                                    {
                                        ActualRectangularState = 0;
                                    }
                                    break;
                                }
                        }
                    }
                    break;
                }
            case Patterns.Oblique:
                {
                    switch (ActualObliqueState)
                    {
                        case 0:
                            if (transform.position.x >= StartPosition.x - 15.0f)
                            {
                                MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = Vector2.left });
                            }
                            else
                            {
                                ActualObliqueState = 1;
                            }
                            break;
                        case 1:
                            if (transform.position.y > BotBorderPosition + 0.88f)
                            {
                                MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = new Vector2(1, -1) });

                            }
                            else
                            {
                                ActualObliqueState = 2;
                            }
                            break;
                        case 2:
                            if (transform.position.x >= StartPosition.x - 15.0f)
                            {
                                MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = Vector2.left });
                            }
                            else
                            {
                                ActualObliqueState = 3;
                            }
                            break;
                        case 3:
                            if (transform.position.y < TopBorderPosition + 0.88f)
                            {
                                MoveController.OnLeftMovement(this, new MovementEventArgs { Direction = new Vector2(1, 1) });
                            }
                            else
                            {
                                ActualObliqueState = 0;
                            }
                            break;
                    }
                    break;
                }
                
                }
        }
       
}
