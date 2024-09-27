using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public enum eStage
{
    [InspectorName("")]none,

    bomOnly,
    golemOnly,

    lastGame,

    [InspectorName("")]eStageMax,
}
public class Manager_StageSelect : MonoBehaviour
{
    //ステージの選択
    [SerializeField,ReadOnly]
    eStage stage;
    public eStage GetStage() { return stage; }
    public void SetStage(eStage stage_) { 
        stage = stage_;
        if (stage <= eStage.none) stage = eStage.eStageMax - 1;
        else if (stage >= eStage.eStageMax) stage = eStage.none + 1;
    }
    public void AddStage() { 
        stage += 1;
        if (stage <= eStage.none) stage = eStage.eStageMax - 1;
        else if (stage >= eStage.eStageMax) stage = eStage.none + 1;
    }
    public void DataSave() { PlayerPrefs.SetInt("stage", (int)stage); }
    public void DataLoad() { stage = (eStage)PlayerPrefs.GetInt("stage"); }

    //敵の出現パターン
    bool[,] stageEnemy = new bool[(int)eStage.eStageMax,(int)eEnemyType.enemyTypeMax];
    public bool[,] GetStageEnemy() {  return stageEnemy; }

    //足場の配置パターン
    eCreatType [] creatType = new eCreatType[(int)eStage.eStageMax];
    public eCreatType[] GetScaffoldType() { return creatType; }
    float []randomBreak = new float[(int)eStage.eStageMax];
    public float[] GetRandomBreak() {  return randomBreak; }

    //背景の選択パターン
    eBackGroundType[] backGroundTypes = new eBackGroundType[(int)eStage.eStageMax];
    public eBackGroundType[] GetBackGroundTypes() { return backGroundTypes; }

    //ステージクリアの条件パターン
    eGateOpenType []gateOpenType = new eGateOpenType[(int)eStage.eStageMax];
    public eGateOpenType[] GetGateOpenType() {  return gateOpenType; }
    int []gateOpenNum = new int[(int)eStage.eStageMax];
    public int[] GetGateOpenNum() {  return gateOpenNum; }

    private void OnEnable()
    {
        DataLoad(); 
        StageEnemySelect();
        StageScaffoldSelect();
        StageBackGroundSelect();
        StageGatoOpenTypeSelect();
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}
    void StageEnemySelect()
    {
        for (int stage = 0; stage < (int)eStage.eStageMax; stage++)
        {
            for (int enemy = 0; enemy < (int)eEnemyType.enemyTypeMax; enemy++)
            {
                bool tmp = false;
                switch ((eStage)stage)
                {
                    case eStage.bomOnly:
                        switch ((eEnemyType)enemy)
                        {
                            case eEnemyType.Bom: tmp = true; break;
                            case eEnemyType.Crow: break;
                            case eEnemyType.Golem: break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.golemOnly:
                        switch ((eEnemyType)enemy)
                        {
                            case eEnemyType.Bom: break;
                            case eEnemyType.Crow: break;
                            case eEnemyType.Golem: tmp = true; break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.lastGame: break;
                    default:
                        switch ((eEnemyType)enemy)
                        {
                            case eEnemyType.Bom: tmp = true; break;
                            case eEnemyType.Crow: tmp = true; break;
                            case eEnemyType.Golem: tmp = true; break;
                            case eEnemyType.LivingArmor: tmp = true; break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                }
                stageEnemy[stage,enemy] = tmp;
            }
        }
    }
    void StageScaffoldSelect()
    {
        for (int stage = 0; stage < (int)eStage.eStageMax; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.bomOnly:
                    creatType[stage] = eCreatType.block;
                    randomBreak[stage] = 0.0f;
                    break;
                case eStage.golemOnly:
                    creatType[stage] = eCreatType.block;
                    randomBreak[stage] = 50.0f;
                    break;
                case eStage.lastGame:
                    creatType[stage] = eCreatType.random;
                    randomBreak[stage] = 0.0f;
                    break;
            }
        }
    }
    void StageBackGroundSelect()
    {
        for (int stage = 0; stage < (int)eStage.eStageMax; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.bomOnly:
                    backGroundTypes[stage] = eBackGroundType.sea;
                    break;
                case eStage.golemOnly:
                    backGroundTypes[stage] = eBackGroundType.sea;
                    break;
                case eStage.lastGame:
                    backGroundTypes[stage] = eBackGroundType.forest;
                    break;
            }
        }
    }
    void StageGatoOpenTypeSelect()
    {
        for (int stage = 0; stage < (int)eStage.eStageMax; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.bomOnly:
                    gateOpenType[stage] = eGateOpenType.scoreCheck_Posi_Destroy_Bom;
                    gateOpenNum[stage] = 15;
                    break;
                case eStage.golemOnly:
                    gateOpenType[stage] = eGateOpenType.none;
                    gateOpenNum[stage] = 30;
                    break;
                case eStage.lastGame:
                    gateOpenType[stage] = eGateOpenType.none;
                    gateOpenNum[stage] = 30;
                    break;
            }
        }
    }


    // Update is called once per frame
    eStage oldStage = eStage.none + 1;
    void Update()
    {
        {
            if (stage <= eStage.none) stage = eStage.eStageMax - 1;
            else if (stage >= eStage.eStageMax) stage = eStage.none + 1;
        }
        {
            if (oldStage != stage)
            {
                oldStage = stage;
                GameObject tmp = GameObject.Find("CreateScaffold");
                if (tmp != null)
                {
                    CreateScaffold createScaffold = tmp.GetComponent<CreateScaffold>();
                    createScaffold.SetCreatType(creatType[(int)stage]);
                    createScaffold.SetRandomBreak(randomBreak[(int)stage]);
                }
            }
        }
    }
}
