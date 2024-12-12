using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public enum eStage
{
    [InspectorName("")]none = -1,

    fastPlay,
    crowStage,
    golemLabyrinth,

    lastGame,

    //dungeon,       //部屋と通路を交互に自動作成、部屋で弾補給　通路を前進
    //labyrinth,     //自動生成迷路

    [InspectorName("")]max,
}
public class Manager_StageSelect : MonoBehaviour
{
    //ステージの選択
    [SerializeField,ReadOnly]
    eStage stage;
    public eStage GetStage() { return stage; }
    public void SetStage(eStage stage_) { 
        stage = stage_;
        if (stage <= eStage.none) stage = eStage.max - 1;
        else if (stage >= eStage.max) stage = eStage.none + 1;
    }
    public void AddStage() { 
        stage += 1;
        if (stage <= eStage.none) stage = eStage.max - 1;
        else if (stage >= eStage.max) stage = eStage.none + 1;
    }
    //GetSituation
    public bool GetGetSituation(eStage stage) { return GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.Stage, (int)stage); }
    public void SetGetSituation(int index, bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.Stage, index, getSituation_); }
    public void DataSave()
    {
        PlayerPrefs.SetInt("stage", (int)stage);
    }
    public void DataLoad()
    {
        stage = (eStage)PlayerPrefs.GetInt("stage");
    }

    //ゲートオープンの条件パターン
    eGateOpenType []gateOpenType = new eGateOpenType[(int)eStage.max];
    public eGateOpenType[] GetGateOpenType() {  return gateOpenType; }
    int []gateOpenNum = new int[(int)eStage.max];
    public int[] GetGateOpenNum() {  return gateOpenNum; }

    private void OnEnable()
    {
        DataLoad(); 
        StageEnemySelect();
        StageGatoOpenTypeSelect();
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}
    void StageEnemySelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            for (int enemy = 0; enemy < (int)eEnemyType.enemyTypeMax; enemy++)
            {
                bool tmp = false;
                switch ((eStage)stage)
                {
                    case eStage.fastPlay:
                        switch ((eEnemyType)enemy)
                        {
                            case eEnemyType.Bom: tmp = true; break;
                            case eEnemyType.Crow: break;
                            case eEnemyType.Golem: break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.crowStage:
                        switch ((eEnemyType)enemy)
                        {
                            case eEnemyType.Bom: break;
                            case eEnemyType.Crow: tmp = true; break;
                            case eEnemyType.Golem: break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.golemLabyrinth:
                        switch ((eEnemyType)enemy)
                        {
                            case eEnemyType.Bom: break;
                            case eEnemyType.Crow: break;
                            case eEnemyType.Golem: tmp = true; break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.lastGame:
                        switch ((eEnemyType)enemy)
                        {
                            case eEnemyType.Bom: tmp = true; break;
                            case eEnemyType.Crow: tmp = true; break;
                            case eEnemyType.Golem: tmp = true; break;
                            case eEnemyType.LivingArmor: tmp = true; break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    default: Debug.Log("StageEnemySelect : stageError"); break;
                }
                GetComponent<Manager_Enemy>().SetStageEnemy(stage, enemy, tmp);
            }
        }
    }
    void StageGatoOpenTypeSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:
                    gateOpenType[stage] = eGateOpenType.scoreCheck_Posi_Destroy_Bom;
                    gateOpenNum[stage] = 15;
                    break;
                case eStage.crowStage:
                    gateOpenType[stage] = eGateOpenType.time_Countdown;
                    gateOpenNum[stage] = 60;
                    break;
                case eStage.golemLabyrinth:
                    gateOpenType[stage] = eGateOpenType.none;
                    gateOpenNum[stage] = 30;
                    break;
                case eStage.lastGame:
                    gateOpenType[stage] = eGateOpenType.none;
                    gateOpenNum[stage] = 30;
                    break;
                default: Debug.Log("error : switch(eStage)"); break;
            }
        }
    }


    // Update is called once per frame
    //void Update()
    //{

    //}
}
