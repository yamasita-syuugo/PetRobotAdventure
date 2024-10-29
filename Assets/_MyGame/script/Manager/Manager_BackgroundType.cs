using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_BackgroundType : MonoBehaviour
{
    [SerializeField]
    int backGroundIndex = 0;
    public int GetBackGroundIndex() { return backGroundIndex; }
    public void SetBackGroundIndex(int backGroundIndex_) { backGroundIndex = backGroundIndex_; }
    public void AddBackGroundIndex(int add = 1)
    {
        backGroundIndex += add;
        if (backGroundIndex < 0) backGroundIndex = backGroundBase.Length - 1;
        else if (backGroundIndex >= backGroundBase.Length) backGroundIndex = 0;
    }
    public void LeftButton() { AddBackGroundIndex(-1); }
    public void RightButton() { AddBackGroundIndex(1); }

    [SerializeField]
    Sprite[] backGroundBase;
    public Sprite[] GetBackGroundBase() { return backGroundBase; }
    public Sprite GetBackGroundBase(int backGroundIndex_) { return backGroundBase[backGroundIndex_]; }
    [SerializeField]
    bool[] getSituation = new bool[16];
    public bool GetGetSituation(int index) { return getSituation[index]; }
    public void SetGetSituation(int index, bool getSituation_) { getSituation[index] = getSituation_; }
    public void DataSave()
    {
        PlayerPrefs.SetInt("backGroundIndex", (int)backGroundIndex);
        Manager_Save.BoolSave("BackGroundGetSituation", backGroundBase.Length, getSituation);
    }
    public void DataLoad()
    {
        backGroundIndex = PlayerPrefs.GetInt("backGroundIndex");
        Manager_Save.BoolLoad("BackGroundGetSituation", backGroundBase.Length, out getSituation);
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