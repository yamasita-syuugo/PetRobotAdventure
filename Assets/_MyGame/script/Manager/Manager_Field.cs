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

    [SerializeField,Range(0,(int)eFieldCreatType.max - 1)]int []fieldCreatTypeIndex = new int[(int)eStage.eStageMax];
    public int GetFieldCreatTypeIndex(eStage stage) {  return fieldCreatTypeIndex[(int)stage]; }

    [SerializeField]
    int []fieldSize = new int[(int)eStage.eStageMax];
    public int GetFieldSize(eStage stage) { return fieldSize[(int)stage]; }

    //足場の配置パターン
    eCreatType[] creatType = new eCreatType[(int)eStage.eStageMax];
    public eCreatType[] GetScaffoldType() { return creatType; }
    float[] randomBreak = new float[(int)eStage.eStageMax];
    public float[] GetRandomBreak() { return randomBreak; }
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GetComponent<Manager_StageSelect>();
        StageScaffoldSelect();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StageScaffoldSelect()
    {
        for (int stage = 0; stage < (int)eStage.eStageMax; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.bomOnly:
                    fieldCreatTypeIndex[stage] = (int)eFieldCreatType.stage;
                    fieldSize[stage] = 7;
                    creatType[stage] = eCreatType.block;
                    randomBreak[stage] = 0.0f;
                    break;
                case eStage.golemOnly:
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
            }
        }
    }
}
