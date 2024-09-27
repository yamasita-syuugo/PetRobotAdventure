using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void StartButton()
    {
        DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
    public void NextButton()
    {
        GameObject tmp = GameObject.Find("Manager_Result");
        if (tmp == null) return;
        Manager_StageSelect manager_StageSelect = tmp.GetComponent<Manager_StageSelect>();
        Debug.Log(manager_StageSelect.GetStage());
        manager_StageSelect.SetStage(manager_StageSelect.GetStage() + 1);
        Debug.Log(manager_StageSelect.GetStage());
        DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void DataSave()
    {
        string name = SceneManager.GetActiveScene().name;
        if (name == "Title")
        {
            GameObject.Find("TitleManager").GetComponent<Manager_StageSelect>().DataSave();
            GameObject.Find("Serect_Chara").GetComponent<Select_Chara>().DataSave();
        }else if(name == "Result")
        {
            GameObject.Find("Manager_Result").GetComponent<Manager_StageSelect>().DataSave();
        }

    }
}
