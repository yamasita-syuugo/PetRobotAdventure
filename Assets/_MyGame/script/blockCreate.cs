using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class blockCreate : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab;
    [SerializeField]
    GameObject icePrefab;
    [SerializeField]
    GameObject grassPrefab;

    [SerializeField]
    GameObject[] blocks = new GameObject[100];

    [SerializeField]
     int fieldSize = 9;

    enum eCreatType
    {
        block, 
        ice,
        grass,
        
        random,
    }
    [SerializeField]
    eCreatType creatType = eCreatType.block;
    // Start is called before the first frame update
    void Start()
    {
        CreateObject();
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

#if UNITY_EDITOR
    //生成
    public void CreateObject()
    {
        DeleteObject();

        int blockSizeX = 1; //todo:objectからサイズを取得する
        int blockSizeY = 1;

        int blockNum = 0;
        for(int x = 0;x < fieldSize; x++)
        {
            for(int y = 0;y < fieldSize; y++)
            {
                GameObject tmpPre;
                switch (creatType)
                {
                    case eCreatType.block: tmpPre = blockPrefab; break;
                    case eCreatType.ice: tmpPre = icePrefab; break;
                    case eCreatType.grass: tmpPre = grassPrefab; break;
                    case eCreatType.random: tmpPre = ((Random.Range(0, 2) == 1) ? icePrefab : blockPrefab); break;

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
        for(int i = 0;i < blocks.Length; i++)if(blocks[i].tag == "Scaffold") DestroyImmediate(blocks[i].gameObject);

        //BlockRegister();
    }
    //サーチ
    void BlockRegister()
    {
        blocks = new GameObject[fieldSize * fieldSize];

        for(int i = 0;i < fieldSize * fieldSize; i++)
        {
        }
        GameObject[] tmp = GetComponentsInChildren<GameObject>();
        blocks = tmp;
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(blockCreate))]
public class a : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        blockCreate trg = target as blockCreate;

        if (GUILayout.Button("Create", GUILayout.Width(100f))){
            trg.CreateObject();
        }
        if (GUILayout.Button("Delete", GUILayout.Width(100f))){
            trg.DeleteObject();
        }
    }
}
#endif