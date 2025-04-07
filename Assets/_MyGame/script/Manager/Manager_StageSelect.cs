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
    //labyrinth,     //自動生成迷路
    //dungeon,       //部屋と通路を交互に自動作成、部屋で弾補給　通路を前進

    [InspectorName("")]none = -1,

    fastPlay,
    crowStage,
    golemLabyrinth,
    iceBom,
    searchGate,
    bossStage,

    lastGame,

    test_混沌,

    [InspectorName("")]max,
}

public struct stStageData
{
    bool []enemySerect;
    public void InitializeEnemySerect() { enemySerect = new bool[(int)eEnemyType.max];}
    public bool[] GetEnemySerect() {  return enemySerect; }
    public bool GetEnemySerect(eEnemyType enemyType) {  return enemySerect[(int)enemyType]; }
    public void SetEnemySerect(eEnemyType enemyType,bool enemySerect_) { enemySerect[(int)enemyType] = enemySerect_; }


    //足場の配置パターン
    eCreatScaffoldType creatScaffoldType;
    public eCreatScaffoldType GetCreatScaffoldType() {  return creatScaffoldType; }
    public void SetCreatScaffoldType(eCreatScaffoldType creatScaffoldType_) {  creatScaffoldType = creatScaffoldType_; }
    float randomScaffoldBreak;
    public float GetRandomScaffoldBreak() {  return randomScaffoldBreak; }
    public void SetRandomScaffoldBreak(float randomScaffoldBreak_) {  randomScaffoldBreak = randomScaffoldBreak_; }
    int holeSize;
    public int GetHoleSize() {  return holeSize; }
    public void SetHoleSize(int holeSize_) {  holeSize = holeSize_; }
    eFieldCreatType fieldCreatTypeIndex;
    public eFieldCreatType GetFieldCreatTypeIndex() {  return fieldCreatTypeIndex; }
    public void SetFieldCreatTypeIndex(eFieldCreatType fieldCreatTypeIndex_) {   fieldCreatTypeIndex = fieldCreatTypeIndex_; }
    int fieldSize;
    public int GetFieldSize() { return fieldSize; }
    public void SetFieldSize(int fieldSize_) {  fieldSize = fieldSize_; }

    eEffectType effectType;
    public eEffectType GetEffectType() {  return effectType; }
    public void SetEffectType(eEffectType effectType_) {  effectType = effectType_; }

    eGateOpenType gateOpenType;
    public eGateOpenType GetGateOpenType() { return gateOpenType; }
    public void SetGateOpenType(eGateOpenType gateOpenType_) {  gateOpenType = gateOpenType_; }
    int gateOpenNum;
    public int GetGateOpenNum() {  return gateOpenNum; }
    public void SetGateOpenNum(int gateOpenNum_) { gateOpenNum = gateOpenNum_; }

