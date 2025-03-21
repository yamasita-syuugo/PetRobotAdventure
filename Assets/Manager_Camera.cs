using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Camera : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    float cameraMoveSpeedDeray = 1.26f;
    public float GetCameraMoveSpeedDeray() { return cameraMoveSpeedDeray; }
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GetComponent<Manager_StageSelect>();
    }

    // Update is called once per frame
    eStage oldStage = eStage.none;
    void Update()
    {
        if (oldStage == manager_StageSelect.GetStage()) return; oldStage = manager_StageSelect.GetStage();

        switch (oldStage)
        {
            case eStage.fastPlay: cameraMoveSpeedDeray = 1.26f; break;
            case eStage.crowStage: cameraMoveSpeedDeray = 1.26f; break;
            case eStage.golemLabyrinth: cameraMoveSpeedDeray = 0.5f; break;
            case eStage.iceBom: cameraMoveSpeedDeray = 1.26f; break;
            case eStage.searchGate: cameraMoveSpeedDeray = 0.5f; break;
            case eStage.lastGame: cameraMoveSpeedDeray = 1.26f; break;
            default: Debug.Log("error : switch(eStage)"); break;
        }
    }
}
