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

    int oldStageIndex = -1;
    // Update is called once per frame
    void Update()
    {
        int stageIndex = (int)manager_StageSelect.GetStage();
        if (oldStageIndex == stageIndex) oldStageIndex = stageIndex;

        GetComponent<TextMeshProUGUI>().text = manager_StageSelect.GetStage().ToString();
    }
}
