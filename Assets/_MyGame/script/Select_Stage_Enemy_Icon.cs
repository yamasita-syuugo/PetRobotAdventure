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
    Manager_StageSelect manager_StageSelect;
    Manager_Enemy manager_Enemy;

    enum eArrangement
    {
        [InspectorName("")] nune = -1,

        threeLines,
        oneRow,

        [InspectorName("")] max,
    }[SerializeField]eArrangement arrangement = eArrangement.threeLines;

    GameObject[] enemyIcon = new GameObject[(int)eEnemyType.max];
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Enemy = manager.GetComponent<Manager_Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyIconCreate();
    }
    eStage oldStage = eStage.none;
    void EnemyDistance()
    {

    }
    [SerializeField]
    float width = 25f;
    [SerializeField]
    float height = 50f;
    [SerializeField]
    int lineNum = 3;
    public void EnemyIconCreate()
    {
        eStage stageIndex = manager_StageSelect.GetStage();
        if (oldStage == stageIndex) return; oldStage = stageIndex;
        bool[] enemy = manager_Enemy.GetStageEnemy(oldStage);

        GameObject tmp;
        EnemyIconDelete();
        enemyIcon = new GameObject[(int)eEnemyType.max];
        int count = 0;
        for (int i = 0; i < enemyIcon.Length; i++)
        {
            Sprite enemyImage = manager_Enemy.GetEnemyImage((eEnemyType)i);

            if (enemyImage == null) continue;

            if (enemy[i]) tmp = Instantiate(new GameObject());
            else continue;
            tmp.name = enemyImage.name;
            tmp.transform.parent = transform;
            tmp.AddComponent<Image>().sprite = enemyImage;
            tmp.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            enemyIcon[count] = tmp;
            float PosX = ((count - 1) % lineNum) * (width * lineNum )- (width * lineNum);
            float PosY = ((count - 1) / lineNum) * height + height;
            switch (arrangement)
            {
                case eArrangement.threeLines:
                    PosX = (count % lineNum) * width - width;
                    PosY = (count / lineNum) * height + height;
                    break;
                case eArrangement.oneRow:
                    PosX = count * width  - width * (enemyIcon.Length - 1) / 2;
                    PosY = height;
                    break;
            }
            tmp.transform.localPosition = new Vector3(PosX, -PosY, 0);
            count++;   
        }
    }
    private void EnemyIconDelete()
    {
        for(int i = enemyIcon.Length - 1;i >= 0;i--) {
            Destroy(enemyIcon[i]);
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
            trg.EnemyIconCreate();
        }
    }
}
#endif