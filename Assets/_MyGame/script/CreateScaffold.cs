using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class CreateScaffold : MonoBehaviour
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

    public enum eCreatType
    {
        None,

        block, 
        grass,
        ice,
        
        random,
    }
    [SerializeField]
    eCreatType creatType = eCreatType.block;
    [SerializeField][Range(0f,100f)]
    float randomBreak = 0.0f;
    enum eRandomBreak
    {
        None,

        random0,
        //random30,
        random50,
        random70,

        randomBreakMax,
    }
    // Start is called before the first frame update
    void Start()
    {
        blocks = new GameObject[fieldSize * fieldSize];
        SetCreatType((eCreatType)PlayerPrefs.GetInt("stage"));
        switch ((eRandomBreak)PlayerPrefs.GetInt("randomBreak"))
        {
            case eRandomBreak.random0:
                GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(0);
                break;
            //case eRandomBreak.random30:
            //    GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(30);
            //    break;
            case eRandomBreak.random50:
                GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(50);
                break;
            case eRandomBreak.random70:
                GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(70);
                break;
        }
        CreateObject();
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void SetCreatType(eCreatType creatType_)
    {
        creatType = creatType_;
    }
    public void SetRandomBreak(float randomBreak_)
    {
        if(randomBreak < 0.0f) randomBreak = 0.0f;
        if (randomBreak > 100.0f) randomBreak = 100.0f;
        randomBreak = randomBreak_;
    }

    //����
    public void CreateObject()
    {
        DeleteObject();

        int blockSizeX = 1; //todo:object����T�C�Y���擾����
        int blockSizeY = 1;

        int blockNum = 0;
        for(int x = 0;x < fieldSize; x++)
        {
            for(int y = 0;y < fieldSize; y++)
            {

                if (randomBreak > Random.RandomRange(0, 100) &&
                    !(fieldSize / 2 * 2 == fieldSize ? (x == fieldSize / 2 - 1 || x == fieldSize / 2) && (y == fieldSize / 2 - 1 || y == fieldSize / 2):( x == fieldSize / 2) && ( y == fieldSize / 2))) continue;

                GameObject tmpPre;
                switch (creatType)
                {
                    case eCreatType.block: tmpPre = blockPrefab; break;
                    case eCreatType.ice: tmpPre = icePrefab; break;
                    case eCreatType.grass: tmpPre = grassPrefab; break;
                    case eCreatType.random: 
                            switch (Random.Range(0, 3)){
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
    //�폜
    public void DeleteObject()
    {
        Transform[] blocks = GetComponentsInChildren<Transform>();
        if (blocks == null) return;
        for(int i = 0;i < blocks.Length; i++)if(blocks[i].tag == "Scaffold") DestroyImmediate(blocks[i].gameObject);

        //BlockRegister();
    }
    //�T�[�`
    void BlockRegister()
    {
        blocks = new GameObject[fieldSize * fieldSize];

        for(int i = 0;i < fieldSize * fieldSize; i++)
        {
        }
        GameObject[] tmp = GetComponentsInChildren<GameObject>();
        blocks = tmp;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CreateScaffold))]
public class a : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CreateScaffold trg = target as CreateScaffold;

        if (GUILayout.Button("Create", GUILayout.Width(100f))){
            trg.CreateObject();
        }
        if (GUILayout.Button("Delete", GUILayout.Width(100f))){
            trg.DeleteObject();
        }
    }
}
#endif