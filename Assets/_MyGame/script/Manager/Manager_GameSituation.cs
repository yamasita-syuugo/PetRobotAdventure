using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameSituation
{
    None,

    clear,
    failure,

    GameSituationMax,
}
public class Manager_GameSituation : MonoBehaviour
{
    eGameSituation gameSituation = eGameSituation.None;
    public void SetGameSituation(eGameSituation gameSituation_) { gameSituation = gameSituation_; }
    public eGameSituation GetGameSituation() { return gameSituation; }
    public void DataSave() { PlayerPrefs.SetInt("GameSituation", (int)gameSituation); }
    public void DataLoad(){ gameSituation = (eGameSituation)PlayerPrefs.GetInt("GameSituation");PlayerPrefs.SetInt("GameSituation",(int)eGameSituation.None); }
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
