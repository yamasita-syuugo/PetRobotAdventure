using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum eUiType
    {
        text,
        image,
        sprig,
    }
public class StopDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}
    [SerializeField]
    eUiType uiType;
    // Update is called once per frame
    bool oldTimeStop = true;
    void Update()
    {
        bool newTimeStop = GameObject.Find("TimeManager").GetComponent<TimeManager>().GetTimeStop();
        if (oldTimeStop == newTimeStop) return;
        oldTimeStop = newTimeStop;
        if (newTimeStop)
        {
            if (uiType == eUiType.text) GetComponent<TextMeshProUGUI>().color = Color.white;
            else if (uiType == eUiType.image) GetComponent<Image>().color = Color.white;
            else if (uiType == eUiType.sprig) GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            if (uiType == eUiType.text) GetComponent<TextMeshProUGUI>().color = Color.clear;
            else if (uiType == eUiType.image) GetComponent<Image>().color = Color.clear;
            else if (uiType == eUiType.sprig) GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }
}
