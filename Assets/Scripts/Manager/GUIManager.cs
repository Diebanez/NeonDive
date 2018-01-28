using UnityEngine.SceneManagement;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        SceneManager.LoadScene("Main_scn");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
