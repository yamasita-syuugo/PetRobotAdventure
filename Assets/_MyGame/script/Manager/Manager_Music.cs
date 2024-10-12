using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Music : MonoBehaviour
{
    [SerializeField]
    AudioClip[] musicBase;
    public AudioClip GetMusicBase(int index_) { return musicBase[index_]; }
    [SerializeField]
    int musicIndex = 0;
    public int GetMusicIndex() { return musicIndex; }
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
    public void DataSave()
    {
        PlayerPrefs.SetInt("musicIndex", musicIndex);

        int musicSize = musicBase.Length;
        for (int i = 0; i < musicSize; i += 16)
        {
            int boolNum = 0;
            for (int j = i; j < i + 16 && j < musicSize; j++)
            {
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                boolNum += boolNumBit * (getSituation[j] ? 1 : 0);
            }
            PlayerPrefs.SetInt("MusicGetSituation" + (i / 16).ToString(), boolNum);
        }
    }
    public void DataLoad()
    {
        musicIndex = PlayerPrefs.GetInt("musicIndex");

        int musicSize = musicBase.Length;
        getSituation = new bool[musicSize];
        for (int i = 0; i < musicSize; i += 16)
        {
            int boolNum = PlayerPrefs.GetInt("MusicGetSituation" + (i / 16).ToString());
            for (int j = i + 15; i <= j; j--)
            {
                if (j >= musicSize) continue;
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                if (boolNum / boolNumBit >= 1) { getSituation[j] = true; boolNum -= boolNumBit; }
                else { getSituation[j] = false; }
            }
        }
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