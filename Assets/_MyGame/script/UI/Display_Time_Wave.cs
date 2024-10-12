using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display_Time_Wave : MonoBehaviour
{
    Manager_Wave enemyCreate;
    // Start is called before the first frame update
    void Start()
    {
        enemyCreate = GameObject.FindWithTag("Manager").GetComponent<Manager_Wave>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = enemyCreate.GetWaveTime();
        //string waveType = enemyCreate.GetWaveName();
        GetComponent<TextMeshProUGUI>().text = time.ToString("000");
    }
}
