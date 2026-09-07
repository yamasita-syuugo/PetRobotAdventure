using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Music : MonoBehaviour
{
    Manager_Music manager_Music;
    Manager_StageSelect manager_StageSelect;
    Manager_Collection manager_Collection;
    Manager_GameSituation manager_GameSituation;
    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_Music = manager.GetComponent<Manager_Music>();
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Collection = manager.GetComponent<Manager_Collection>();
        manager_GameSituation = manager.GetComponent<Manager_GameSituation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = manager_Music.GetMusicBase(manager_Music.GetMusicIndex());
    }

    // Update is called once per frame
    int oldMusicIndex = -1;
    void Update()
    {
        int newMusicIndex = 0;
        if (SceneManager.GetActiveScene().name == "Title" && !manager_StageSelect.GetMusicSerect()) newMusicIndex = 1;
        else if(SceneManager.GetActiveScene().name == "Result") {
            if (manager_GameSituation.GetGameSituation() == eGameSituation.clear) newMusicIndex = manager_Music.GetMusicBase().Length - 2;
            else if (manager_GameSituation.GetGameSituation() == eGameSituation.failure) newMusicIndex = manager_Music.GetMusicBase().Length - 1;
            else newMusicIndex = 0;
        }
        else if (manager_StageSelect.GetMusicSerect() || SceneManager.GetActiveScene().name == "Collection") newMusicIndex = manager_Music.GetMusicIndex();
        else newMusicIndex = manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetMusicIndex();

        if (oldMusicIndex == newMusicIndex) return; oldMusicIndex = newMusicIndex;

        GetComponent<AudioSource>().clip = manager_Music.GetMusicBase(oldMusicIndex);
        GetComponent<AudioSource>().volume = manager_Music.GetVolume(newMusicIndex);
        GetComponent<AudioSource>().Play();
        manager_Collection.SetGetSituation(eCollectionType.music, oldMusicIndex, true);
    }
}