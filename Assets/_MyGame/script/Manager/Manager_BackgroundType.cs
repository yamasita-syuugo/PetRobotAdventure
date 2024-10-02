using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBackGroundType
{
    [InspectorName("")]none,

    sea,
    forest,

    [InspectorName("")] backGroundTypeMax,
}

public class Manager_BackgroundType : MonoBehaviour
{
    [SerializeField]
    eBackGroundType backGroundType = eBackGroundType.sea;
    public eBackGroundType GetBackGroundType() { return backGroundType; }
    public void AddBackGroundType(int add = 1)
    {
        backGroundType += add;
        if(backGroundType <= eBackGroundType.none)backGroundType = eBackGroundType.backGroundTypeMax - 1;
        else if(backGroundType >= eBackGroundType.backGroundTypeMax) backGroundType = eBackGroundType.none + 1;
    }
    public void DataSave() { PlayerPrefs.SetInt("backGroundType", (int)backGroundType); }
    void DataLoad() { backGroundType = (eBackGroundType)PlayerPrefs.GetInt("backGroundType"); }
    public void LeftButton() { AddBackGroundType(-1); }
    public void RightButton() { AddBackGroundType(1); }
    [SerializeField]
    Sprite[] backGroundBase = new Sprite[(int)eBackGroundType.backGroundTypeMax];
    public Sprite GetBackGroundBase(eBackGroundType backGroundType_) { return backGroundBase[(int)backGroundType_]; }
    public Sprite[] GetBackGroundBase() { return backGroundBase; }
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
