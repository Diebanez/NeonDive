using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public float QteTime = 4.0f;
    public GameObject QteCanvas;
    public Text PrintSequence;
    public Text PressedText;
    public Text ShowTimer;
    public bool IsPaused;
    public int QteFailDamage = 60;
    public Image QETImage;
    

    private bool ExecuteQte = false;
    private float timer = 0.0f;
    private string insertedSequence = "";
    private GameObject fromPlayer;
    private GameObject toControl;

    public GameObject ActualPlayer;
    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (!IsPaused)
        {
        }
        else if(ExecuteQte)
        {
            if (timer <= 0)
            {
                ShowTimer.text = "0.0";
                fromPlayer.GetComponent<EntityController>().TakeDamage(QteFailDamage);
                QteCanvas.SetActive(false);
                QETImage.enabled = false;
                ExecuteQte = false;
                IsPaused = false;
            }
            else
            {
                PressedText.text = insertedSequence;
                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    insertedSequence += "A";
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    insertedSequence += "B";
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton2))
                {
                    insertedSequence += "X";
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    insertedSequence += "Y";
                }
                if (insertedSequence == PrintSequence.text.Substring(0, insertedSequence.Length) && insertedSequence.Length == PrintSequence.text.Length)
                {
                    QteCanvas.SetActive(false);
                    QETImage.enabled = false;
                    ExecuteQte = false;
                    IsPaused = false;
                    toControl.GetComponent<EntityController>().IsPlayer = true;
                    toControl.tag = "Player";
                    toControl.name = "Player";
                    fromPlayer.GetComponent<MovementController>().DestroyPlayer();
                    ActualPlayer = toControl;
                    ActualPlayer.GetComponent<DiveController>().StarDiveCooldown();
                }
                else if (insertedSequence != PrintSequence.text.Substring(0, insertedSequence.Length))
                {
                    insertedSequence = "";
                }
                timer -= Time.deltaTime;
                ShowTimer.text = timer.ToString();
            }
        }
    }    
    public void StartQteEvent(GameObject actualPlayer, GameObject tryControl)
    {
        fromPlayer = actualPlayer;
        toControl = tryControl;
        insertedSequence = "";
        IsPaused = true;
        ExecuteQte = true;
        char[] sequence = new char[4];
        for(int i = 0; i < 4; i++)
        {
            switch((int)Random.Range(0, 3)){
                case 0:
                    {
                        sequence[i] = 'X';
                        break;
                    }
                case 1:
                    {
                        sequence[i] = 'Y';
                        break;
                    }
                case 2:
                    {
                        sequence[i] = 'A';
                        break;
                    }
                case 3:
                    {
                        sequence[i] = 'B';
                        break;
                    }
            }
            QteCanvas.SetActive(true);
            QETImage.enabled = true;
            string textToPrint = "";
            foreach(char ch in sequence)
            {
                textToPrint += ch;
            }
            PrintSequence.text = textToPrint;
            timer = QteTime;
        }
    }
}
