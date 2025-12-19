using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Retry_Hyde : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.active = GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().GetGameSituation() == eGameSituation.failure;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
