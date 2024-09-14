using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    eStage stage;
    public eStage GetStage() { return stage; }

    bool[,] stageEnemy = new bool[(int)eStage.eStageMax,(int)eEnemyType.enemyTypeMax];
    public bool[,] GetStageEnemy() {  return stageEnemy; }
    eCreatType [] creatType = new eCreatType[(int)eStage.eStageMax];
    public eCreatType[] GetScaffoldType() { return creatType; }
    float []randomBreak = new float[(int)eStage.eStageMax];
    public float[] GetRandomBreak() {  return randomBreak; }

    eBackGroundType[] backGroundTypes = new eBackGroundType[(int)eStage.eStageMax];
    public eBackGroundType[] GetBackGroundTypes() { return backGroundTypes; }

    private void OnEnable()
    {
        DataLoad(); 
        StageEnemySelect();
        StageScaffoldSelect();
        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetCreatType(creatType[(int)stage]);
        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(randomBreak[(int)stage]);
        StageBackGroundSelect();
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


    // Update is called once per frame
    void Update()
    {
        {
            GameObject tmp = GameObject.Find("Serect_Steag");
            if (tmp == null) return; 
            eStage stage_ = tmp.GetComponent<Select_Stage>().GetStage();
            if (stage != stage_)
            {
                stage = stage_;
                GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetCreatType(creatType[(int)stage]);
                GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(randomBreak[(int)stage]);
            }
        }
    }
    public void DataSave() { PlayerPrefs.SetInt("stage", (int)stage); }
    public void DataLoad() { stage = (eStage)PlayerPrefs.GetInt("stage"); }
}
