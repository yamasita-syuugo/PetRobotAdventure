using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Wave : MonoBehaviour
{
    [SerializeField]
    eEnemyType waveType = eEnemyType.bom;
    float waveTimer = 30;
    Manager_Time timeManager;
    // Start is called before the first frame update
    void Start()
    {
        waveType = eEnemyType.bom;
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
            if (waveType < eEnemyType.max - 1)
            {
                waveTimer = 30;
                AddWave();
                timeManager.SetTimeStop(true);
            }
        }
    }

    void AddWave(int add = 1)
    {
        if (waveType >= eEnemyType.max - 1) return;
        waveType += add;
    }
    public eEnemyType GetWaveType()
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
            case eEnemyType.bom: return "BOM";
            case eEnemyType.crow: return "CROW";
            case eEnemyType.golem: return "GOLEM";
            case eEnemyType.livingArmor: return "LIVING_ARMOR";
            case eEnemyType.enemyMass: return "EnemyMass";
        }
        return "FREE";
    }
}
