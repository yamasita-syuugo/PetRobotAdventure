using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum eStage
{
    //stage,        //正方形、ランダムで穴あけもできる
    //dungeon,       //部屋と通路を交互に自動作成、部屋で弾補給　通路を前進
    //labyrinth,     //自動生成迷路

    [InspectorName("")]none = -1,

    fastPlay,
    crowStage,
    golemLabyrinth,

    lastGame,

    [InspectorName("")]max,
}

public struct stStageData
{
    bool []enemySerect;
    public void InitializeEnemySerect() { enemySerect = new bool[(int)eEnemyType.max];}
    public bool[] GetEnemySerect() {  return enemySerect; }
    public void SetEnemySerect(eEnemyType enemyType,bool enemySerect_) { enemySerect[(int)enemyType] = enemySerect_; }


    //足場の配置パターン
    eCreatScaffoldType creatScaffoldType;
    public eCreatScaffoldType GetCreatScaffoldType() {  return creatScaffoldType; }
    public void SetCreatScaffoldType(eCreatScaffoldType creatScaffoldType_) {  creatScaffoldType = creatScaffoldType_; }
    float randomScaffoldBreak;
    public float GetRandomScaffoldBreak() {  return randomScaffoldBreak; }
    public void SetRandomScaffoldBreak(float randomScaffoldBreak_) {  randomScaffoldBreak = randomScaffoldBreak_; }
    eFieldCreatType fieldCreatTypeIndex;
    public eFieldCreatType GetFieldCreatTypeIndex() {  return fieldCreatTypeIndex; }
    public void SetFieldCreatTypeIndex(eFieldCreatType fieldCreatTypeIndex_) {   fieldCreatTypeIndex = fieldCreatTypeIndex_; }
    int fieldSize;
    public int GetFieldSize() { return fieldSize; }
    public void SetFieldSize(int fieldSize_) {  fieldSize = fieldSize_; }

    eGateOpenType gateOpenType;
    public eGateOpenType GetGateOpenType() { return gateOpenType; }
    public void SetGateOpenType(eGateOpenType gateOpenType_) {  gateOpenType = gateOpenType_; }
    int gateOpenNum;
    public int GetGateOpenNum() {  return gateOpenNum; }
    public void SetGateOpenNum(int gateOpenNum_) { gateOpenNum = gateOpenNum_; }

    int backGroundIndex;
    public int GetBackGroundIndex() {  return backGroundIndex; }
    public void SetBackGroundIndex(int backGroundIndex_) { 
        backGroundIndex = backGroundIndex_;
        int backGroundNum = GameObject.FindWithTag("Manager").GetComponent<Manager_BackgroundType>().GetBackGroundBase().Length;
        if (backGroundIndex < 0) backGroundIndex = 0; else if (backGroundIndex >= backGroundNum) backGroundIndex = backGroundNum - 1;
    }

    int MusicIndex;
    public int GetMusicIndex() {  return MusicIndex; }
    public void SetMusicIndex(int MusicIndex_) {
        MusicIndex = MusicIndex_;
        int MusicNum = GameObject.FindWithTag("Manager").GetComponent<Manager_Music>().GetMusicBase().Length;
        if (MusicIndex < 0) MusicIndex = 0; else if (MusicIndex >= MusicNum) MusicIndex = MusicNum - 1;
    }
}

public class Manager_StageSelect : MonoBehaviour
{
    //ステージの選択
    [SerializeField, ReadOnly]
    eStage stage;
    public eStage GetStage() { return stage; }
    public void SetStage(eStage stage_)
    {
        stage = stage_;
        if (stage <= eStage.none) stage = eStage.max - 1;
        else if (stage >= eStage.max) stage = eStage.none + 1;
    }
    public void AddStage()
    {
        stage += 1;
        if (stage <= eStage.none) stage = eStage.max - 1;
        else if (stage >= eStage.max) stage = eStage.none + 1;
    }

    bool randomStage = false;
    public bool GetRandomStage() { return randomStage; }
    public void SetRandomStage(bool randomStage_) { randomStage = randomStage_; }
    public void RandomStageChange() { randomStage = !randomStage; }

    bool musicSerect = false;
    public bool GetMusicSerect() {  return musicSerect; }
    public void SetMusicSerect(bool musicSerect_) { musicSerect = musicSerect_; }
    public void MusicSerectChenge() { musicSerect = !musicSerect; }

    bool backGroundSerect = false;
    public bool GetBackGroundSerect() {  return backGroundSerect; }
    public void SetBackGroundSerect(bool backGroundSerect_) {  backGroundSerect = backGroundSerect_; }
    public void BackGroundSerectChange() { backGroundSerect = !backGroundSerect; }

