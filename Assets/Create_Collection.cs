using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum eCollectionType
{
    None = -1,

    Stage,
    PlayerTechnique,
    MousePointer,
    Background,
    Music,

}

public class CollectionType : MonoBehaviour
{
    [SerializeField]
    GameObject collection_Base;

    [SerializeField]
    eCollectionType collectionType = eCollectionType.None;
    // Start is called before the first frame update
    void Start()
    {
        if (collectionType == eCollectionType.None) Debug.Log(transform.name + " : TypeError");

        CollectionCreate();
    }

    [SerializeField]
    Sprite musicSprite;
    int wideSize = 70;
    void CollectionCreate()
    {
        switch (collectionType)
        {
            case eCollectionType.None: break;
            case eCollectionType.Stage:
                for (int i = 0; i < (int)eStage.eStageMax; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().GetGetStage((eStage)i))
                    {
                        //tmp.GetComponentInChildren<SpriteRenderer>().sprite = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().GetBackGroundBase(i);
                        tmp.GetComponent<Button>().onClick.AddListener(() => GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().SetStage((eStage)i));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.PlayerTechnique:
                for (int i = 0; i < GameObject.FindWithTag("Manager").GetComponent<Manager_Player>().GetPlayerTypeBases().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    //if (GameObject.FindWithTag("Manager").GetComponent<Manager_Player>().GetPlayerTypeIndex(i))
                    //{
                        tmp.GetComponentInChildren<SpriteRenderer>().sprite = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>().GetPlayerTypeBase(i).GetComponent<SpriteRenderer>().sprite;
                        tmp.GetComponent<Button>().onClick.AddListener(() => GameObject.FindWithTag("Manager").GetComponent<Manager_Player>().SetPlayerTypeIndex(i));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    //}
                    //else
                    //{
                    //    tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    //}
                }
                break;
            case eCollectionType.MousePointer:
                for (int i = 0; i < GameObject.FindWithTag("Manager").GetComponent<Manager_MousePointerType>().GetMousePointerAnimations().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(50, 50);

                    if (GameObject.FindWithTag("Manager").GetComponent<Manager_MousePointerType>().GetGetSituation(i))
                    {
                        tmp.GetComponentInChildren<Animator>().runtimeAnimatorController = GameObject.FindWithTag("Manager").GetComponent<Manager_MousePointerType>().GetMousePointerAnimation(i);
                        tmp.GetComponent<Button>().onClick.AddListener(() => GameObject.FindWithTag("Manager").GetComponent<Manager_MousePointerType>().SetMousePointerIndex(tmpInt));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.Background:
                for (int i = 0; i < GameObject.FindWithTag("Manager").GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(5.5f, 5.5f);

                    if (GameObject.FindWithTag("Manager").GetComponent<Manager_MousePointerType>().GetGetSituation(i))
                    {
                        tmp.GetComponent<Button>().onClick.AddListener(() => GameObject.FindWithTag("Manager").GetComponent<Manager_BackgroundType>().SetBackGroundIndex(tmpInt));
                        tmp.GetComponentInChildren<SpriteRenderer>().sprite = GameObject.FindWithTag("Manager").GetComponent<Manager_BackgroundType>().GetBackGroundBase(i);
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "NO";
                    }
                }
                break;
            case eCollectionType.Music:
                for (int i = 0; i < GameObject.FindWithTag("Manager").GetComponent<Manager_Music>().GetMusicBase().Length; i++)
                {
                    GameObject tmp = Instantiate(collection_Base);
                    int tmpInt = i; //iを直接入れるとfor文のiの最終値の値になる
                    tmp.transform.parent = transform;
                    tmp.transform.localPosition = new Vector2(i * wideSize, 0);
                    tmp.transform.localScale = new Vector2(1, 1);
                    tmp.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(35, 35);

                    if (GameObject.FindWithTag("Manager").GetComponent<Manager_MousePointerType>().GetGetSituation(i))
                    {
                        tmp.GetComponent<Button>().onClick.AddListener(() => GameObject.FindWithTag("Manager").GetComponent<Manager_Music>().SetMusicIndex(tmpInt));
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
    void test() { }
    // Update is called once per frame
    //void Update()
    //{

    //}
}
