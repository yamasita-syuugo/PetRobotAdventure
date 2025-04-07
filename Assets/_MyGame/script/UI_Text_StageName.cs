using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Text_StageName : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
    }

    // Update is called once per frame
    int oldStageIndex = -1;
    bool oldRandom = false;
    void Update()
    {
        int stageIndex = (int)manager_StageSelect.GetStage();
        if (oldStageIndex == stageIndex || oldRandom == manager_StageSelect.GetRandomStage()) { oldStageIndex = stageIndex; oldRandom = manager_StageSelect.GetRandomStage(); }

        if (oldRandom) GetComponent<TextMeshProUGUI>().text = "?";
        else GetComponent<TextMeshProUGUI>().text = manager_StageSelect.GetStage().ToString();
    }
}
