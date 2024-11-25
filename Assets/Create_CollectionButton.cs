using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;


#if UNITY_EDITOR
using UnityEditor;
#endif

enum eCollectionType
{
    None = -1,

    Stage,
    Player,
    MousePointer,
    Background,
    Music,

}

public class Create_CollectionButton : MonoBehaviour
{
    GameObject manager;

    [SerializeField]
    GameObject collection_Base;
    [SerializeField,ReadOnly]
    GameObject[] collection = null;

    [SerializeField]
    eCollectionType collectionType = eCollectionType.None;
    // Start is called before the first frame update
    void Start()
    {
        if (collectionType == eCollectionType.None) Debug.Log(transform.name + " : TypeError");

        if (manager == null) manager = GameObject.FindWithTag("Manager");

        CollectionCreate();
    }

    [SerializeField]
    Sprite musicSprite;
    int wideSize = 70;
    public void CollectionCreate()
    {
        if(manager == null) manager = GameObject.FindWithTag("Manager");

        CollectionDelete();

        switch (collectionType)
        {
            case eCollectionType.None: break;
            case eCollectionType.Stage:
                collection = new GameObject[(int)eStage.eStageMax];
                for (int i = 0; i < (int)eStage.eStageMax; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (manager.GetComponent<Manager_StageSelect>().GetGetStage((eStage)i))
                    {
                        //tmp.GetComponentInChildren<SpriteRenderer>().sprite = manager.GetComponent<Manager_StageSelect>().GetBackGroundBase(i);
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_StageSelect>().SetStage((eStage)tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.Player:
                collection = new GameObject[manager.GetComponent<Manager_Player>().GetPlayerTypeBases().Length];
                for (int i = 0; i < manager.GetComponent<Manager_Player>().GetPlayerTypeBases().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (manager.GetComponent<Manager_Player>().GetGetSituation(i))
                    {
                        tmp.GetComponentInChildren<SpriteRenderer>().sprite = manager.GetComponent<Manager_Player>().GetPlayerTypeBase(i).GetComponent<SpriteRenderer>().sprite;
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player>().SetPlayerTypeIndex(tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.MousePointer:
                collection = new GameObject[manager.GetComponent<Manager_MousePointerType>().GetMousePointerAnimations().Length];
                for (int i = 0; i < manager.GetComponent<Manager_MousePointerType>().GetMousePointerAnimations().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (manager.GetComponent<Manager_MousePointerType>().GetGetSituation(i))
                    {
                        tmp.GetComponentInChildren<Animator>().runtimeAnimatorController = manager.GetComponent<Manager_MousePointerType>().GetMousePointerAnimation(i);
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_MousePointerType>().SetMousePointerIndex(tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.Background:
                collection = new GameObject[manager.GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length];
                for (int i = 0; i < manager.GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(5.5f, 5.5f);

                    if (manager.GetComponent<Manager_MousePointerType>().GetGetSituation(i))
                    {
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_BackgroundType>().SetBackGroundIndex(tmpInt));
                        tmp.GetComponentInChildren<SpriteRenderer>().sprite = manager.GetComponent<Manager_BackgroundType>().GetBackGroundBase(i);
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.Music:
                collection = new GameObject[manager.GetComponent<Manager_Music>().GetMusicBase().Length];
                for (int i = 0; i < manager.GetComponent<Manager_Music>().GetMusicBase().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(35, 35);

                    if (manager.GetComponent<Manager_MousePointerType>().GetGetSituation(i))
                    {
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Music>().SetMusicIndex(tmpInt));
                        tmp.GetComponentInChildren<SpriteRenderer>().sprite = musicSprite;
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
        }
    }
    public void CollectionDelete()
    {
        if (collection == null) return;
        for (int i = 0; i < collection.Length; i++)
        {
            GameObject.DestroyImmediate(collection[i].gameObject);
        }
        collection = null;
    }
    // Update is called once per frame
    //void Update()
    //{

    //}
}


#if UNITY_EDITOR
[CustomEditor(typeof(Create_CollectionButton))]
public class collection : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Create_CollectionButton trg = target as Create_CollectionButton;

        if (GUILayout.Button("Create", GUILayout.Width(100f)))
        {
            trg.CollectionCreate();
        }
        if (GUILayout.Button("Delete", GUILayout.Width(100f)))
        {
            trg.CollectionDelete();
        }
    }
}
#endif
