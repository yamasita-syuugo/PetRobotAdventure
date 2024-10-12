using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display_GameSituation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        switch (GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().GetGameSituation())
        {
            case eGameSituation.clear: text.text = "clear";text.color = Color.red; break;
            case eGameSituation.failure: text.text = "failure"; text.color = Color.blue; break;
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
