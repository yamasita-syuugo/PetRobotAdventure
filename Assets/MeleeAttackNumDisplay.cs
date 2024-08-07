using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAttackNumDisplay : MonoBehaviour
{
    CreateEnemy enemyCreate;
    int MeleeAttackNum;
    int MeleeAttackMaxNum;
    [SerializeField]
    GameObject MeleeAttackIconBase;
    GameObject[] MeleeAttackIcon;
    [SerializeField]
    float wide = 150f;
    // Start is called before the first frame update
    void Start()
    {
        enemyCreate = GameObject.Find("CreateEnemy").GetComponent<CreateEnemy>();
        
    }

    // Update is called once per frame
    bool fast = false;
    void Update()
    {if(!fast) { fast = true; MeleeAttackMaxNum = enemyCreate.GetEndCountLength();
        MeleeAttackIcon = new GameObject[MeleeAttackMaxNum];
        for (int i = 0; i < MeleeAttackMaxNum; i++)
        {
            MeleeAttackIcon[i] = Instantiate(MeleeAttackIconBase, gameObject.transform);
            MeleeAttackIcon[i].transform.localPosition = new Vector3(i * wide / MeleeAttackMaxNum - wide / 2, 0, 0);
        }
        }
        BulletIconDisplay();
    }

    void BulletIconDisplay()
    {
        if (MeleeAttackNum == enemyCreate.GetEnemyCountNum()) return;
        MeleeAttackNum = enemyCreate.GetEnemyCountNum();
        for (int i = 0; i < MeleeAttackIcon.Length; i++)
        {
            if (i < MeleeAttackNum) MeleeAttackIcon[i].GetComponent<Image>().color = Color.red;
            else MeleeAttackIcon[i].GetComponent<Image>().color = Color.white;
        }
    }
}
