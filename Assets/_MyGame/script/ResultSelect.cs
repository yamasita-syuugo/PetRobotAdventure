using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSelect : MonoBehaviour
{
    Manager_PlayerController manager_PlayerController;
    private void OnEnable()
    {
        manager_PlayerController = GameObject.FindWithTag("Manager").GetComponent<Manager_PlayerController>();
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        ButtonSelect();
    }
    enum eButton
    {
        None,

        next,
        retry,
        title,

        buttonMax,
    }
    [SerializeField]
    eButton button = eButton.next;
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
        if(!manager_PlayerController.JoystickButtonDown(eJoystickButton.circle)) return;
        Manager_StageSelect manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
        switch (button)
        {
            case eButton.next:
                manager_StageSelect.SetStage(manager_StageSelect.GetStage()  + 1);
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
                break;
            case eButton.retry:
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
                break;
            case eButton.title:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
                break;
        }
    }
}
