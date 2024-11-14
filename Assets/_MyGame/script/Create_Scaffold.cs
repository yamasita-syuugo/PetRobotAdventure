using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using static Unity.Burst.Intrinsics.X86.Avx;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Create_Scaffold : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    Manager_Field manager_Field;

    eScaffoldType scoffoldType = eScaffoldType.block;

    [Header("scaffoldBase")]
    GameObject[] scaffoldBases = new GameObject[(int)eScaffoldType.scaffoldMax];
    [SerializeField]
    GameObject blockBase;
    [SerializeField]
    GameObject iceBase;
    [SerializeField]
    GameObject grassBase;
    [SerializeField]
    GameObject movePanelBase;
    void SetScaffoldBases() { 
        scaffoldBases[(int)eScaffoldType.block] = blockBase;
        scaffoldBases[(int)eScaffoldType.ice] = iceBase;
        scaffoldBases[(int)eScaffoldType.grass] = grassBase;
        scaffoldBases[(int)eScaffoldType.movePanel] = movePanelBase;
    }



    [SerializeField]
    eCreatType creatType = eCreatType.block;
    public void SetCreatType(eCreatType creatType_) { creatType = creatType_; }
    [SerializeField]
    [Range(0f, 100f)]
    float randomBreak = 0.0f;
    public void SetRandomBreak(float randomBreak_)
    {
        randomBreak = randomBreak_;
        if (randomBreak < 0.0f) randomBreak = 0.0f;
        if (randomBreak > 100.0f) randomBreak = 100.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetScaffoldBases();

        GameObject manager = GameObject.FindWithTag("Manager");
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Field = manager.GetComponent<Manager_Field>();

        Load();

        CreateObject();
    }

    // Update is called once per frame
    eStage oldStage = eStage.none;
    void Update()
    {
        eStage stage = manager_StageSelect.GetStage();
        if (oldStage == stage) return; oldStage = stage;
        Debug.Log(stage);
        SetCreatType(manager_Field.GetScaffoldType()[(int)stage]);
        SetRandomBreak(manager_Field.GetRandomBreak()[(int)stage]);
        CreateObject();

    }

    //生成
    public void CreateObject()
    {
        DeleteObject();

        int blockSizeX = 1; //todo:objectからサイズを取得する
        int blockSizeY = 1;

        int blockNum = 0;

        int fieldSize = manager_Field.GetFieldSize(manager_StageSelect.GetStage());

        GameObject[] blocks = new GameObject[fieldSize * fieldSize];
                        GameObject tmpBase;
                        GameObject tmpScaffold;
        switch ((eFieldCreatType)manager_Field.GetFieldCreatTypeIndex(manager_StageSelect.GetStage()))
        {
            case eFieldCreatType.stage:     //正方形ステージ
                for (int x = 0; x < fieldSize; x++)
                {
                    for (int y = 0; y < fieldSize; y++)
                    {
                        tmpBase = ScaffoldSelect();

                        if (randomBreak > Random.RandomRange(0, 100) &&
                            !(fieldSize / 2 * 2 == fieldSize ? (x == fieldSize / 2 - 1 || x == fieldSize / 2) &&
                            (y == fieldSize / 2 - 1 || y == fieldSize / 2) : (x == fieldSize / 2) && (y == fieldSize / 2))) continue;

                        tmpScaffold = Instantiate<GameObject>(tmpBase, new Vector3(
                    (x - (float)fieldSize / 2.0f) * blockSizeX + blockSizeX / 2.0f,
                    (y - (float)fieldSize / 2.0f) * blockSizeY + blockSizeY / 2.0f, 0),
                    Quaternion.identity);
                        tmpScaffold.transform.parent = transform;

                        blocks[blockNum++] = tmpScaffold;
                    }
                }
                break;
            case eFieldCreatType.labyrinth:
                int scaffoldNum = fieldSize * fieldSize;
                int[,] ScaffoldPos = new int[scaffoldNum, 2];//0 = x;1 = y;

                ScaffoldPos[0, 0] = 0; ScaffoldPos[0, 1] = 0;

                int old1Random = Random.RandomRange(0, 4);
                if (old1Random <= 0) {          ScaffoldPos[1, 0] = ScaffoldPos[0, 0] + 1;  ScaffoldPos[1, 1] = ScaffoldPos[0, 1]; }
                else if (old1Random <= 1) {     ScaffoldPos[1, 0] = ScaffoldPos[0, 0] - 1;  ScaffoldPos[1, 1] = ScaffoldPos[0, 1]; }
                else if (old1Random <= 2) {     ScaffoldPos[1, 0] = ScaffoldPos[0, 0];      ScaffoldPos[1, 1] = ScaffoldPos[0, 1] + 1; }
                else {                          ScaffoldPos[1, 0] = ScaffoldPos[0, 0];      ScaffoldPos[1, 1] = ScaffoldPos[0, 1] - 1; }
                for(int i  = 0; i < 2; i++)
                {
                    tmpScaffold = Instantiate<GameObject>(ScaffoldSelect(), new Vector3(
                ScaffoldPos[i, 0] * blockSizeX,
                ScaffoldPos[i, 1] * blockSizeY, 0),
                Quaternion.identity);
                    tmpScaffold.transform.parent = transform;
                }

                for (int i = 2; i < scaffoldNum; i++)
                {
                    //ポジション設定
                    {
                        int[] old2Pos = { 0, 0 };
                        int[] old1Pos = { 0, 0 };
                        old2Pos[0] = ScaffoldPos[i - 2, 0]; old2Pos[1] = ScaffoldPos[i - 2, 1];
                        old1Pos[0] = ScaffoldPos[i - 1, 0]; old1Pos[1] = ScaffoldPos[i - 1, 1];

                        int random = Random.RandomRange(0, 100);
                        if (random <= 80)
                        {
                            ScaffoldPos[i, 0] = old1Pos[0] + (old1Pos[0] - old2Pos[0]);
                            ScaffoldPos[i, 1] = old1Pos[1] + (old1Pos[1] - old2Pos[1]);
                        }
                        else if (random <= 90)
                        {
                            ScaffoldPos[i, 0] = old1Pos[0] + (old1Pos[1] - old2Pos[1]);
                            ScaffoldPos[i, 1] = old1Pos[1] + (old1Pos[0] - old2Pos[0]);
                        }
                        else
                        {
                            ScaffoldPos[i, 0] = old1Pos[0] - (old1Pos[1] - old2Pos[1]);
                            ScaffoldPos[i, 1] = old1Pos[1] - (old1Pos[0] - old2Pos[0]);
                        }

                    }

                    tmpScaffold = Instantiate<GameObject>(ScaffoldSelect(), new Vector3(
                ScaffoldPos[i, 0] * blockSizeX,
                ScaffoldPos[i, 1] * blockSizeY, 0),
                Quaternion.identity);
                    tmpScaffold.transform.parent = transform;

                    blocks[blockNum++] = tmpScaffold;

                    if (i == scaffoldNum - 1) GameObject.FindWithTag("Manager").GetComponent<Manager_Gate>().SerGatePos(tmpScaffold.transform.position); 
                }
                break;
        }
    }
    GameObject ScaffoldSelect()
    {
        if (creatType == eCreatType.random) return scaffoldBases[Random.Range(0, (int)eScaffoldType.scaffoldMax)];
        return scaffoldBases[(int)creatType];
    }
    //削除
    public void DeleteObject()
    {
        Transform[] blocks = GetComponentsInChildren<Transform>();
        if (blocks == null) return;
        for (int i = 0; i < blocks.Length; i++) if (blocks[i].tag == "Scaffold") DestroyImmediate(blocks[i].gameObject);
    }

    public void Load()
    {
        Manager_StageSelect manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();

        creatType = manager_Field.GetScaffoldType()[(int)manager_StageSelect.GetStage()];
        randomBreak = manager_Field.GetRandomBreak()[(int)manager_StageSelect.GetStage()];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Create_Scaffold))]
public class a : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Create_Scaffold trg = target as Create_Scaffold;

        if (GUILayout.Button("Create", GUILayout.Width(100f))){
            trg.CreateObject();
        }
        if (GUILayout.Button("Delete", GUILayout.Width(100f))){
            trg.DeleteObject();
        }
        if (GUILayout.Button("Load", GUILayout.Width(100f))){
            trg.Load();
        }
    }
}
#endif