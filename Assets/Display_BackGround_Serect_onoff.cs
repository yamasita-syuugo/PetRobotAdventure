using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_BackGround_Serect_onoff : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    bool oldBackGroundSerect = true;
    void Update()
    {
        if (GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().GetBackGroundSerect() == oldBackGroundSerect) return;
        oldBackGroundSerect = !oldBackGroundSerect;

        if (oldBackGroundSerect) GetComponent<Image>().color = Color.red;
        else GetComponent<Image>().color = Color.clear;
    }
}
