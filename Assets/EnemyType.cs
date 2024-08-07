using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    [SerializeField]
    eEnemyType enemyType = eEnemyType.none;
    // Start is called before the first frame update
    void Start()
    {
        if (enemyType == eEnemyType.none) Debug.Log(name + " enemyType = none");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public eEnemyType GetEnemyType() {  return enemyType; }    
}
