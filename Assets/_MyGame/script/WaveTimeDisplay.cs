using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveTimeDisplay : MonoBehaviour
{
    [SerializeField]
    EnemyCreate enemyCreate;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        float time = enemyCreate.GetWaveTime();
        string waveType = enemyCreate.GetWaveName();
        GetComponent<TextMeshProUGUI>().text = time.ToString("000") + "\n" + waveType;
    }
}
