using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ButtonSelect();
        StageSelect();
    }

    enum eButton
    {
        None,

        start,
        gacha,
        bestScoar,
        end,

        buttonMax,
    }
    [SerializeField]
    eButton button = eButton.start;
        bool triggerBut = false;
    eButton oldButton = eButton.None;
    void ButtonSelect()
    {

        if (Input.GetAxis("Vertical") == 0 && triggerBut) triggerBut = false;
        if (!triggerBut)
        {
            button = (eButton)((int)button + Input.GetAxis("Vertical"));
            if (button == eButton.buttonMax) button = eButton.None + 1;
            if (button == eButton.None) button = eButton.buttonMax - 1;

            PlayerPrefs.SetInt("button", (int)button);

            if (button != oldButton)
            {
                oldButton = button;

                triggerBut = true;
            }
        }
        if (Input.GetAxis("Decision") == 0) return;
        switch (button)
        {
            case eButton.start:
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
                break;
            case eButton.gacha:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Gacha");
                break;
            case eButton.bestScoar:
                UnityEngine.SceneManagement.SceneManager.LoadScene("BestScoar");
                break;
            case eButton.end:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
                break;
        }
    }

    enum eStage
    {
        None,

        block,
        grass,
        ice,

        stageMax,
    }
    [SerializeField]
    eStage stage = eStage.block;
    enum eRandomBreak
    {
        None,

        random0,
        //random30,
        random50,
        random70,

        randomBreakMax,
    }
    [SerializeField]
    eRandomBreak randomBreak = eRandomBreak.random0;

    eStage oldStage = eStage.None;
    bool triggerHor = false;
    eRandomBreak oldRandom = eRandomBreak.None;
    bool triggerQE = false;
    void StageSelect()
    {
        if (Input.GetAxis("Horizontal") == 0 && triggerHor) triggerHor = false;
        if (!triggerHor)
        {
            stage = (eStage)((int)stage + Input.GetAxis("Horizontal"));
            if (stage == eStage.stageMax) stage = eStage.None + 1;
            if (stage == eStage.None) stage = eStage.stageMax - 1;

            if (oldStage != stage)
            {
                oldStage = stage;

                PlayerPrefs.SetInt("stage", (int)stage);

                switch (stage)
                {
                    case eStage.block:
                        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetCreatType(CreateScaffold.eCreatType.block);
                        break;
                    case eStage.ice:
                        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetCreatType(CreateScaffold.eCreatType.ice);
                        break;
                    case eStage.grass:
                        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetCreatType(CreateScaffold.eCreatType.grass);
                        break;
                }
                GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().CreateObject();

                triggerHor = true;
            }
        }

        if (Input.GetAxis("QE") == 0 && triggerQE) triggerQE = false;
        if (!triggerQE)
        {
            randomBreak = (eRandomBreak)((int)randomBreak + Input.GetAxis("QE"));
            if (randomBreak == eRandomBreak.randomBreakMax) randomBreak = eRandomBreak.randomBreakMax - 1;
            if (randomBreak == eRandomBreak.None) randomBreak = eRandomBreak.None + 1;

            if (oldRandom != randomBreak)
            {
                oldRandom = randomBreak; 

                PlayerPrefs.SetInt("randomBreak", (int)randomBreak);

                switch (randomBreak)
                {
                    case eRandomBreak.random0:
                        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(0);
                        break;
                    //case eRandomBreak.random30:
                    //    GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(30);
                    //    break;
                    case eRandomBreak.random50:
                        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(50);
                        break;
                    case eRandomBreak.random70:
                        GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().SetRandomBreak(70);
                        break;
                }
                GameObject.Find("CreateScaffold").GetComponent<CreateScaffold>().CreateObject();

                triggerQE = true;
            }
        }
    }
}
