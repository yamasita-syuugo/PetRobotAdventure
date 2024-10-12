using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        waveCheck = GameObject.FindWithTag("Manager").GetComponent<Manager_Wave>();
        enemyIcon = GetComponentsInChildren<Image>();
        SetPosition();
        enemyType = (eEnemyType)(int)waveCheck.GetWaveType();
        if (enemyType > oldEnemyType)
        {
            oldEnemyType = enemyType;
        }

        EnemyIconDisplay();
    }
    Manager_Wave waveCheck;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Manager").GetComponent<Manager_Time>().GetTimeStop()) return;
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
    [SerializeField]
    float width = 75f;
    [SerializeField] 
    float height = 50f;
    void SetPosition()
    {
        for(int i = 0; i < enemyIcon.Length; i++)
        {
            if (enemyIcon[i].gameObject.name == "nowWaveIcon") continue;

            float PosX = (i % 3.0f) * width - width;
            float PosY = (i / 3.0f) * height - height;
            enemyIcon[i].transform.localPosition = new Vector3(PosX, PosY, 0);
        }
    }

    void EnemyIconDisplay()
    {
        for (int i = 0; i < enemyIcon.Length; i++)
        {
            if (enemyIcon[i].gameObject.name == "nowWaveIcon")
            {
                enemyIcon[i].color = Color.white;
                continue;
            }
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