    //GetSituation
    public bool GetGetSituation(eStage stage) { return GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.Stage, (int)stage); }
    public void SetGetSituation(int index, bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.Stage, index, getSituation_); }
    public void DataSave()
    {
        PlayerPrefs.SetInt("stage", (int)stage);

        bool[] customData = { randomStage, musicSerect, backGroundSerect, };
        Manager_Save.BoolSave("customData", customData.Length, customData);
    }
    public void DataLoad()
    {
        stage = (eStage)PlayerPrefs.GetInt("stage");

        bool[] customData;
        Manager_Save.BoolLoad("customData", 3, out customData);
        randomStage = customData[0];
        musicSerect = customData[1];
        backGroundSerect = customData[2];

        if (randomStage) stage = (eStage)UnityEngine.Random.Range(0, (int)eStage.max - 1);
    }

    stStageData[] stageData = new stStageData[(int)eStage.max];
    public stStageData GetStageData(eStage stage) { return stageData[(int)stage]; }

    //ゲートオープンの条件パターン
    public eGateOpenType GetGateOpenType(eStage stage) { return stageData[(int)stage].GetGateOpenType(); }
    public int GetGateOpenNum(eStage stage) { return stageData[(int)stage].GetGateOpenNum(); }

    private void OnEnable()
    {
        StageEnemySelect();
        StageScaffoldSelect();
        StageGatoOpenTypeSelect();
        StageBackGroundSelect();
        StageMusicSelect();
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}
    void StageEnemySelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            stageData[stage].InitializeEnemySerect();
            for (eEnemyType enemy = 0; enemy < eEnemyType.max; enemy++)
            {
                bool tmp = false;
                switch ((eStage)stage)
                {
                    case eStage.fastPlay:
                        switch (enemy)
                        {
                            case eEnemyType.Bom: tmp = true; break;
                            case eEnemyType.Crow: break;
                            case eEnemyType.Golem: break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.crowStage:
                        switch (enemy)
                        {
                            case eEnemyType.Bom: break;
                            case eEnemyType.Crow: tmp = true; break;
                            case eEnemyType.Golem: break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.golemLabyrinth:
                        switch (enemy)
                        {
                            case eEnemyType.Bom: break;
                            case eEnemyType.Crow: break;
                            case eEnemyType.Golem: tmp = true; break;
                            case eEnemyType.LivingArmor: break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    case eStage.lastGame:
                        switch (enemy)
                        {
                            case eEnemyType.Bom: tmp = true; break;
                            case eEnemyType.Crow: tmp = true; break;
                            case eEnemyType.Golem: tmp = true; break;
                            case eEnemyType.LivingArmor: tmp = true; break;
                            case eEnemyType.EnemyMass: break;
                        }
                        break;
                    default: Debug.Log("StageEnemySelect : " + ((eStage)stage).HumanName()); break;
                }
                stageData[stage].SetEnemySerect(enemy, tmp);
            }
        }
    }
    void StageScaffoldSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(7);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.block);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(3);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.block);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;
                case eStage.golemLabyrinth:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.labyrinth);
                    stageData[stage].SetFieldSize(9);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.movePanel);
                    stageData[stage].SetRandomScaffoldBreak(50.0f);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(9);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.random);
                    stageData[stage].SetRandomScaffoldBreak(50.0f);
                    break;
                default: Debug.Log("StageScaffoldSelect : " + ((eStage)stage).HumanName()); break;
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
                    stageData[stage].SetGateOpenType(eGateOpenType.scoreCheck_Posi_Destroy_Bom);
                    stageData[stage].SetGateOpenNum(15);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(60);
                    break;
                case eStage.golemLabyrinth:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                default: Debug.Log("StageGatoOpenTypeSelect : " + ((eStage)stage).HumanName()); break;
            }
        }
    }
    void StageBackGroundSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:
                    stageData[stage].SetBackGroundIndex(0);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetBackGroundIndex(1);
                    break;
                case eStage.golemLabyrinth:
                    stageData[stage].SetBackGroundIndex(2);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetBackGroundIndex(3);
                    break;
                default: Debug.Log("StageBackGroundSelect : " + ((eStage)stage).HumanName()); break;
            }
        }
    }
    void StageMusicSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:
                    stageData[stage].SetMusicIndex(0);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.golemLabyrinth:
                    stageData[stage].SetMusicIndex(2);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetMusicIndex(3);
                    break;
                default: Debug.Log("StageMusicSelect : " + ((eStage)stage).HumanName()); break;
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
