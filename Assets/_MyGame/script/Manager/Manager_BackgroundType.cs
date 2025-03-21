using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBackGroundType
{
    [InspectorName("")]none = -1,

    sea,
    forest,

    [InspectorName("")] max,
}

public class Manager_BackgroundType : MonoBehaviour
{
    [SerializeField]
    eBackGroundType backGroundIndex = 0;
    public eBackGroundType GetBackGroundIndex() { return backGroundIndex; }
    public void SetBackGroundIndex(eBackGroundType backGroundIndex_) { backGroundIndex = backGroundIndex_; }
    public void AddBackGroundIndex(int add = 1)
    {
        backGroundIndex += add;
        if (backGroundIndex < 0) backGroundIndex = eBackGroundType.max - 1;
        else if (backGroundIndex >= eBackGroundType.max) backGroundIndex = 0;
    }
    public void LeftButton() { AddBackGroundIndex(-1); }
    public void RightButton() { AddBackGroundIndex(1); }

    Sprite[] backGround_Panel_Base = new Sprite[(int)eBackGroundType.max];
    public Sprite GetBackGround_Panel_Base(eBackGroundType backGroundType) { return backGround_Panel_Base[(int)backGroundType]; }
    public Sprite[] GetBackGround_Panel_Base() { return backGround_Panel_Base; }
    void SetBackGround_Panel_Base()
    {
        backGround_Panel_Base[(int)eBackGroundType.sea] = backGround_Panel_sea;
        backGround_Panel_Base[(int)eBackGroundType.forest] = backGround_Panel_forest;
    }
    [SerializeField] Sprite backGround_Panel_sea;
    [SerializeField] Sprite backGround_Panel_forest;

    //GetSituation
    public bool GetGetSituation(int index) { return GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.background,index); }
    public void SetGetSituation(int index, bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.background, index,getSituation_); }
    public void DataSave()
    {
        PlayerPrefs.SetInt("backGroundIndex", (int)backGroundIndex);
    }
    public void DataLoad()
    {
        backGroundIndex = (eBackGroundType)PlayerPrefs.GetInt("backGroundIndex");
    }
    private void OnEnable()
    {
        SetBackGround_Panel_Base();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

}