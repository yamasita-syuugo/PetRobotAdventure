using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;
    // Start is called before the first frame update
    void Start()
    {
        timeManager = GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = ((int)timeManager.GetPlayTime()).ToString("000");
    }
}
