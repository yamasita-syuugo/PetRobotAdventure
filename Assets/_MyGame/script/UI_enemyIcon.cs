using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_enemyIcon : MonoBehaviour
{
    enum eEnemyType
    {
        Bom,            //Bom           :��{�I�ȓG������
        Crow,           //Crow          :�v���C���[�������o��
        Golem,          //Golem         :�n�ʂ�����v���C���[��e���o��
        LivingArmor,    //LivingArmor   :�n�ʂ���������U���
        EnemyMass,      //EnemyMass     :�G�̏W���̂��ׂē|���Ə�����

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
