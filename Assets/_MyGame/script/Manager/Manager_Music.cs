using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Music : MonoBehaviour
{
    [SerializeField]
    AudioClip[] musicBase;
    public AudioClip[] GetMusicBase() { return musicBase; }
    public AudioClip GetMusicBase(int index_) { return musicBase[index_]; }
    [SerializeField]
    int musicIndex = 0;
    public int GetMusicIndex() { return musicIndex; }
    public void SetMusicIndex(int musicIndex_) {  musicIndex = musicIndex_ ; }
    public void AddMusicIndex(int add_ = 1)
    {
        musicIndex += add_;
        if (musicIndex < 0) musicIndex = musicBase.Length - 1;
        if (musicIndex >= musicBase.Length) musicIndex = 0;
    }
    public void MusicIndexLeftButton() { AddMusicIndex(-1); }
    public void MusicIndexRightButton() { AddMusicIndex(1); }
    [SerializeField]
    bool[] getSituation = new bool[16];
    public bool GetGetSituation(int index) { return getSituation[index]; }
    public  void SetGetSituation(int index ,bool getSituation_) { getSituation[index] = getSituation_; }
    public void DataSave()
    {
        PlayerPrefs.SetInt("musicIndex", musicIndex);
        Manager_Save.BoolSave("MusicGetSituation", musicBase.Length, getSituation);
    }
    public void DataLoad()
    {
        musicIndex = PlayerPrefs.GetInt("musicIndex");
        Manager_Save.BoolLoad("MusicGetSituation", musicBase.Length, out getSituation);
    }
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}