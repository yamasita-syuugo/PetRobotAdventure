using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWaveType
{
    None = -1,

    bom,            //next:bomを一定数shotで倒す
    crow,           //next:crowを一定数呼ぶ
    golem,          //next:golemを一定数弾く
    livingArmor,    //next:livingArmorを
    enemyMass,      //next:

    //              //足場を壊して回る  空中を移動する     いくつかの攻撃に耐える
    //bossEnemy,    //カウントでendGameを発動   攻撃でカウントを遅らせる    近づくと連続で攻撃できるのでカメラに収まる範囲で距離を取り遠距離攻撃する  遠距離攻撃は

    max
}
public class Manager_Wave : MonoBehaviour
{
    [SerializeField]
    eWaveType waveType = eWaveType.bom;
    float waveTimer = 30;
    Manager_Time timeManager;
    // Start is called before the first frame update
    void Start()
    {
        waveType = eWaveType.bom;
        timeManager = GetComponent<Manager_Time>();
    }

    [SerializeField]
    bool waveEnabled = false;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Manager_Time>().GetTimeStop()) return;
        float time = GetComponent<Manager_Time>().GetPlayTime();

        if (waveTimer > 0)
        {
            if(waveEnabled)waveTimer -= Time.deltaTime;
        }
        else
        {
            if (waveType < eWaveType.max - 1)
            {
                waveTimer = 30;
                AddWave();
                timeManager.SetTimeStop(true);
            }
        }
    }

    void AddWave(int add = 1)
    {
        if (waveType >= eWaveType.max - 1) return;
        waveType += add;
    }
    public eWaveType GetWaveType()
    {
        return waveType;
    }
    public float GetWaveTime()
    {
        return waveTimer;
    }

    public string GetWaveName()
    {
        switch (waveType)
        {
            case eWaveType.bom: return "BOM";
            case eWaveType.crow: return "CROW";
            case eWaveType.golem: return "GOLEM";
            case eWaveType.livingArmor: return "LIVING_ARMOR";
            case eWaveType.enemyMass: return "EnemyMass";
        }
        return "FREE";
    }
}
