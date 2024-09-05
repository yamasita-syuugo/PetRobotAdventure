using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveTimeDisplay : MonoBehaviour
{
    WaveManager enemyCreate;
    // Start is called before the first frame update
    void Start()
    {
        enemyCreate = GameObject.Find("TimeManager").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = enemyCreate.GetWaveTime();
        //string waveType = enemyCreate.GetWaveName();
        GetComponent<TextMeshProUGUI>().text = time.ToString("000");
    }
}
