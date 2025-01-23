using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

enum eButtonType
{
    none,

    BackGround,
    Music,

    [InspectorName("")]max,
}

public class HidingButton : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    [SerializeField]
    eButtonType buttonType = eButtonType.none;

    Button[] button;
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();

        button = GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    bool oldButtonTypeSerect = true;
    void Update()
    {
        switch (buttonType)
        {
            case eButtonType.none: Debug.Log("ButtonType : error"); return; break;
            case eButtonType.BackGround:
                bool newBackGroundSerect = manager_StageSelect.GetBackGroundSerect();
                if (oldButtonTypeSerect == newBackGroundSerect) return; oldButtonTypeSerect = newBackGroundSerect;
                break;
            case eButtonType.Music:
                bool newMusicSerect = manager_StageSelect.GetMusicSerect();
                if (oldButtonTypeSerect == newMusicSerect) return; oldButtonTypeSerect = newMusicSerect;
                break;
        }

        if (oldButtonTypeSerect) { for (int i = 0; i < button.Length; i++) { button[i].gameObject.SetActive(true); } }
        else for (int i = 0; i < button.Length; i++) { button[i].gameObject.SetActive(false); }
    }
}
