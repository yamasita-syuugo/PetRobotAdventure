using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_enemyIcon : MonoBehaviour
{
    eEnemyType enemyType;
    eEnemyType oldEnemyType;
    Image[] enemyIcon = new Image[(int)eEnemyType.enemyTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        waveCheck = GameObject.Find("TimeManager").GetComponent<WaveManager>();
        enemyIcon = GetComponentsInChildren<Image>();
        enemyType = (eEnemyType)(int)waveCheck.GetWaveType();
        if (enemyType > oldEnemyType)
        {
            oldEnemyType = enemyType;
        }

        EnemyIconDisplay();
    }
    WaveManager waveCheck;
    // Update is called once per frame
    void Update()
    {
        if((int)enemyType != (int)waveCheck.GetWaveType())
        {
            enemyType = (eEnemyType)(int)waveCheck.GetWaveType();
            if(enemyType > oldEnemyType)
            {
                oldEnemyType = enemyType;
            }
            EnemyIconDisplay();
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
