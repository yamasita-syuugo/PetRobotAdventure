using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Select_Stage : MonoBehaviour
{
    [SerializeField]
    eStage stage = eStage.none + 1;
    public eStage GetStage() { return stage; }
    void AddPlayerType(int add = 1)
    {
        stage = stage + add;
        if (stage <= eStage.none) stage = eStage.eStageMax - 1;
        else if (stage >= eStage.eStageMax) stage = eStage.none + 1;
    }
    public void LeftButton() { AddPlayerType(-1); }
    public void RightButton() { AddPlayerType(1); }
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DataSave() { PlayerPrefs.SetInt("stage", (int)stage); }
    public void DataLoad() { stage = (eStage)PlayerPrefs.GetInt("stage"); }
}
