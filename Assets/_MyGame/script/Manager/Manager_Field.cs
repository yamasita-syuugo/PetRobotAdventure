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

    [InspectorName("")] scaffoldMax,
}
public enum eCreatType
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
    eCreatType[] creatType = new eCreatType[(int)eStage.max];
    public eCreatType[] GetScaffoldType() { return creatType; }
    float[] randomBreak = new float[(int)eStage.max];
    public float[] GetRandomBreak() { return randomBreak; }

    private void OnEnable()
    { 
        manager_StageSelect = GetComponent<Manager_StageSelect>();
        StageScaffoldSelect();
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
    int[] fieldCreatTypeIndex;
    public int GetFieldCreatTypeIndex(eStage stage) { return fieldCreatTypeIndex[(int)stage]; }
    int []fieldSize;
    public int GetFieldSize(eStage stage) { return fieldSize[(int)stage]; }
    void StageScaffoldSelect()
    {
        fieldCreatTypeIndex = new int[(int)eStage.max];
        fieldSize = new int[(int)eStage.max];
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:
                    fieldCreatTypeIndex[stage] = (int)eFieldCreatType.stage;
                    fieldSize[stage] = 7;
                    creatType[stage] = eCreatType.block;
                    randomBreak[stage] = 0.0f;
                    break;
                case eStage.crowStage:
                    fieldCreatTypeIndex[stage] = (int)eFieldCreatType.stage;
                    fieldSize[stage] = 3;
                    creatType[stage] = eCreatType.block;
                    randomBreak[stage] = 0.0f;
                    break;
                case eStage.golemLabyrinth:
                    fieldCreatTypeIndex[stage] = (int)eFieldCreatType.labyrinth;
                    fieldSize[stage] = 9;
                    creatType[stage] = eCreatType.movePanel;
                    randomBreak[stage] = 50.0f;
                    break;
                case eStage.lastGame:
                    fieldCreatTypeIndex[stage] = (int)eFieldCreatType.stage;
                    fieldSize[stage] = 9;
                    creatType[stage] = eCreatType.random;
                    randomBreak[stage] = 50.0f;
                    break;
                default:Debug.Log("error : switch(eStage)"); break;
            }
        }
    }
}
