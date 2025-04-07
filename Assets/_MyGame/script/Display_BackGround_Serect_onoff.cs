using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_BackGround_Serect_onoff : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();

        if (manager_StageSelect.GetBackGroundSerect()) GetComponent<Image>().color = new Color(0.3f, 0.5f, 0.6f);
        else GetComponent<Image>().color = Color.white;
    }

    // Update is called once per frame
    bool oldBackGroundSerect = true;
    void Update()
    {
        if (manager_StageSelect.GetBackGroundSerect() == oldBackGroundSerect) return;
        oldBackGroundSerect = !oldBackGroundSerect;

        if (oldBackGroundSerect) GetComponent<Image>().color = new Color(0.3f,0.5f,0.6f);
        else GetComponent<Image>().color = Color.white;
    }
}
