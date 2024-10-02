using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Save : MonoBehaviour
{
    public void DataSave()
    {
        GetComponent<Manager_StageSelect>().DataSave();
        GetComponent<Manager_BackgroundType>().DataSave();
        GetComponent<Manager_MousePointerType>().DataSave();
        GameObject.Find("Serect_Chara").GetComponent<Select_Chara>().DataSave();
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
