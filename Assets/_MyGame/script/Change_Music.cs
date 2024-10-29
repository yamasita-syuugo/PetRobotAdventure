using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Music : MonoBehaviour
{
    Manager_Music manager_Music;
    // Start is called before the first frame update
    void Start()
    {
        manager_Music = GameObject.FindWithTag("Manager").GetComponent<Manager_Music>();

        GetComponent<AudioSource>().clip = manager_Music.GetMusicBase(manager_Music.GetMusicIndex());
    }

    // Update is called once per frame
    int oldMusicIndex = -1;
    void Update()
    {
        if (oldMusicIndex == manager_Music.GetMusicIndex()) return; oldMusicIndex = manager_Music.GetMusicIndex();
        GetComponent<AudioSource>().clip = manager_Music.GetMusicBase(manager_Music.GetMusicIndex());
        GetComponent<AudioSource>().Play();
    }
}