using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectFall;


public enum eCollectionType
{
    None = -1,

    Stage,
    Player,
    MousePointer,
    Background,
    Music,

[InspectorName("")]max,
}

public class Manager_Collection : MonoBehaviour
{
    [SerializeField]
    bool[] getSituation_Stage = new bool[(int)eStage.max];
    [SerializeField]
    bool[] getSituation_Player = new bool[16];
    [SerializeField]
    bool[] getSituation_MousePointer = new bool[16];
    [SerializeField]
    bool[] getSituation_Background = new bool[16];
    [SerializeField]
    bool[] getSituation_Music = new bool[16];
    public bool GetGetSituation(eCollectionType collectionType, int index)
    {
        switch (collectionType)
        {
            case eCollectionType.Stage: return getSituation_Stage[index]; break;
            case eCollectionType.Player: return getSituation_Player[index]; break;
            case eCollectionType.MousePointer: return getSituation_MousePointer[index]; break;
            case eCollectionType.Background: return getSituation_Background[index]; break;
            case eCollectionType.Music: return getSituation_Music[index]; break;
            default: Debug.Log("NoSwitch"); break;
        }
        return false;
    }
    public void SetGetSituation(eCollectionType collectionType, int index, bool getSituation_)
    {
        switch (collectionType)
        {
            case eCollectionType.Stage: getSituation_Stage[index] = getSituation_; break;
            case eCollectionType.Player: getSituation_Player[index] = getSituation_; break;
            case eCollectionType.MousePointer: getSituation_MousePointer[index] = getSituation_; break;
            case eCollectionType.Background: getSituation_Background[index] = getSituation_; break;
            case eCollectionType.Music: getSituation_Music[index] = getSituation_; break;
            default: Debug.Log("NoSwitch"); break;
        }
    }
    private void OnEnable()
    {
        DataLoad();
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
    public void DataSave()
    {
        Manager_Save.BoolSave("StageSituation", (int)eStage.max, getSituation_Stage);
        Manager_Save.BoolSave("PlayerGetSituation", GetComponent<Manager_Player>().GetPlayerTypeBases().Length, getSituation_Player);
        Manager_Save.BoolSave("MousePointerGetSituation", GetComponent<Manager_MousePointerType>().GetMousePointerAnimations().Length, getSituation_MousePointer);
        Manager_Save.BoolSave("BackGroundGetSituation", GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length, getSituation_Background);
        Manager_Save.BoolSave("MusicGetSituation", GetComponent<Manager_Music>().GetMusicBase().Length, getSituation_Music);
    }
    public void DataLoad()
    {
        Manager_Save.BoolLoad("StageSituation", (int)eStage.max, out getSituation_Stage);
        Manager_Save.BoolLoad("PlayerGetSituation", GetComponent<Manager_Player>().GetPlayerTypeBases().Length, out getSituation_Player);
        Manager_Save.BoolLoad("MousePointerGetSituation", GetComponent<Manager_MousePointerType>().GetMousePointerAnimations().Length, out getSituation_MousePointer);
        Manager_Save.BoolLoad("BackGroundGetSituation", GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length, out getSituation_Background);
        Manager_Save.BoolLoad("MusicGetSituation", GetComponent<Manager_Music>().GetMusicBase().Length, out getSituation_Music);
    }
}
