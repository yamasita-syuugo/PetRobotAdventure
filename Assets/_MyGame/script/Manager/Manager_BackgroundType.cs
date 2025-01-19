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
    public Sprite GetBackGroundBase(int backGroundIndex_) {
        if (backGroundIndex_ < 0) backGroundIndex_ = 0;else if(backGroundIndex_ >= backGroundBase.Length)backGroundIndex_ = backGroundBase.Length - 1;
        return backGroundBase[backGroundIndex_]; }
    //GetSituation
    public bool GetGetSituation(int index) { return GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.Background,index); }
    public void SetGetSituation(int index, bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.Background, index,getSituation_); }
    public void DataSave()
    {
        PlayerPrefs.SetInt("backGroundIndex", (int)backGroundIndex);
    }
    public void DataLoad()
    {
        backGroundIndex = PlayerPrefs.GetInt("backGroundIndex");
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