    eBackGroundType backGroundIndex;
    public eBackGroundType GetBackGroundIndex() {  return backGroundIndex; }
    public void SetBackGroundIndex(eBackGroundType backGroundIndex_) { 
        backGroundIndex = backGroundIndex_;
        eBackGroundType backGroundNum = eBackGroundType.max;
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
    public bool GetMusicSerect() { return musicSerect; }
    public void SetMusicSerect(bool musicSerect_) { musicSerect = musicSerect_; }
    public void MusicSerectChenge() { musicSerect = !musicSerect; }

    bool backGroundSerect = false;
    public bool GetBackGroundSerect() { return backGroundSerect; }
    public void SetBackGroundSerect(bool backGroundSerect_) { backGroundSerect = backGroundSerect_; }
    public void BackGroundSerectChange() { backGroundSerect = !backGroundSerect; }

    //GetSituation
    public bool GetGetSituation(eStage stage) { return GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.stage, (int)stage); }
    public void SetGetSituation(int index, bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.stage, index, getSituation_); }
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
        StageEffectSelect();
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
                            case eEnemyType.bom: tmp = true; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.crowStage:
                        switch (enemy)
                        {
                            case eEnemyType.bom: break;
                            case eEnemyType.crow: tmp = true; break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.golemLabyrinth:
                        switch (enemy)
                        {
                            case eEnemyType.bom: break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: tmp = true; break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.iceBom:
                        switch (enemy)
                        {
                            case eEnemyType.bom: tmp = true; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.searchGate:
                        switch (enemy)
                        {
                            case eEnemyType.bom: tmp = true; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: tmp = true; break;
                            case eEnemyType.livingArmor: tmp = true; break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.bossStage:
                        switch (enemy)
                        {
                            case eEnemyType.bom: tmp = true; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor:  break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.lastGame:
                        switch (enemy)
                        {
                            case eEnemyType.bom: tmp = true; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.test_混沌:
                        switch (enemy)
                        {
                            case eEnemyType.bom: tmp = true; break;
                            case eEnemyType.crow: tmp = true; break;
                            case eEnemyType.golem:  break;
                            case eEnemyType.livingArmor:  break;
                            case eEnemyType.enemyMass:  break;
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
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.blockOnly);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(3);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.grassOnly);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;
                case eStage.golemLabyrinth:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.labyrinth);
                    stageData[stage].SetFieldSize(9);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.movePanelOnly);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;
                case eStage.iceBom:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(5);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.iceOnly);
                    stageData[stage].SetRandomScaffoldBreak(50.0f);
                    break;
                case eStage.searchGate:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.frameStage);
                    stageData[stage].SetFieldSize(53);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.movePanelOnly);//todo:grassとBlockのランダムを作成し充てる
                    stageData[stage].SetHoleSize(4);
                    stageData[stage].SetRandomScaffoldBreak(4);
                    break;
                case eStage.bossStage:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.bossStage);
                    stageData[stage].SetFieldSize(15);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.blockOnly);
                    stageData[stage].SetHoleSize(11);
                    //stageData[stage].SetRandomScaffoldBreak(4);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(9);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.random);
                    stageData[stage].SetRandomScaffoldBreak(50.0f);
                    break;
                    
                case eStage.test_混沌:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(9);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.blockOnly);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;

                default: Debug.Log("StageScaffoldSelect : " + ((eStage)stage).HumanName()); break;
            }
        }
    }

    void StageEffectSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.crowStage: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.golemLabyrinth: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.iceBom: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.searchGate: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.bossStage: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.lastGame: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.test_混沌: stageData[stage].SetEffectType(eEffectType.none); break;
                default: Debug.Log("StageEffectSelect : " + ((eStage)stage).HumanName()); break;
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
                case eStage.iceBom:
                    stageData[stage].SetGateOpenType(eGateOpenType.scoreCheck_Posi_Destroy_Bom);
                    stageData[stage].SetGateOpenNum(20);
                    break;
                case eStage.searchGate:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                case eStage.bossStage:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                case eStage.test_混沌:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(60);
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
                    stageData[stage].SetBackGroundIndex(eBackGroundType.sea);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.forest);
                    break;
                case eStage.golemLabyrinth:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.sea);
                    break;
                case eStage.iceBom:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.sea);
                    break;
                case eStage.searchGate:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.sea);
                    break;
                case eStage.bossStage:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.forest);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.sea);
                    break;
                case eStage.test_混沌:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.forest);
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
                case eStage.iceBom:
                    stageData[stage].SetMusicIndex(3);
                    break;
                case eStage.searchGate:
                    stageData[stage].SetMusicIndex(3);
                    break;
                case eStage.bossStage:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetMusicIndex(4);
                    break;
                case eStage.test_混沌:
                    stageData[stage].SetMusicIndex(0);
                    break;
                default: Debug.Log("StageMusicSelect : " + ((eStage)stage).HumanName()); break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Special();
    }

    int count = (int)eEnemyType.bom;
    private void Special()
    {
        if (stage == eStage.lastGame) if (count < (int)GetComponent<Manager_Time>().GetPlayTime() / 30) { count++; stageData[(int)eStage.lastGame].SetEnemySerect((eEnemyType)count, true); }
    }
}
