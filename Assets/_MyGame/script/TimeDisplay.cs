using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        float time = timeManager.GetPlayTime();
        GetComponent<TextMeshProUGUI>().text = time.ToString("000");
    }
}
