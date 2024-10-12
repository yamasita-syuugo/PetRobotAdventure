using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_BackgroundType : MonoBehaviour
{
    [SerializeField]
    int backGroundIndex = 0;
    public int GetBackGroundType() { return backGroundIndex; }
    public void AddBackGroundType(int add = 1)
    {
        backGroundIndex += add;
        if (backGroundIndex < 0) backGroundIndex = backGroundBase.Length - 1;
        else if (backGroundIndex >= backGroundBase.Length) backGroundIndex = 0;
    }
    public void LeftButton() { AddBackGroundType(-1); }
    public void RightButton() { AddBackGroundType(1); }

    [SerializeField]
    Sprite[] backGroundBase;
    public Sprite[] GetBackGroundBase() { return backGroundBase; }
    public Sprite GetBackGroundBase(int backGroundIndex_) { return backGroundBase[backGroundIndex_]; }
    [SerializeField]
    bool[] getSituation = new bool[16];
    public void DataSave()
    {
        PlayerPrefs.SetInt("backGroundIndex", (int)backGroundIndex);

        int backGroundBaseSize = backGroundBase.Length;
        for (int i = 0; i < backGroundBaseSize; i += 16)
        {
            int boolNum = 0;
            for (int j = i; j < i + 16 && j < backGroundBaseSize; j++)
            {
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                boolNum += boolNumBit * (getSituation[j] ? 1 : 0);
            }
            PlayerPrefs.SetInt("BackGroundGetSituation" + (i / 16).ToString(), boolNum);
        }
    }
    public void DataLoad()
    {
        backGroundIndex = PlayerPrefs.GetInt("backGroundIndex");

        int backGroundBaseSize = backGroundBase.Length;
        getSituation = new bool[backGroundBaseSize];
        for (int i = 0; i < backGroundBaseSize; i += 16)
        {
            int boolNum = PlayerPrefs.GetInt("BackGroundGetSituation" + (i / 16).ToString());
            for (int j = i + 15; i <= j; j--)
            {
                if (j >= backGroundBaseSize) continue;
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                if (boolNum / boolNumBit >= 1) { getSituation[j] = true; boolNum -= boolNumBit; }
                else { getSituation[j] = false; }
            }
        }
    }
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