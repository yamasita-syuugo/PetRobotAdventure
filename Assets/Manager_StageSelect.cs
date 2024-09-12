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
    eScaffoldType []scaffoldType = new eScaffoldType [(int)eStage.eStageMax];
    public eScaffoldType[] GetScaffoldType() { return scaffoldType; }
    float []randomBreak = new float[(int)eStage.eStageMax];
    public float[] GetRandomBreak() {  return randomBreak; }

    private void OnEnable()
    {
        DataLoad(); 
        StageEnemySelect();
        StageScaffoldSelect();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
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
                    scaffoldType[stage] = eScaffoldType.block;
                    randomBreak[stage] = 0.0f;
                    break;
                case eStage.golemOnly:
                    scaffoldType[stage] = eScaffoldType.block;
                    randomBreak[stage] = 50.0f;
                    break;
                case eStage.lastGame:
                    scaffoldType[stage] = eScaffoldType.block;
                    randomBreak[stage] = 0.0f;
                    break;
            }
        }
    }


    // Update is called once per frame
    //void Update()
    //{

    //}
    public void DataSave() { PlayerPrefs.SetInt("stage", (int)stage); }
    public void DataLoad() { stage = (eStage)PlayerPrefs.GetInt("stage"); }
}
