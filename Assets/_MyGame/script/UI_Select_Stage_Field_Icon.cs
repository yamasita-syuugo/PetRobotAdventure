using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Select_Stage_Field_Icon : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
    }

    // Update is called once per frame
    eStage oldStageIndex = eStage.none;
    void Update()
    {
        if (oldStageIndex == manager_StageSelect.GetStage()) return; oldStageIndex = manager_StageSelect.GetStage();

        switch (oldStageIndex)
        {
            case eStage.none: break;
            case eStage.fastPlay: break;
            case eStage.crowStage: break;
            case eStage.golemLabyrinth: break;
            case eStage.iceBom: break;
            case eStage.searchGate: break;
            case eStage.lastGame: break;

            default: Debug.Log("error : switch(eStage)"); break;
        }
    }
}
