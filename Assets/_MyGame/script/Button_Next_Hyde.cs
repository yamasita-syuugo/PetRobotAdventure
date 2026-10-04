using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Next_Hyde : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        Manager_Collection manager_Collection  = manager.GetComponent<Manager_Collection>();
        Manager_StageSelect manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        if ((int)manager_StageSelect.GetStage() >= (int)eStage.max - 1) { gameObject.active = false; return; }
        gameObject.active = /*GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().GetGameSituation() == eGameSituation.clear;*/
            manager_Collection.GetGetSituation(eCollectionType.stage, (int)manager_StageSelect.GetStage() + 1);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
