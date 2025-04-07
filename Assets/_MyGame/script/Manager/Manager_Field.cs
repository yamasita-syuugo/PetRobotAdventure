using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eFieldCreatType
{
    [InspectorName("")] none = -1,

    stage,
    labyrinth,
    frameStage,
    //dungeon,
    bossStage,

    [InspectorName("")] max,
}
public enum eScaffoldType
{
    [InspectorName("")] none = -1,

    block,
    grass,
    ice,
    movePanel,

    //rail
    //トロッコ

    [InspectorName("")] max,
}
public enum eCreatScaffoldType
{
    [InspectorName("")] None = -1,

    blockOnly,
    grassOnly,
    iceOnly,
    movePanelOnly,

    random,
}
public class Manager_Field : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    //足場の配置パターン
    //eCreatScaffoldType[] creatType = new eCreatScaffoldType[(int)eStage.max];
    public eCreatScaffoldType GetScaffoldType(eStage stage) { return manager_StageSelect.GetStageData(stage).GetCreatScaffoldType(); }
    //float[] randomBreak = new float[(int)eStage.max];
    public float GetRandomBreak(eStage stage) { return manager_StageSelect.GetStageData(stage).GetRandomScaffoldBreak(); }
    public int GetHoleSize(eStage stage) { return manager_StageSelect.GetStageData(stage).GetHoleSize(); }

    [Header("scaffoldBase")]
    GameObject[] scaffoldBases = new GameObject[(int)eScaffoldType.max];
    public GameObject GetScaffoldBases(eScaffoldType scaffoldType) { return scaffoldBases[(int)scaffoldType]; }
    void SetScaffoldBases()
    {
        scaffoldBases[(int)eScaffoldType.block] = blockBase;
        scaffoldBases[(int)eScaffoldType.ice] = iceBase;
        scaffoldBases[(int)eScaffoldType.grass] = grassBase;
        scaffoldBases[(int)eScaffoldType.movePanel] = movePanelBase;
    }
    [SerializeField]
    GameObject blockBase;
    [SerializeField]
    GameObject iceBase;
    [SerializeField]
    GameObject grassBase;
    [SerializeField]
    GameObject movePanelBase;

    private void OnEnable()
    {
        manager_StageSelect = GetComponent<Manager_StageSelect>();

        SetScaffoldBases();
    }
    // Start is called before the first frame update
    //    void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public GameObject ScaffoldSelect()
    {
        eCreatScaffoldType creatScaffoldType = manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetCreatScaffoldType();
        if (creatScaffoldType == eCreatScaffoldType.random) return GetScaffoldBases((eScaffoldType)Random.Range(0, (int)eScaffoldType.max));
        return GetScaffoldBases((eScaffoldType)creatScaffoldType);
    }
    /*[SerializeField, Range(0, (int)eFieldCreatType.max - 1)]*/
    //int[] fieldCreatTypeIndex;
    public eFieldCreatType GetFieldCreatTypeIndex(eStage stage) { return manager_StageSelect.GetStageData(stage).GetFieldCreatTypeIndex(); }
    //int []fieldSize;
    public int GetFieldSize(eStage stage) { return manager_StageSelect.GetStageData(stage).GetFieldSize(); }
}
