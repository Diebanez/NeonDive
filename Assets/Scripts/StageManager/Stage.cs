using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage : MonoBehaviour
{
    public Enemy[] Enemies;

    public GameObject[] GenerateStage()
    {
        GameObject[] arrayToReturn = new GameObject[Enemies.Length];
        for (int i = 0; i < Enemies.Length; i++)
        {
            arrayToReturn[i] = Enemies[i].GenerateEnemy();
        }

        return arrayToReturn;
    }
}
