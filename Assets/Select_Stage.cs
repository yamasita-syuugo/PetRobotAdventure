using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Select_Stage : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    void AddPlayerType(int add = 1)
    {
        manager_StageSelect.SetStage(manager_StageSelect.GetStage() + add);
    }
    public void LeftButton() { AddPlayerType(-1); }
    public void RightButton() { AddPlayerType(1); }
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GameObject.Find("TitleManager").GetComponent<Manager_StageSelect>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //}
}
