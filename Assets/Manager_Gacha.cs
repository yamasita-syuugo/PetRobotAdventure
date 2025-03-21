using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Gacha : MonoBehaviour
{
    Manager_Player manager_Player;
    Manager_MousePointerType manager_MousePointerType;
    Manager_BackgroundType manager_BackgroundType;
    Manager_Music manager_Music;
    [SerializeField]
    int prizeNum;

    [SerializeField]
    int gachaPoint = 0;
    int needGachaPoint = 100;
    public int GetGachaPoint() {  return gachaPoint; }
    public void SetGachaPoint(int gachaPoint_) {  gachaPoint = gachaPoint_; }
    public void AddGachaPoint(int addPoint = 1) { gachaPoint += addPoint; }
    public void Gacha()
    {
        if (gachaPoint < needGachaPoint) return; gachaPoint -= needGachaPoint;

        int getPrize = Random.Range(0, prizeNum);
        Debug.Log("random "+getPrize);
        if (getPrize >= 0 && getPrize < manager_Player.GetPlayerTypeBases().Length - 1)
        {
            manager_Player.SetGetSituation(getPrize + 1, true);
            Debug.Log("getPlayer "  + (getPrize + 1));
        }
        getPrize -= manager_Player.GetPlayerTypeBases().Length - 1;
        if (getPrize >= 0 && getPrize < manager_MousePointerType.GetMousePointerAnimations().Length - 1)
        {
            manager_MousePointerType.SetGetSituation(getPrize + 1, true);
            Debug.Log("getMouse "  + (getPrize + 1));
        }
        getPrize -= manager_MousePointerType.GetMousePointerAnimations().Length - 1;
        if (getPrize >= 0 && getPrize < manager_BackgroundType.GetBackGround_Panel_Base().Length - 1)
        {
            manager_BackgroundType.SetGetSituation(getPrize + 1, true);
            Debug.Log("getBack " + (getPrize + 1));
        }
        getPrize -= manager_BackgroundType.GetBackGround_Panel_Base().Length - 1;
        if (getPrize >= 0 && getPrize < manager_Music.GetMusicBase().Length - 1)
        {
            manager_Music.SetGetSituation(getPrize + 1, true);
            Debug.Log("getMusic " + (getPrize + 1));
        }
        getPrize -= manager_Music.GetMusicBase().Length - 1;

        GetComponent<Manager_Save>().DataSave();
    }
    public void DataSave()
    {
        PlayerPrefs.SetInt("GachaPoint", gachaPoint);
    }
    public void DataLoad()
    {
        SetGachaPoint(PlayerPrefs.GetInt("GachaPoint"));
    }
    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GetComponent<Manager_Player>();
        manager_MousePointerType = GetComponent<Manager_MousePointerType>();
        manager_BackgroundType = GetComponent<Manager_BackgroundType>();
        manager_Music = GetComponent<Manager_Music>();

        prizeNum = manager_Player.GetPlayerTypeBases().Length - 1 + manager_MousePointerType.GetMousePointerAnimations().Length - 1 + manager_BackgroundType.GetBackGround_Panel_Base().Length - 1 + manager_Music.GetMusicBase().Length - 1;

        DataLoad();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
