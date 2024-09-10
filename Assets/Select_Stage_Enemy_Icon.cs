using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Select_Stage_Enemy_Icon : MonoBehaviour
{
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
        for (int i = 0; i < enemyIcon.Length; i++)
        {
            if (enemyIconBase[i].Equals(null)) continue;
            if (enemyIcon[i].Equals(null) == false) continue;
            if (enemyIconBase[i].gameObject.name == "nowWaveIcon") continue;

            GameObject tmp = Instantiate(enemyIconBase[i]);
            enemyIcon[i] = tmp;
            tmp.transform.parent = transform;
            float PosX = ((i - 1) % 3.0f) * width - width;
            float PosY = ((i - 1) / 3) * height + height;
            tmp.transform.localPosition = new Vector3(PosX, -PosY, 0);
            tmp.transform.localScale = new Vector3(1,1,1);
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
        if (oldStage == GetComponent<Select_Stage>().GetStage()) return;oldStage = GetComponent<Select_Stage>().GetStage();

        switch (oldStage)
        {
            case eStage.bomOnly:break;
            case eStage.golemOnly:break;
            case eStage.lastGame:break;
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