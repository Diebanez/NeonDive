using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public GameObject prefab;
    public Vector2 StartPosition;
    public Patterns StagePattern;

    public GameObject GenerateEnemy()
    {

        GameObject obj = Instantiate(prefab, StartPosition, Quaternion.identity);
        obj.GetComponent<EnemyController>().ActualPattern = StagePattern;
        return obj;
    }
}
