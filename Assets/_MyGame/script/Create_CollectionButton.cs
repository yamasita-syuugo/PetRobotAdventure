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
    Manager_StageSelect manager_StageSelect;
    Manager_Player manager_Player;
    Manager_Player_Technique manager_Player_Technique;
    Manager_MousePointerType manager_MousePointerType;
    Manager_BackgroundType manager_BackgroundType;
    Manager_Music manager_Music;


    [SerializeField]
    GameObject collection_Base;
    [SerializeField,ReadOnly]
    GameObject[] collection = null;

    [SerializeField]
    eCollectionType collectionType = eCollectionType.none;
    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Player = manager.GetComponent<Manager_Player>();
        manager_Player_Technique = manager.GetComponent<Manager_Player_Technique>();
        manager_MousePointerType = manager.GetComponent<Manager_MousePointerType>();
        manager_BackgroundType = manager.GetComponent<Manager_BackgroundType>();
        manager_Music = manager.GetComponent<Manager_Music>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CollectionCreate();
    }

    [SerializeField]
    Sprite musicSprite;
    int wideSize = 70;
    public void CollectionCreate()
    {
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
                    tmp.transform.localPosition = new Vector2(i * wideSize - ((int)eStage.max - 1) * wideSize / 2, 165);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (manager_StageSelect.GetGetSituation((eStage)i))
                    {
                        //tmp.GetComponentInChildren<SpriteRenderer>().sprite = manager_StageSelect.GetBackGroundBase(i);
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager_StageSelect.SetStage((eStage)tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.player:
                collection = new GameObject[manager_Player.GetPlayerTypeBases().Length];
                for (int i = 0; i < manager_Player.GetPlayerTypeBases().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize - (manager_Player.GetPlayerTypeBases().Length - 1) * wideSize / 2, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (manager_Player.GetGetSituation(i))
                    {
                        tmp.GetComponentInChildren<SpriteRenderer>().sprite = manager_Player.GetPlayerTypeBase(i).GetComponent<SpriteRenderer>().sprite;
                        tmp.GetComponentInChildren<Animator>().runtimeAnimatorController = manager_Player.GetPlayerTypeBase(i).GetComponent<Animator>().runtimeAnimatorController;
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager_Player.SetPlayerTypeIndex((ePlayerType)tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";

                        //TechniqueButton
                        int techniqueNum = 0;
                        Sprite[] techniqueImage = null;
                        switch ((ePlayerType)i)
                        {
                            case ePlayerType.PetRobot:
                                techniqueNum = (int)ePlayerWeaponType.max;
                                techniqueImage = manager_Player_Technique.GetWeaponImage();
                                break;
                            case ePlayerType.WizardGhost:
                                techniqueNum = (int)ePlayerMagicType.max;
                                techniqueImage = manager_Player_Technique.GetMagicImage();
                                break;
                            case ePlayerType.WereWolf:
                                techniqueNum = (int)ePlayerAttackType.max;
                                techniqueImage = manager_Player_Technique.GetAttackImage();
                                break;
                        }
                        for (int tec = 1; tec < techniqueNum; tec++)
                        {
                            GameObject tmp_Tec = Instantiate(collection_Base);
                            tmp_Tec.transform.parent = tmp.transform;
                            tmp_Tec.transform.localPosition = new Vector2(0, tec * -wideSize);
                            tmp_Tec.transform.localScale = new Vector2(1, 1);
                            tmp_Tec.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                            tmp_Tec.GetComponentInChildren<SpriteRenderer>().sprite = techniqueImage[tec];
                            tmp_Tec.GetComponentInChildren<Animator>().runtimeAnimatorController = null;
                            int tmpInt_tec = tec; //iを直接入れるとfor文のtecの最終値の値になる
                            tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager_Player.SetPlayerTypeIndex((ePlayerType)tmpInt));   //左右クリック
                            tmp_Tec.GetComponent<Button>().onClick.AddListener(() => manager_Player_Technique.SetOne(tmpInt_tec));  //左クリック
                            tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetPlayerType((ePlayerType)i);    //右クリック設定
                            tmp_Tec.GetComponent<Collection_Button_Right_Click>().SetTechniqueIndex(tec);
                            tmp_Tec.GetComponentInChildren<TextMeshProUGUI>().text = "";
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
                collection = new GameObject[manager_MousePointerType.GetMousePointerAnimations().Length];
                for (int i = 0; i < manager_MousePointerType.GetMousePointerAnimations().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize - (manager_MousePointerType.GetMousePointerAnimations().Length - 1) * wideSize / 2, 65);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (manager_MousePointerType.GetGetSituation(i))
                    {
                        tmp.GetComponentInChildren<Animator>().runtimeAnimatorController = manager_MousePointerType.GetMousePointerAnimation(i);
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager_MousePointerType.SetMousePointerIndex(tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.background:
                collection = new GameObject[manager_BackgroundType.GetBackGround_Panel_Base().Length];
                for (int i = 0; i < manager_BackgroundType.GetBackGround_Panel_Base().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize - (manager_BackgroundType.GetBackGround_Panel_Base().Length - 1) * wideSize / 2, -35);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(45, 45);

                    if (manager_BackgroundType.GetGetSituation(i))
                    {
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager_BackgroundType.SetBackGroundIndex((eBackGroundType)tmpInt));
                        tmp.GetComponentInChildren<SpriteRenderer>().sprite = manager_BackgroundType.GetBackGround_Panel_Base((eBackGroundType)i);
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.music:
                collection = new GameObject[manager_Music.GetMusicBase().Length];
                for (int i = 0; i < manager_Music.GetMusicBase().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    collection[i] = tmp;
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize - (manager_Music.GetMusicBase().Length - 1) * wideSize / 2, -135);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(35, 35);

                    if (manager_Music.GetGetSituation(i))
                    {
                        int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                        tmp.GetComponent<Button>().onClick.AddListener(() => manager_Music.SetMusicIndex(tmpInt));
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
