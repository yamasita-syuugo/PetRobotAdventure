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
    [SerializeField]
    GameObject blockPrefab;
    [SerializeField]
    GameObject icePrefab;
    [SerializeField]
    GameObject grassPrefab;

    GameObject[] blocks;

    [SerializeField]
    int fieldSize = 9;

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
    //enum eRandomBreak
    //{
    //    None,

    //    random0,
    //    //random30,
    //    random50,
    //    random70,

    //    randomBreakMax,
    //}
    // Start is called before the first frame update
    void Start()
    {
        Load();

        CreateObject();
    }

    // Update is called once per frame
    eStage oldStage = eStage.none;
    void Update()
    {
        GameObject tmp = GameObject.FindWithTag("Manager");
        if (tmp != null)
        {
            Manager_StageSelect manager_StageSelect = tmp.GetComponent<Manager_StageSelect>();
            eStage stage = manager_StageSelect.GetStage();
            if (oldStage != stage)
            {
                oldStage = stage;
                SetCreatType(manager_StageSelect.GetScaffoldType()[(int)stage]);
                SetRandomBreak(manager_StageSelect.GetRandomBreak()[(int)stage]);
                CreateObject();
            }
        }
    }

    //生成
    public void CreateObject()
    {
        DeleteObject();

        int blockSizeX = 1; //todo:objectからサイズを取得する
        int blockSizeY = 1;

        int blockNum = 0;
        blocks = new GameObject[fieldSize * fieldSize];
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {

                if (randomBreak > Random.RandomRange(0, 100) &&
                    !(fieldSize / 2 * 2 == fieldSize ? (x == fieldSize / 2 - 1 || x == fieldSize / 2) && (y == fieldSize / 2 - 1 || y == fieldSize / 2) : (x == fieldSize / 2) && (y == fieldSize / 2))) continue;

                GameObject tmpPre;
                switch (creatType)
                {
                    case eCreatType.block: tmpPre = blockPrefab; break;
                    case eCreatType.ice: tmpPre = icePrefab; break;
                    case eCreatType.grass: tmpPre = grassPrefab; break;
                    case eCreatType.random:
                        switch (Random.Range(0, 3))
                        {
                            case (int)eCreatType.block - 1:
                                tmpPre = blockPrefab;
                                break;
                            case (int)eCreatType.ice - 1:
                                tmpPre = icePrefab;
                                break;
                            case (int)eCreatType.grass - 1:
                                tmpPre = grassPrefab;
                                break;
                            default: tmpPre = blockPrefab; break;
                        }
                        break;

                    default: tmpPre = blockPrefab; break;
                }
                GameObject tmp = Instantiate<GameObject>(tmpPre, new Vector3(
                    (x - (float)fieldSize / 2.0f) * blockSizeX + blockSizeX / 2.0f,
                    (y - (float)fieldSize / 2.0f) * blockSizeY + blockSizeY / 2.0f, 0),
                    Quaternion.identity);
                tmp.transform.parent = transform;

                blocks[blockNum++] = tmp;
            }
        }

        //BlockRegister();
    }
    //削除
    public void DeleteObject()
    {
        Transform[] blocks = GetComponentsInChildren<Transform>();
        if (blocks == null) return;
        for (int i = 0; i < blocks.Length; i++) if (blocks[i].tag == "Scaffold") DestroyImmediate(blocks[i].gameObject);

        //BlockRegister();
    }
    //サーチ
    void BlockRegister()
    {
        blocks = new GameObject[fieldSize * fieldSize];

        for (int i = 0; i < fieldSize * fieldSize; i++)
        {
        }
        GameObject[] tmp = GetComponentsInChildren<GameObject>();
        blocks = tmp;
    }

    public void Load()
    {
        Manager_StageSelect manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();

        creatType = manager_StageSelect.GetScaffoldType()[(int)manager_StageSelect.GetStage()];
        randomBreak = manager_StageSelect.GetRandomBreak()[(int)manager_StageSelect.GetStage()];
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