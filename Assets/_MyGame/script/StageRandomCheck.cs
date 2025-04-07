using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRandomCheck : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();

        if (manager_StageSelect.GetRandomStage()) GetComponent<Image>().color = new Color(0.6f, 0.3f, 0.3f);
        else GetComponent<Image>().color = Color.white;
    }

    // Update is called once per frame
    bool oldStageRandom = true;
    void Update()
    {
        if (manager_StageSelect.GetRandomStage() == oldStageRandom) return; oldStageRandom = !oldStageRandom;

        if (oldStageRandom )GetComponent<Image>().color = new Color(0.6f,0.3f,0.3f);
        else GetComponent<Image>().color = Color.white;
    }
}
