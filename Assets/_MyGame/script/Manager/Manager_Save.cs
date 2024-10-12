using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Save : MonoBehaviour
{
    public void DataSave()
    {
        GetComponent<Manager_StageSelect>().DataSave();

        GetComponent<Manager_Player>().DataSave();

        GetComponent<Manager_BackgroundType>().DataSave();
        GetComponent<Manager_MousePointerType>().DataSave();
        GetComponent<Manager_Music>().DataSave();

        GetComponent<Manager_GameSituation>().DataSave();   //クリアか失敗かの保存

    }
    public void DataLoad()
    {
        GetComponent<Manager_StageSelect>().DataLoad();

        GetComponent<Manager_Player>().DataLoad();

        GetComponent<Manager_BackgroundType>().DataLoad();
        GetComponent<Manager_MousePointerType>().DataLoad();
        GetComponent<Manager_Music>().DataLoad();

        GetComponent<Manager_GameSituation>().DataLoad();   //クリアか失敗かの保存

    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnApplicationQuit()
    {
        DataLoad();
    }
}
