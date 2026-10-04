using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Manager_ButtonSelect : MonoBehaviour
{
    [SerializeField]
    GameObject fastButton;
    [SerializeField]
    GameObject button;
    //private void OnEnable()
    //{

    //}
    // Start is called before the first frame update
    //void Start()
    //{


    //}

    // Update is called once per frame
    void Update()
    {

        ButtonRecovery();

        if (button != null) if (button.active == true) return; else { if (fastButton.active == true) button = fastButton;
                else { GameObject canvas = GameObject.Find("Canvas");Button[] buttons = canvas.GetComponentsInChildren<Button>();
                    for (int i = 0; i < buttons.Length; i++) if (buttons[i].IsActive()) { button = buttons[i].GetComponent<GameObject>(); break; } } }
        EventSystem.current.SetSelectedGameObject(button);
    }
    void ButtonRecovery()
    {
        if (button == EventSystem.current.currentSelectedGameObject) return;
        if(fastButton == null)fastButton = EventSystem.current.currentSelectedGameObject;

        if (EventSystem.current.currentSelectedGameObject == null) EventSystem.current.SetSelectedGameObject(button);
        button = EventSystem.current.currentSelectedGameObject;
    }

}
