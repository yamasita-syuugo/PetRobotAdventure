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
        GameObject.FindWithTag("Manager").GetComponent<Manager_Player>().DataSave();
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
