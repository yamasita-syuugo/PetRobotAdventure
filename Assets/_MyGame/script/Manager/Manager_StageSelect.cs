using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public enum eStage
{
    [InspectorName("")]none = -1,

    bomOnly,
    golemOnly,

    lastGame,

    //dungeon,       //部屋と通路を交互に自動作成、部屋で弾補給　通路を前進
    //labyrinth,     //自動生成迷路

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
    [SerializeField]
    bool[] getStage = new bool[(int)eStage.eStageMax];
    public bool GetGetStage (eStage stage) { return getStage[(int)stage]; }
    public void DataSave()
    {
        PlayerPrefs.SetInt("stage", (int)stage);
        Manager_Save.BoolSave("StageSituation", (int)eStage.eStageMax, getStage);
    }
    public void DataLoad()
    {
        stage = (eStage)PlayerPrefs.GetInt("stage");
        Manager_Save.BoolLoad("StageSituation", (int)eStage.eStageMax,out getStage);
    }

    //足場の配置パターン
    eCreatType [] creatType = new eCreatType[(int)eStage.eStageMax];
    public eCreatType[] GetScaffoldType() { return creatType; }
    float []randomBreak = new float[(int)eStage.eStageMax];
    public float[] GetRandomBreak() {  return randomBreak; }

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
                GetComponent<Manager_Enemy>().SetStageEnemy(stage, enemy, tmp);
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
                GameObject tmp = GameObject.FindWithTag("Create");
                if (tmp != null)
                {
                    Create_Scaffold createScaffold = tmp.GetComponent<Create_Scaffold>();
                    createScaffold.SetCreatType(creatType[(int)stage]);
                    createScaffold.SetRandomBreak(randomBreak[(int)stage]);
                }
            }
        }
    }
}
