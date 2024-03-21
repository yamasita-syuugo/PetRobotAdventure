using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class blockCreate : MonoBehaviour
{
    public GameObject blockPrefab;

    public GameObject[] blocks = { };

    public int fieldSize = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

#if UNITY_EDITOR
    //生成
    public void CreateObject()
    {
        //DeleteObject();

        int blockSizeX = 1; //todo:objectからサイズを取得する
        int blockSizeY = 1;

        int blockNum = 0;
        for(int x = 0;x < fieldSize; x++)
        {
            for(int y = 0;y < fieldSize; y++)
            {
                GameObject tmp = Instantiate<GameObject>(blockPrefab,new Vector3(
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
        for (int i = 0; i < blocks.Length ; i++)
        {
            if (blocks[i] != null)
            {
                Destroy(blocks[i]);
            }
        }

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