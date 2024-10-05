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
        GameObject tmp = GameObject.FindWithTag("Manager");
        if (tmp == null) return;
        Manager_StageSelect manager_StageSelect = tmp.GetComponent<Manager_StageSelect>();
        manager_StageSelect.SetStage(manager_StageSelect.GetStage() + 1);
        DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void DataSave()
    {
        string name = SceneManager.GetActiveScene().name;
            GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().DataSave();
        if (name == "Title")
        {
            GameObject.FindWithTag("Manager").GetComponent<Manager_Player>().DataSave();
        }
    }
}
