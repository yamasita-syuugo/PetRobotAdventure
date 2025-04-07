using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundType
{
    [InspectorName("")]none = -1,

    //Player
    fall,
    waterSound,
    shot,
    impact,

    //enemy
    explosion,

    [InspectorName("")]max,
}

public class Manager_Sounds : MonoBehaviour
{
    AudioClip[] soundBase = new AudioClip[(int)eSoundType.max];
    void SetSoundBase()
    {
        soundBase[(int)eSoundType.fall] = audioClip_Fall;
        soundBase[(int)eSoundType.waterSound] = audioClip_WaterSound;
        soundBase[(int)eSoundType.shot] = audioClip_Shot;
        soundBase[(int)eSoundType.impact] = audioClip_Impact;

        soundBase[(int)eSoundType.explosion] = audioClip_Explosion;
    }
    [SerializeField] AudioClip audioClip_Fall;
    [SerializeField] AudioClip audioClip_WaterSound;
    [SerializeField] AudioClip audioClip_Shot;
    [SerializeField] AudioClip audioClip_Impact;

    [SerializeField] AudioClip audioClip_Explosion;

    [SerializeField] AudioSource []sound = new AudioSource[10];
    public AudioSource GetSound(eSoundType soundType) { return sound[(int)soundType]; }

    private void OnEnable()
    {
        SetSoundBase();

        int soundNum = 0;
        for(int i = 0; i < soundBase.Length; i++)
        {
            if (soundBase[i] == null) continue;
            ;
            sound[i] = Instantiate<GameObject>(new GameObject()).AddComponent<AudioSource>();
            sound[i].clip = soundBase[i];
            sound[i].name = soundBase[i].name;
            sound[i].transform.parent = transform;
        }
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
