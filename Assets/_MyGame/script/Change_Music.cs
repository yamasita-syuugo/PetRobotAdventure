using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Music : MonoBehaviour
{
    Manager_Music manager_Music;
    Manager_StageSelect manager_StageSelect;
    // Start is called before the first frame update
    void Start()
    {
        manager_Music = GameObject.FindWithTag("Manager").GetComponent<Manager_Music>();
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();

        GetComponent<AudioSource>().clip = manager_Music.GetMusicBase(manager_Music.GetMusicIndex());
    }

    // Update is called once per frame
    int oldMusicIndex = -1;
    void Update()
    {
        int newMusicIndex;
        if (manager_StageSelect.GetMusicSerect() || SceneManager.GetActiveScene().name == "Collection") newMusicIndex = manager_Music.GetMusicIndex();
        else newMusicIndex = manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetMusicIndex();
        if (oldMusicIndex == newMusicIndex) return; oldMusicIndex = newMusicIndex;

        GetComponent<AudioSource>().clip = manager_Music.GetMusicBase(oldMusicIndex);
        if(!(SceneManager.GetActiveScene().name == "Title") || manager_StageSelect.GetMusicSerect())GetComponent<AudioSource>().Play();
    }
}