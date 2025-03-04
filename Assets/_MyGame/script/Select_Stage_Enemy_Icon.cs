using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class Select_Stage_Enemy_Icon : MonoBehaviour
{
    enum eArrangement
    {
        [InspectorName("")] nune = -1,

        threeLines,
        oneRow,

        [InspectorName("")] max,
    }[SerializeField]eArrangement arrangement = eArrangement.threeLines;

    [SerializeField]
    GameObject Image_Base;
    [SerializeField]
    GameObject[] enemyIconBase = new GameObject[(int)eEnemyType.max];
    GameObject[] enemyIcon = new GameObject[(int)eEnemyType.max];
    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }
    [SerializeField]
    float width = 25f;
    [SerializeField]
    float height = 50f;
    [SerializeField]
    int lineNum = 3;
    public void SetPosition()
    {
        GameObject tmp;
        EnemyIconDelete();
        enemyIcon = new GameObject[enemyIconBase.Length];
        for (int i = 0; i < enemyIcon.Length; i++)
        {
            if (enemyIconBase[i] == null) continue;
            if (enemyIconBase[i].gameObject.name == "nowWaveIcon") continue;

            string enemyName = enemyIconBase[i].gameObject.name;
            tmp = GameObject.Find(enemyName + "(Clone)");
            if (tmp == null) tmp = Instantiate(enemyIconBase[i]);
            enemyIcon[i] = tmp;
            tmp.transform.parent = transform;
            float PosX = ((i - 1) % lineNum) * (width * lineNum )- (width * lineNum);
            float PosY = ((i - 1) / lineNum) * height + height;
            switch (arrangement)
            {
                case eArrangement.threeLines:
                    PosX = (i % lineNum) * width - width;
                    PosY = (i / lineNum) * height + height;
                    break;
                case eArrangement.oneRow:
                    PosX = i * width  - width * (enemyIcon.Length - 1) / 2;
                    PosY = height;
                    break;
            }
            tmp.transform.localPosition = new Vector3(PosX, -PosY, 0);
            tmp.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void EnemyIconDelete()
    {
        for(int i = enemyIcon.Length - 1;i >= 0;i--) {
            Destroy(enemyIcon[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDistance();
    }
    eStage oldStage = eStage.none;
    void EnemyDistance()
    {
        eStage stageIndex = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().GetStage();
        if (oldStage == stageIndex) return; oldStage = stageIndex;

        bool[] enemy = GameObject.FindWithTag("Manager").GetComponent<Manager_Enemy>().GetStageEnemy(oldStage);
        for (int i = 0; i < (int)enemyIcon.Length; i++)
        {
            if (enemyIcon[i] == null) continue;

            if (enemy[i]) enemyIcon[i].GetComponent<Image>().color = Color.white;
            else enemyIcon[i].GetComponent<Image>().color = Color.black;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Select_Stage_Enemy_Icon))]
public class Select_Stage_Enemy_Icon_Creat : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Select_Stage_Enemy_Icon trg = target as Select_Stage_Enemy_Icon;

        if (GUILayout.Button("Create", GUILayout.Width(100f)))
        {
            trg.SetPosition();
        }
    }
}
#endif