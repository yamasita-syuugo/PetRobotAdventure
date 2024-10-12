using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControl_Start : MonoBehaviour
{
    // Start is called before the first frame update
    bool gameClear = false;
    void Start()
    {
        if(GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().GetGameSituation() == eGameSituation.clear) gameClear = true;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void StartButton()
    {
        GameObject.FindWithTag("Manager").GetComponent<Manager_Save>().DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
    public void NextButton()
    {
        if (!gameClear) { GetComponent<Image>().color = Color.gray; return; }

        GameObject tmp = GameObject.FindWithTag("Manager");
        if (tmp == null) return;
        Manager_StageSelect manager_StageSelect = tmp.GetComponent<Manager_StageSelect>();
        manager_StageSelect.SetStage(manager_StageSelect.GetStage() + 1);
        GameObject.FindWithTag("Manager").GetComponent<Manager_Save>().DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
