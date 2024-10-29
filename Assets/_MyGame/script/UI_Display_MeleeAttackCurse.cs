using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Display_MeleeAttackCurse : UI_Display__Base
{
    Create_Enemy enemyCreate;
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
        GetComponent<TextMeshProUGUI>().text = "";

        enemyCreate = GameObject.FindWithTag("Create").GetComponent<Create_Enemy>();
    }

    // Update is called once per frame
    bool fast = false;
    void Update()
    {
        if (!fast)
        {
            fast = true;

            MeleeAttackMaxNum = connectTechnique.GetComponent<Player_Technique_Container_MeleeAttackCurse>().GetEndCountLength();
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
        if (MeleeAttackNum == connectTechnique.GetComponent<Player_Technique_Container_MeleeAttackCurse>().GetEnemyCountNum()) return;
        MeleeAttackNum = connectTechnique.GetComponent<Player_Technique_Container_MeleeAttackCurse>().GetEnemyCountNum();
        for (int i = 0; i < MeleeAttackIcon.Length; i++)
        {
            if (i < MeleeAttackNum) MeleeAttackIcon[i].GetComponent<Image>().color = Color.red;
            else MeleeAttackIcon[i].GetComponent<Image>().color = Color.white;
        }
    }
}
