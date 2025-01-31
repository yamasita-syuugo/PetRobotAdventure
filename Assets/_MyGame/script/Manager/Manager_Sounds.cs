using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Sounds : MonoBehaviour
{
    [SerializeField]
    GameObject[] soundBase;
    [SerializeField]
    AudioSource []sound = new AudioSource[10];
    public AudioSource GetSound(string name)
    {
        for (int i = 0; i < sound.Length; i++) {
            if (soundBase[i].name == name) return sound[i];
        }
        return null;
    }

    private void OnEnable()
    {
        int soundNum = 0;
        for(int i = 0; i < soundBase.Length; i++)
        {
            if (soundBase[i] == null) continue;
            GameObject sound_ = Instantiate(soundBase[i]);
            sound_.transform.parent = transform;
            sound[i] = sound_.GetComponent<AudioSource>();
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
