using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_enemyIcon : MonoBehaviour
{
    enum eEnemyType
    {
        Bom,            //Bom           :基本的な敵床を壊す
        Crow,           //Crow          :プレイヤーを押し出す
        Golem,          //Golem         :地面を歩きプレイヤーを弾き出す
        LivingArmor,    //LivingArmor   :地面を歩き武器を振り回す
        EnemyMass,      //EnemyMass     :敵の集合体すべて倒すと消える

        enemyTypeMax,
    }
    eEnemyType enemyType;
    eEnemyType oldEnemyType;
    Image[] enemyIcon = new Image[(int)eEnemyType.enemyTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        waveCheck = GameObject.Find("CreateEnemy").GetComponent<EnemyCreate>();
        enemyIcon = GetComponentsInChildren<Image>();
        enemyType = (eEnemyType)(int)waveCheck.GetWaveType();
        if (enemyType > oldEnemyType)
        {
            oldEnemyType = enemyType;
        }

        EnemyIconDisplay();
    }
    EnemyCreate waveCheck;
    // Update is called once per frame
    void Update()
    {
        if((int)enemyType != (int)waveCheck.GetWaveType())
        {
            EnemyIconDisplay();

            enemyType = (eEnemyType)(int)waveCheck.GetWaveType();
            if(enemyType > oldEnemyType)
            {
                oldEnemyType = enemyType;
            }
        }
    }

    void EnemyIconDisplay()
    {
        for (int i = 0; i < enemyIcon.Length; i++)
        {
            if (i < (int)oldEnemyType)
            {
                if (i < (int)enemyType)
                {
                    enemyIcon[i].color = Color.white;
                    continue;
                }
                enemyIcon[i].color = Color.black;
                continue;
            }
            enemyIcon[i].color = Color.clear;
        }
    }
}
