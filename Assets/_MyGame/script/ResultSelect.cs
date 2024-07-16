using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonSelect();
    }
    enum eButton
    {
        None,

        retry,
        title,

        buttonMax,
    }
    [SerializeField]
    eButton button = eButton.retry;
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
            case eButton.retry:
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
                break;
            case eButton.title:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
                break;
        }
    }
}
