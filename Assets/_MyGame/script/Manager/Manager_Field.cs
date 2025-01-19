using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eFieldCreatType
{
    [InspectorName("")] none = -1,

    stage,
    labyrinth,
    //dungeon,

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

    block,
    grass,
    ice,
    movePanel,

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

    private void OnEnable()
    { 
        manager_StageSelect = GetComponent<Manager_StageSelect>();
    }
        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    /*[SerializeField, Range(0, (int)eFieldCreatType.max - 1)]*/ 
    //int[] fieldCreatTypeIndex;
    public eFieldCreatType GetFieldCreatTypeIndex(eStage stage) { return manager_StageSelect.GetStageData(stage).GetFieldCreatTypeIndex(); }
    //int []fieldSize;
    public int GetFieldSize(eStage stage) { return manager_StageSelect.GetStageData(stage).GetFieldSize(); }
}
