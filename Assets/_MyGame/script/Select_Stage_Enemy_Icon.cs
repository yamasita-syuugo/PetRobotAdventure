using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Select_Stage_Enemy_Icon : MonoBehaviour
{
    [SerializeField]
    GameObject Image_Base;
    [SerializeField]
    GameObject[] enemyIconBase = new GameObject[(int)eEnemyType.enemyTypeMax];
    GameObject[] enemyIcon = new GameObject[(int)eEnemyType.enemyTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }
    [SerializeField]
    float width = 75f;
    [SerializeField]
    float height = 50f;
    public void SetPosition()
    {
        GameObject tmp;
        for (int i = 0; i < enemyIcon.Length; i++)
        {
            if (enemyIconBase[i] == null) continue;
            if (enemyIconBase[i].gameObject.name == "nowWaveIcon") continue;

            string enemyName = enemyIconBase[i].gameObject.name;
            tmp = GameObject.Find(enemyName + "(Clone)");
            if (tmp == null) tmp = Instantiate(enemyIconBase[i]);
            enemyIcon[i] = tmp;
            tmp.transform.parent = transform;
            float PosX = ((i - 1) % 3.0f) * width - width;
            float PosY = ((i - 1) / 3) * height + height;
            tmp.transform.localPosition = new Vector3(PosX, -PosY, 0);
            tmp.transform.localScale = new Vector3(1, 1, 1);
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
        Manager_StageSelect manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
        if (oldStage == manager_StageSelect.GetStage()) return; oldStage = manager_StageSelect.GetStage();

        bool[,] enemy = GameObject.FindWithTag("Manager").GetComponent<Manager_Enemy>().GetStageEnemy();
        for (int i = 0; i < (int)enemyIcon.Length; i++)
        {
            if (enemyIcon[i] == null) continue;

            if (enemy[(int)oldStage, i]) enemyIcon[i].GetComponent<Image>().color = Color.white;
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