using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public enum eStage
{
    //stage,        //正方形、ランダムで穴あけもできる
    //labyrinth,     //自動生成迷路
    //dungeon,       //部屋と通路を交互に自動作成、部屋で弾補給　通路を前進

    [InspectorName("")]none = -1,

    fastPlay,
    crowStage,
    bomRush,
    golemPush,
    iceLabyrinth,
    searchGate,
    dummyGate,
    bossStage,

    lastGame,

    test_混沌,

    [InspectorName("")]max,
}

public struct stStageData
{
    //敵の出現状況
    float []enemySerect;
    public void InitializeEnemySerect() { enemySerect = new float[(int)eEnemyType.max];}
    public float[] GetEnemySerect() {  return enemySerect; }
    public float GetEnemySerect(eEnemyType enemyType) {  return enemySerect[(int)enemyType]; }
    public void SetEnemySerect(eEnemyType enemyType,float enemySerect_) { enemySerect[(int)enemyType] = enemySerect_; }
    int []enemyStartSpaun;
    public void InitializeEnemyStartSpaun() { enemyStartSpaun = new int[(int)eEnemyType.max]; }
    public int[] GetEnemyStartSpaun() { return enemyStartSpaun; }
    public int GetEnemyStartSpaun(eEnemyType enemyType) { return enemyStartSpaun[(int)enemyType]; }
    public void SetEnemyStartSpaun(eEnemyType enemyType, int enemyStartSpaun_) { enemyStartSpaun[(int)enemyType] = enemyStartSpaun_; }
    float[] enemySpaunTimeReset;
    public void InitializeEnemySpaunTimeReset() { enemySpaunTimeReset = new float[(int)eEnemyType.max]; }
    public float[] GetEnemySpaunTimeReset() { return enemySpaunTimeReset; }
    public float GetEnemySpaunTimeReset(eEnemyType enemyType) { return enemySpaunTimeReset[(int)enemyType]; }
    public void SetEnemySpaunTimeReset(eEnemyType enemyType,float enemySpaunTimeReset_) {  enemySpaunTimeReset[(int)enemyType] = enemySpaunTimeReset_; }


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

    //エフェクト選択
    eEffectType effectType;
    public eEffectType GetEffectType() {  return effectType; }
    public void SetEffectType(eEffectType effectType_) {  effectType = effectType_; }

    //ゲート開錠条件
    eGateOpenType gateOpenType;
    public eGateOpenType GetGateOpenType() { return gateOpenType; }
    public void SetGateOpenType(eGateOpenType gateOpenType_) {  gateOpenType = gateOpenType_; }
    int gateOpenNum;
    public int GetGateOpenNum() {  return gateOpenNum; }
    public void SetGateOpenNum(int gateOpenNum_) { gateOpenNum = gateOpenNum_; }
    bool gatePosRandom;
    public bool GetGatePosRandom() {  return gatePosRandom; }
    public void SetGatePosRandom(bool gatePosRandom_) {  gatePosRandom = gatePosRandom_; }

    //背景選択
    eBackGroundType backGroundIndex;
    public eBackGroundType GetBackGroundIndex() {  return backGroundIndex; }
    public void SetBackGroundIndex(eBackGroundType backGroundIndex_) { 
        backGroundIndex = backGroundIndex_;
        eBackGroundType backGroundNum = eBackGroundType.max;
        if (backGroundIndex < 0) backGroundIndex = 0; else if (backGroundIndex >= backGroundNum) backGroundIndex = backGroundNum - 1;
    }

    //音楽選択
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
    Manager_PlayerController manager_PlayerController;
    Manager_Collection manager_Collection;

