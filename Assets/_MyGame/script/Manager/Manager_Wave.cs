using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWaveType
{
    None,

    bom,            //next:bom‚ğˆê’è”shot‚Å“|‚·
    crow,           //next:crow‚ğˆê’è”ŒÄ‚Ô
    golem,          //next:golem‚ğˆê’è”’e‚­
    livingArmor,    //next:livingArmor‚ğ
    enemyMass,      //next:

    //              //‘«ê‚ğ‰ó‚µ‚Ä‰ñ‚é  ‹ó’†‚ğˆÚ“®‚·‚é     ‚¢‚­‚Â‚©‚ÌUŒ‚‚É‘Ï‚¦‚é
    //bossEnemy,    //ƒJƒEƒ“ƒg‚ÅendGame‚ğ”­“®   UŒ‚‚ÅƒJƒEƒ“ƒg‚ğ’x‚ç‚¹‚é    ‹ß‚Ã‚­‚Æ˜A‘±‚ÅUŒ‚‚Å‚«‚é‚Ì‚ÅƒJƒƒ‰‚Éû‚Ü‚é”ÍˆÍ‚Å‹——£‚ğæ‚è‰“‹——£UŒ‚‚·‚é  ‰“‹——£UŒ‚‚Í

    waveTypeMax
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
            if (waveType < eWaveType.waveTypeMax - 1)
            {
                waveTimer = 30;
                AddWave();
                timeManager.SetTimeStop(true);
            }
        }
    }

    void AddWave(int add = 1)
    {
        if (waveType >= eWaveType.waveTypeMax - 1) return;
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
