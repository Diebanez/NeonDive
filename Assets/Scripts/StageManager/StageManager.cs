using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
    public Stage[] Stages;
    private GameObject[] actualStage;
    private int actualStageIndex;
    public Text AnnouncementText;
    public int StagePause = 3;
    private bool isGameStarted = false;
    private bool canChange = true;

    private void Start()
    {
        actualStageIndex = -1;
        StartCoroutine(WaitForNextStage(StagePause));        
    }

    private void Update()
    {
        if (canChange && isGameStarted && IsStateEnded())
        {
            StartCoroutine(WaitForNextStage(StagePause));            
        }
    }

    private bool IsStateEnded()
    {
        foreach(GameObject gn in actualStage)
        {
            if(gn != null && gn.tag != "Player")
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator WaitForNextStage(int seconds)
    {
        canChange = false;
        actualStageIndex++;
        AnnouncementText.text = "Stage " + ((int)(actualStageIndex + 1));        
        if (actualStageIndex < Stages.Length || actualStageIndex == 0)
        {
            yield return new WaitForSeconds(seconds);
            actualStage = Stages[actualStageIndex].GenerateStage();
            AnnouncementText.text = "";
        }
        if(actualStageIndex >= Stages.Length)
        {
            SceneManager.LoadScene("Win_scn");
        }
        isGameStarted = true;
        canChange = true;
        
    }
}
