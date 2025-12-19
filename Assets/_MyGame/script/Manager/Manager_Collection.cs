using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectFall;


public enum eCollectionType
{
    [InspectorName("")] none = -1,

    stage,
    player,
    medal,
    mousePointer,
    background,
    music,

    [InspectorName("")] max,
}
public enum eCollectionsTab
{
    [InspectorName("")] none = -1,

    stage = eCollectionType.stage,
    player = eCollectionType.player,
    medal = eCollectionType.medal,
    other,

    [InspectorName("")] max,
}

public class Manager_Collection : MonoBehaviour
{
    [SerializeField]
    GameObject collectionCoin;
    public GameObject GetCollectionCoin() { return collectionCoin; }
    int collectionCoins = 0;
    public int GetCollectionCoins() { return collectionCoins; }
    public bool AddCollectionCoins(int add = 1)
    {
        if (collectionCoins >= -add)
        {
            collectionCoins += add; return true;
        }
        else return false;
    }
    int coinDenominator = 600;
    public int GetCoinDenominator() {  return coinDenominator; }
    public void SetCoinDenominator(int coinDenominator_) { coinDenominator = coinDenominator_; }

    eCollectionsTab collectionsTab = eCollectionsTab.stage;
    public eCollectionsTab GetCollectionsTab() { return collectionsTab; }
    public void SetCollectionsTab(int collectionsType_) { collectionsTab = (eCollectionsTab)collectionsType_; }
    public void AddCollectionsTab(int add) { collectionsTab += add;if (collectionsTab <= eCollectionsTab.none) collectionsTab = eCollectionsTab.max - 1;if (collectionsTab >= eCollectionsTab.max) collectionsTab = eCollectionsTab.none + 1; }

    //class b { public int[] c = new int[4]; }
    //b[] a = new b[3];//todo:íÜêgcÇ™null
    [SerializeField]
    bool[] getSituation_Stage = new bool[(int)eStage.max];
    [SerializeField]
    bool[] getSituation_Player = new bool[16];
    [SerializeField]
    bool[] getSituation_Medal = new bool[16];
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
            case eCollectionType.stage: return getSituation_Stage[index]; break;
            case eCollectionType.player: return getSituation_Player[index]; break;
            case eCollectionType.medal: return getSituation_Medal[index]; break;
            case eCollectionType.mousePointer: return getSituation_MousePointer[index]; break;
            case eCollectionType.background: return getSituation_Background[index]; break;
            case eCollectionType.music: return getSituation_Music[index]; break;
            default: Debug.Log("NoSwitch"); break;
        }
        return false;
    }
    public void SetGetSituation(eCollectionType collectionType, int index, bool getSituation_)
    {
        switch (collectionType)
        {
            case eCollectionType.stage: getSituation_Stage[index] = getSituation_; break;
            case eCollectionType.player: getSituation_Player[index] = getSituation_; break;
            case eCollectionType.medal: getSituation_Medal[index] = getSituation_; break;
            case eCollectionType.mousePointer: getSituation_MousePointer[index] = getSituation_; break;
            case eCollectionType.background: getSituation_Background[index] = getSituation_; break;
            case eCollectionType.music: getSituation_Music[index] = getSituation_; break;
            default: Debug.Log("NoSwitch"); break;
        }
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
        PlayerPrefs.SetInt("CollectionCoins", collectionCoins);

        Manager_Save.BoolSave("StageSituation", (int)eStage.max, getSituation_Stage);
        Manager_Save.BoolSave("PlayerGetSituation", (int)ePlayerType.max, getSituation_Player);
        Manager_Save.BoolSave("MedalGetSituation", (int)eMedalType.max, getSituation_Medal);
        Manager_Save.BoolSave("MousePointerGetSituation", GetComponent<Manager_MousePointerType>().GetMousePointerAnimations().Length, getSituation_MousePointer);
        Manager_Save.BoolSave("BackGroundGetSituation", GetComponent<Manager_BackgroundType>().GetBackGround_Panel_Base().Length, getSituation_Background);
        Manager_Save.BoolSave("MusicGetSituation", GetComponent<Manager_Music>().GetMusicBase().Length, getSituation_Music);
    }
    public void DataLoad()
    {
        collectionCoins = PlayerPrefs.GetInt("CollectionCoins");

        Manager_Save.BoolLoad("StageSituation", (int)eStage.max, out getSituation_Stage);
        getSituation_Stage[0] = true;
        Manager_Save.BoolLoad("PlayerGetSituation", (int)ePlayerType.max, out getSituation_Player);
        getSituation_Player[0] = true;
        Manager_Save.BoolLoad("MedalGetSituation", (int)eMedalType.max, out getSituation_Medal);
        Manager_Save.BoolLoad("MousePointerGetSituation", GetComponent<Manager_MousePointerType>().GetMousePointerAnimations().Length, out getSituation_MousePointer);
        getSituation_MousePointer[0] = true;
        Manager_Save.BoolLoad("BackGroundGetSituation", GetComponent<Manager_BackgroundType>().GetBackGround_Panel_Base().Length, out getSituation_Background);
        Manager_Save.BoolLoad("MusicGetSituation", GetComponent<Manager_Music>().GetMusicBase().Length, out getSituation_Music);
    }
}
