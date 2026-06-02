using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_SceneSelect : MonoBehaviour
{
    Manager_GameSituation manager_GameSituation;
    Manager_StageSelect manager_StageSelect;
    Manager_Save manager_Save;
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_GameSituation = manager.GetComponent<Manager_GameSituation>();
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Save = manager.GetComponent<Manager_Save>();

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void MainGameSteat(int addStage = 0)
    {
        manager_StageSelect.AddStage(addStage);
        manager_Save.DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
    public void NextMainGameSteat()
    {
        if (!(manager_GameSituation.GetGameSituation() == eGameSituation.clear)) { GetComponent<Image>().color = Color.gray; return; }

        manager_StageSelect.AddStage(1);
        manager_Save.DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
    public void CollectionSteat()
    {
        manager_Save.DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Collection");
    }
    public void BestScoarSteat()
    {
        manager_Save.DataSave();

        UnityEngine.SceneManagement.SceneManager.LoadScene("BestScoar");
    }
    public void TitleSteat()
    {
        manager_Save.DataSave();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
    public void ApplicationEnd()
    {
        manager_Save.DataSave();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
