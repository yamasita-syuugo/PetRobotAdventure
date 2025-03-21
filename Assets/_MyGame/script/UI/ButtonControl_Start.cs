using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControl_Start : MonoBehaviour
{
    GameObject manager;
    Manager_GameSituation manager_GameSituation;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        manager_GameSituation = manager.GetComponent<Manager_GameSituation>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void StartButton()
    {
        manager.GetComponent<Manager_Save>().DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
    public void NextButton()
    {
        if (!(manager_GameSituation.GetGameSituation() == eGameSituation.clear)) { GetComponent<Image>().color = Color.gray; return; }

        if (manager == null) return;
        Manager_StageSelect manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_StageSelect.SetStage(manager_StageSelect.GetStage() + 1);

        manager.GetComponent<Manager_Save>().DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
