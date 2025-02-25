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

public class Create_CollectionButton : MonoBehaviour
{
    GameObject manager;

    [SerializeField]
    GameObject collection_Base;
    [SerializeField,ReadOnly]
    GameObject[] collection = null;

    [SerializeField]
    eCollectionType collectionType = eCollectionType.none;
    // Start is called before the first frame update
    void Start()
    {
        if (collectionType == eCollectionType.none) Debug.Log(transform.name + " : TypeError");

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
            case eCollectionType.none: break;
            case eCollectionType.stage:
                collection = new GameObject[(int)eStage.max];
                for (int i = 0; i < (int)eStage.max; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (manager.GetComponent<Manager_StageSelect>().GetGetSituation((eStage)i))
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
            case eCollectionType.player:
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
                        tmp.GetComponentInChildren<Animator>().runtimeAnimatorController = manager.GetComponent<Manager_Player>().GetPlayerTypeBase(i).GetComponent<Animator>().runtimeAnimatorController;
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player>().SetPlayerTypeIndex((ePlayerType)tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";

                        //TechniqueButton
                        switch ((ePlayerType)i)
                        {
                            case ePlayerType.PetRobot:
                                for (int tec = 1; tec < (int)ePlayerWeaponType.max; tec++)
                                {
                                    GameObject tmp_Tec = Instantiate(collection_Base);
                                    tmp_Tec.transform.parent = tmp.transform;
                                    tmp_Tec.transform.localPosition = new Vector2(0, tec * -wideSize);
                                    tmp_Tec.transform.localScale = new Vector2(1, 1);
                                    tmp_Tec.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                                    tmp_Tec.GetComponentInChildren<SpriteRenderer>().sprite = manager.GetComponent<Manager_Player_Technique>().GetWeaponImage(tec);
                                    tmp_Tec.GetComponentInChildren<Animator>().runtimeAnimatorController = null;
                                    int tmpInt_tec = tec; //iを直接入れるとfor文のtecの最終値の値になる
                                    tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player>().SetPlayerTypeIndex((ePlayerType)tmpInt));   //左右クリック
                                    tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player_Technique>().SetOne(tmpInt_tec));  //左クリック
                                    tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetPlayerType((ePlayerType)i);    //右クリック設定
                                    tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetTechniqueIndex(tec);
                                    tmp_Tec.GetComponentInChildren<TextMeshProUGUI>().text = "";
                                }
                                break;
                                case ePlayerType.WizardGhost:
                                for (int tec = 1; tec < (int)ePlayerMagicType.max; tec++)
                                {
                                    GameObject tmp_Tec = Instantiate(collection_Base);
                                    tmp_Tec.transform.parent = tmp.transform;
                                    tmp_Tec.transform.localPosition = new Vector2(0, tec * -wideSize);
                                    tmp_Tec.transform.localScale = new Vector2(1, 1);
                                    tmp_Tec.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                                    tmp_Tec.GetComponentInChildren<SpriteRenderer>().sprite = manager.GetComponent<Manager_Player_Technique>().GetMagicImage(tec);
                                    tmp_Tec.GetComponentInChildren<Animator>().runtimeAnimatorController = null;
                                    int tmpInt_tec = tec; //iを直接入れるとfor文のtecの最終値の値になる
                                    tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player>().SetPlayerTypeIndex((ePlayerType)tmpInt));   //左右クリック
                                    tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player_Technique>().SetOne(tmpInt_tec));  //左クリック
                                    tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetPlayerType((ePlayerType)i);    //右クリック設定
                                    tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetTechniqueIndex(tec);
                                    tmp_Tec.GetComponentInChildren<TextMeshProUGUI>().text = "";
                                }
                                break;
                                case ePlayerType.WereWolf:
                                for (int tec = 1; tec < (int)ePlayerAttackType.max; tec++)
                                {
                                    GameObject tmp_Tec = Instantiate(collection_Base);
                                    tmp_Tec.transform.parent = tmp.transform;
                                    tmp_Tec.transform.localPosition = new Vector2(0, tec * -wideSize);
                                    tmp_Tec.transform.localScale = new Vector2(1, 1);
                                    tmp_Tec.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                                    tmp_Tec.GetComponentInChildren<SpriteRenderer>().sprite = manager.GetComponent<Manager_Player_Technique>().GetAttackImage(tec);
                                    tmp_Tec.GetComponentInChildren<Animator>().runtimeAnimatorController = null;
                                    int tmpInt_tec = tec; //iを直接入れるとfor文のtecの最終値の値になる
                                    tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player>().SetPlayerTypeIndex((ePlayerType)tmpInt));   //左右クリック
                                    tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager.GetComponent<Manager_Player_Technique>().SetOne(tmpInt_tec));  //左クリック
                                    tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetPlayerType((ePlayerType)i);    //右クリック設定
                                    tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetTechniqueIndex(tec);
                                    tmp_Tec.GetComponentInChildren<TextMeshProUGUI>().text = "";
                                }
                                break;
                        }

                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.medal:break;
            case eCollectionType.mousePointer:
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
            case eCollectionType.background:
                collection = new GameObject[manager.GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length];
                for (int i = 0; i < manager.GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(2f, 2f);

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
            case eCollectionType.music:
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
