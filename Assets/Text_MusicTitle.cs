using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_MusicTitle : MonoBehaviour
{
    Manager_Music manager_Music;
    // Start is called before the first frame update
    void Start()
    {
        manager_Music = GameObject.FindWithTag("Manager").GetComponent<Manager_Music>();
    }

    // Update is called once per frame
    int oldMusicIndex = -1;
    void Update()
    {
        if (oldMusicIndex == manager_Music.GetMusicIndex()) return; oldMusicIndex = manager_Music.GetMusicIndex();

        if (manager_Music.GetMusicBase(oldMusicIndex) == null) { GetComponent<TextMeshProUGUI>().text = "ÅÙ - Ç»Çµ"; return; }
        GetComponent<TextMeshProUGUI>().text = "ÅÙ - " + manager_Music.GetMusicBase(oldMusicIndex).name;
    }
}
