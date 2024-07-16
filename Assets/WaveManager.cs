using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWaveType
{
    None,

    bom,
    crow,
    golem,
    livingArmor,
    enemyMass,

    waveTypeMax
}
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    eWaveType waveType = eWaveType.bom;
    float waveTimer = 30;
    // Start is called before the first frame update
    void Start()
    {
        waveType = eWaveType.bom;
    }

    // Update is called once per frame
    void Update()
    {
        float time = GetComponent<TimeManager>().GetPlayTime();


        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
        }
        else
        {
            if (waveType < eWaveType.waveTypeMax - 1)
            {
                waveTimer = 30;
                waveType += 1;
            }
        }
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
