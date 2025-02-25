using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_Music_Serect_onoff : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    bool oldMusicSerect = true;
    void Update()
    {
        if (GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().GetMusicSerect() == oldMusicSerect) return;
        oldMusicSerect = !oldMusicSerect;

        if (oldMusicSerect) GetComponent<Image>().color = Color.red;
        else GetComponent<Image>().color = Color.clear;
    }
}