    //ステージの選択
    [SerializeField, ReadOnly]
    eStage stage;
    public eStage GetStage() { return stage; }
    public void SetStage(eStage stage_)
    {
        stage = stage_;
        if (stage <= eStage.none) stage = eStage.max - 1;
        else if (stage >= eStage.max) stage = eStage.none + 1;

        for (; (stage) > 0; stage--) { if (manager_Collection.GetGetSituation(eCollectionType.stage, (int)stage)) break; else continue; }
    }
    public void AddStage(int num = 1)
    {
        stage += num;
        if (stage <= eStage.none) stage = eStage.none + 1;
        else if (stage >= eStage.max) stage = eStage.max - 1;

        for (; (stage) > 0; stage--) { if (manager_Collection.GetGetSituation(eCollectionType.stage, (int)stage)) break; else continue; }
    }

    [SerializeField] bool randomStage = false;
    public bool GetRandomStage() { return randomStage; }
    public void SetRandomStage(bool randomStage_) { randomStage = randomStage_; }
    public void RandomStageChange() { randomStage = !randomStage; }

    [SerializeField] bool musicSerect = false;
    public bool GetMusicSerect() { return musicSerect; }
    public void SetMusicSerect(bool musicSerect_) { musicSerect = musicSerect_; }
    public void MusicSerectChenge() { musicSerect = !musicSerect; }

    [SerializeField] bool backGroundSerect = false;
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

