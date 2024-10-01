using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Stage_Image : MonoBehaviour
{
    [SerializeField]
    Sprite[] StageImage = new Sprite[(int)eStage.eStageMax];
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    eStage oldStage = 0;
    void Update()
    {
        eStage stage = GameObject.FindWithTag("Manager").GetComponentInParent<Manager_StageSelect>().GetStage();
        if (oldStage == stage) return; oldStage = stage;
        if (oldStage == eStage.none) return; if (oldStage == eStage.eStageMax) return;
        GetComponent<Image>().sprite = StageImage[(int)stage];
    }
}
