using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRandomCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    bool oldStageRandom = true;
    void Update()
    {
        if (GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>().GetRandomStage() == oldStageRandom) return;
        oldStageRandom = !oldStageRandom;

        if (oldStageRandom )GetComponent<Image>().color = Color.red;
        else GetComponent<Image>().color = Color.clear;
    }
}
