using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBackImage_Color : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       if(GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().GetGameSituation() == eGameSituation.clear)GetComponent<Image>().color = Color.yellow;
       else GetComponent<Image>().color = Color.blue;

        GetComponent<Image>().color *= new Color(1, 1, 1, 0.9f);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