        //if (randomStage) stage = (eStage)UnityEngine.Random.Range(0, (int)eStage.max - 1);//todo::ランダムステージをステージ選択から要素全てランダムに変える
    }

    stStageData[] stageData = new stStageData[(int)eStage.max];
    public stStageData GetStageData(eStage stage) { return stageData[(int)stage]; }
    stStageData randomStageData = new stStageData();
    public stStageData GetRandomStageData() { return randomStageData; }

    //ゲートオープンの条件パターン
    public eGateOpenType GetGateOpenType(eStage stage) { return stageData[(int)stage].GetGateOpenType(); }
    public int GetGateOpenNum(eStage stage) { return stageData[(int)stage].GetGateOpenNum(); }

    private void OnEnable()
    {
        manager_PlayerController = GetComponent<Manager_PlayerController>();
        manager_Collection = GetComponent<Manager_Collection>();

        StageScaffoldSelect();
        StageGatoOpenTypeSelect();
        StageEnemySelect();
        StageEffectSelect();

        StageBackGroundSelect();
        StageMusicSelect();
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}


    void StageScaffoldSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            stageData[stage].SetGatePosRandom(false);
            switch ((eStage)stage)//labyrinthのsizeは奇数
            {
                case eStage.fastPlay:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(7);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.blockOnly);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(5);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.grassOnly);
                    stageData[stage].SetRandomScaffoldBreak(0.0f);
                    break;
                case eStage.golemPush:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(14);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.blockOnly);
                    stageData[stage].SetRandomScaffoldBreak(60);
                    stageData[stage].SetGatePosRandom(true);
                    break;
                case eStage.iceLabyrinth:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.labyrinth);
                    stageData[stage].SetFieldSize(10);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.iceOnly);
                    stageData[stage].SetRandomScaffoldBreak(10.0f);
                    break;
                case eStage.dummyGate:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.frameStage);
                    stageData[stage].SetFieldSize(56);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.blockOnly);
                    stageData[stage].SetHoleSize(4);
                    stageData[stage].SetRandomScaffoldBreak(20);
                    break;
                case eStage.bomRush:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.stage);
                    stageData[stage].SetFieldSize(4);
                    stageData[stage].SetCreatScaffoldType(eCreatScaffoldType.blockOnly);
                    stageData[stage].SetRandomScaffoldBreak(75.0f);
                    break;
                case eStage.searchGate:
                    stageData[stage].SetFieldCreatTypeIndex(eFieldCreatType.frameStage);
                    stageData[stage].SetFieldSize(56);
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

                default: Debug.Log("StageScaffoldSelect : " + ((eStage)stage).ToString()); break;
            }
        }

        eFieldCreatType fieldType = (eFieldCreatType)UnityEngine.Random.Range(0, (int)eFieldCreatType.max - 1);
        randomStageData.SetFieldCreatTypeIndex(fieldType);
        eCreatScaffoldType creatScaffoldType = (eCreatScaffoldType)UnityEngine.Random.Range(0, (int)eCreatScaffoldType.max - 1);
        randomStageData.SetCreatScaffoldType(creatScaffoldType);
        switch (fieldType)
        {
            case eFieldCreatType.stage:
                randomStageData.SetFieldSize(UnityEngine.Random.Range(3, 20));
                randomStageData.SetRandomScaffoldBreak(UnityEngine.Random.Range(0, 50)); break;//0:穴なし　100:全損
            case eFieldCreatType.labyrinth:
                randomStageData.SetFieldSize(UnityEngine.Random.Range(7, 20));
                randomStageData.SetRandomScaffoldBreak(UnityEngine.Random.Range(0, 10)); break;
            case eFieldCreatType.frameStage:
                randomStageData.SetFieldSize(UnityEngine.Random.Range(25, 100));
                randomStageData.SetHoleSize(UnityEngine.Random.Range(2, 6));
                randomStageData.SetRandomScaffoldBreak(UnityEngine.Random.Range(0, 30)); break;
            case eFieldCreatType.bossStage:
                int stageSize = UnityEngine.Random.Range(7, 12);
                randomStageData.SetFieldSize(stageSize);
                randomStageData.SetHoleSize(stageSize - UnityEngine.Random.Range(1, 4));
                randomStageData.SetRandomScaffoldBreak(UnityEngine.Random.Range(0, 50)); break;

        }
        //Debug.Log("randomField : " + fieldType.HumanName() +"; scaffoldType : " + creatScaffoldType.HumanName() + "; fieldSize : " + 
        //    randomStageData.GetFieldSize() + "; holeSize : " + randomStageData.GetHoleSize() + "; randomBreak : " + randomStageData.GetRandomScaffoldBreak());
    }
    void StageGatoOpenTypeSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:
                    stageData[stage].SetGateOpenType(eGateOpenType.scoreCheck_Posi_Destroy_ + (int)eEnemyType.bom);
                    stageData[stage].SetGateOpenNum(10);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(20);
                    break;
                case eStage.golemPush:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(30);
                    break;
                case eStage.iceLabyrinth:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                case eStage.dummyGate:
                    stageData[stage].SetGateOpenType(eGateOpenType.scoreCheck_Posi_Destroy_ + (int)eEnemyType.bom);//ゲートを一定数くぐる
                    stageData[stage].SetGateOpenNum(7);
                    break;
                case eStage.bomRush:
                    stageData[stage].SetGateOpenType(eGateOpenType.scoreCheck_Posi_Destroy_ + (int)eEnemyType.bom);
                    stageData[stage].SetGateOpenNum(15);
                    break;
                case eStage.searchGate:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                case eStage.bossStage:
                    stageData[stage].SetGateOpenType(eGateOpenType.scoreCheck_Posi_Destroy_ + (int)eEnemyType.bom);
                    stageData[stage].SetGateOpenNum(25);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(5);
                    break;
                case eStage.test_混沌:
                    stageData[stage].SetGateOpenType(eGateOpenType.time_Countdown);
                    stageData[stage].SetGateOpenNum(60);
                    break;
                default: Debug.Log("StageGatoOpenTypeSelect : " + ((eStage)stage).ToString()); break;
            }
        }


        int openType = UnityEngine.Random.Range(0, 1);
        switch (openType)
        {
            case 0: randomStageData.SetGateOpenType(eGateOpenType.time_Countdown); break;
            case 1: randomStageData.SetGateOpenType(eGateOpenType.scoreCheck_Posi_Destroy_ + UnityEngine.Random.Range(0, (int)eEnemyType.max - 1)); break;
        }
        int[] openNum = new int[2];
        switch (stageData[(int)GetStage()].GetFieldCreatTypeIndex())
        {
            case eFieldCreatType.stage://0 = time; 1 = enemy;
                switch (openType) { case 0: openNum[0] = 20; openNum[1] = 30; break; case 1: openNum[0] = 5; openNum[1] = 20; break; }
                break;
            case eFieldCreatType.labyrinth:
                switch (openType) { case 0: openNum[0] = 5; openNum[1] = 40; break; case 1: openNum[0] = 5; openNum[1] = 20; break; }
                break;
            case eFieldCreatType.frameStage:
                switch (openType) { case 0: openNum[0] = 5; openNum[1] = 30; break; case 1: openNum[0] = 15; openNum[1] = 60; break; }
                break;
            case eFieldCreatType.bossStage:
                switch (openType) { case 0: openNum[0] = 20; openNum[1] = 30; break; case 1: openNum[0] = 10; openNum[1] = 30; break; }
                break;
        }
        randomStageData.SetGateOpenNum(UnityEngine.Random.Range(openNum[0], openNum[1]));

    }
    void StageEnemySelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            stageData[stage].InitializeEnemySerect();
            stageData[stage].InitializeEnemyStartSpaun();
            stageData[stage].InitializeEnemySpaunTimeReset();
            for (eEnemyType enemy = 0; enemy < eEnemyType.max; enemy++)
            {
                int enemyStartSpawn = -1;
                float spawnStratTime = -1;
                float spawnTime = -1;
                switch ((eStage)stage)
                {
                    case eStage.fastPlay:
                        switch (enemy)
                        {
                            case eEnemyType.bom: spawnStratTime = 1; break;
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
                            case eEnemyType.crow: spawnStratTime = 1; break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.golemPush:
                        switch (enemy)
                        {
                            case eEnemyType.bom:spawnStratTime = 1;spawnTime = 2;  break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: spawnStratTime = 1;spawnTime = 1; enemyStartSpawn = 7; break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.iceLabyrinth:
                        switch (enemy)
                        {
                            case eEnemyType.bom: spawnStratTime = 1; enemyStartSpawn = 5; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.dummyGate:
                        switch (enemy)
                        {
                            case eEnemyType.bom: spawnStratTime = 1; break;
                            case eEnemyType.crow: spawnStratTime = 1; spawnTime = 3f; break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                            case eEnemyType.fakeGate: spawnStratTime = 100; spawnTime = 10; enemyStartSpawn = 80; break;
                        }
                        break;
                    case eStage.bomRush:
                        switch (enemy)
                        {
                            case eEnemyType.bom: spawnStratTime = 1; spawnTime = 1.5f; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.searchGate:
                        switch (enemy)
                        {
                            case eEnemyType.bom: spawnStratTime = 1; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: spawnStratTime = 1; break;
                            case eEnemyType.livingArmor: spawnStratTime = 1; break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.bossStage:
                        switch (enemy)
                        {
                            case eEnemyType.bom: spawnStratTime = 1; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.lastGame:
                        switch (enemy)
                        {
                            case eEnemyType.bom: spawnStratTime = 1; break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    case eStage.test_混沌:
                        switch (enemy)
                        {
                            case eEnemyType.bom: break;
                            case eEnemyType.crow: break;
                            case eEnemyType.golem: break;
                            case eEnemyType.livingArmor: break;
                            case eEnemyType.enemyMass: break;
                        }
                        break;
                    default: Debug.Log("StageEnemySelect : " + ((eStage)stage).ToString()); break;
                }
                stageData[stage].SetEnemySerect(enemy, spawnStratTime);
                stageData[stage].SetEnemyStartSpaun(enemy, enemyStartSpawn);
                stageData[stage].SetEnemySpaunTimeReset(enemy, spawnTime);
            }
        }

        {
            randomStageData.InitializeEnemySerect();
            randomStageData.InitializeEnemyStartSpaun();
            randomStageData.InitializeEnemySpaunTimeReset();

            for (eEnemyType enemy = 0; enemy < eEnemyType.max; enemy++)
            {
                int enemyStartSpawn = -1;//ゲームスタート時、値の数だけ出現
                float spawnStratTime = -1;//スポーン開始までのラグ、ここが0未満で出てこない
                float spawnTime = -1;//スポーン間隔
                switch (enemy)
                {
                    case eEnemyType.bom: enemyStartSpawn = UnityEngine.Random.Range(0, 7); spawnStratTime = UnityEngine.Random.Range(-1, 0); spawnTime = 10; break;
                    case eEnemyType.crow: break;
                    case eEnemyType.golem: break;
                    case eEnemyType.livingArmor: break;
                    case eEnemyType.enemyMass: break;
                }
                randomStageData.SetEnemyStartSpaun(enemy, enemyStartSpawn);
                randomStageData.SetEnemySerect(enemy, spawnStratTime);
                randomStageData.SetEnemySpaunTimeReset(enemy, spawnTime);
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
                case eStage.golemPush: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.iceLabyrinth: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.dummyGate: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.bomRush: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.searchGate: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.bossStage: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.lastGame: stageData[stage].SetEffectType(eEffectType.cloud); break;
                case eStage.test_混沌: stageData[stage].SetEffectType(eEffectType.none); break;
                default: Debug.Log("StageEffectSelect : " + ((eStage)stage).ToString()); break;
            }
        }

        randomStageData.SetEffectType((eEffectType)UnityEngine.Random.Range(0, (int)eEffectType.max - 1)); 
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
                    stageData[stage].SetBackGroundIndex(eBackGroundType.sea);
                    break;
                case eStage.golemPush:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.sea);
                    break;
                case eStage.iceLabyrinth:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.forest);
                    break;
                case eStage.dummyGate:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.forest);
                    break;
                case eStage.bomRush:
                    stageData[stage].SetBackGroundIndex(eBackGroundType.forest);
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
                default: Debug.Log("StageBackGroundSelect : " + ((eStage)stage).ToString()); break;
            }
        }

        randomStageData.SetBackGroundIndex((eBackGroundType)UnityEngine.Random.Range(0,(int)eBackGroundType.max - 1));
        if (SceneManager.GetActiveScene().name == "Result") PlayerPrefs.SetInt("ResultBackGroundIndex", (int)randomStageData.GetBackGroundIndex());
        randomStageData.SetBackGroundIndex((eBackGroundType)PlayerPrefs.GetInt("ResultBackGroundIndex"));
    }
    void StageMusicSelect()
    {
        for (int stage = 0; stage < (int)eStage.max; stage++)
        {
            switch ((eStage)stage)
            {
                case eStage.fastPlay:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.crowStage:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.golemPush:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.iceLabyrinth:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.dummyGate:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.bomRush:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.searchGate:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.bossStage:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.lastGame:
                    stageData[stage].SetMusicIndex(1);
                    break;
                case eStage.test_混沌:
                    stageData[stage].SetMusicIndex(0);
                    break;
                default: Debug.Log("StageMusicSelect : " + ((eStage)stage).ToString()); break;
            }
        }

        randomStageData.SetMusicIndex(UnityEngine.Random.Range(0, GameObject.FindWithTag("Manager").GetComponent<Manager_Music>().GetMusicBase().Length));
    }

    // Update is called once per frame
    void Update()
    {
        Special();

        JoystickSelect();
    }

    int count = (int)eEnemyType.bom;
    private void Special()
    {
        if (stage == eStage.lastGame) if (count < (int)GetComponent<Manager_Time>().GetPlayTime() / 30) { count++; stageData[(int)eStage.lastGame].SetEnemySerect((eEnemyType)count, 1); }
    }

    void JoystickSelect() { 
        if(SceneManager.GetActiveScene().name == "Title") {
            if (manager_PlayerController.JoystickButtonDown(eJoystickButton.L1)) AddStage(-1);
            if (manager_PlayerController.JoystickButtonDown(eJoystickButton.R1)) AddStage(1);
            if (manager_PlayerController.JoystickButtonDown(eJoystickButton.L2)) SetStage(eStage.none + 1);
            if (manager_PlayerController.JoystickButtonDown(eJoystickButton.R2)) SetStage(eStage.max - 1);
        }
    }
}
