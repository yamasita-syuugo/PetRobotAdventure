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

    [SerializeField]
    bool[,] stageEnemy = new bool[(int)eStage.eStageMax,(int)eEnemyType.enemyTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    public void DataSave() { PlayerPrefs.SetInt("stage", (int)stage); }
    public void DataLoad() { stage = (eStage)PlayerPrefs.GetInt("stage"); }
}
