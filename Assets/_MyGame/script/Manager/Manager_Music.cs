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
    //GetSituation
    public bool GetGetSituation(int index) { return GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.Music,index); }
    public  void SetGetSituation(int index ,bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.Music, index,getSituation_); }
    public void DataSave()
    {
        PlayerPrefs.SetInt("musicIndex", musicIndex);
    }
    public void DataLoad()
    {
        musicIndex = PlayerPrefs.GetInt("musicIndex");
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