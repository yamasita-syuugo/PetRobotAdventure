using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_MusicTitle : MonoBehaviour
{
    Manager_Music manager_Music;
    Manager_StageSelect manager_StageSelect;
    // Start is called before the first frame update
    void Start()
    {
        manager_Music = GameObject.FindWithTag("Manager").GetComponent<Manager_Music>();
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
    }

    // Update is called once per frame
    int oldMusicIndex = -1;
    void Update()
    {
        int newMusicIndex;
        if(manager_StageSelect.GetMusicSerect())newMusicIndex = manager_Music.GetMusicIndex();
        else newMusicIndex = manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetMusicIndex();
        if (oldMusicIndex == newMusicIndex) return; oldMusicIndex = newMusicIndex;

        if (manager_Music.GetMusicBase(oldMusicIndex) == null) { GetComponent<TextMeshProUGUI>().text = "ÅÙ - Ç»Çµ"; return; }
        GetComponent<TextMeshProUGUI>().text = "ÅÙ - " + manager_Music.GetMusicBase(oldMusicIndex).name;
    }
}
