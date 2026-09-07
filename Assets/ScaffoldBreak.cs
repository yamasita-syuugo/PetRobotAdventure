using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldBreak : MonoBehaviour
{
    [SerializeField] GameObject []blocks;

    // Start is called before the first frame update
    void Start()
    {
        Manager_StageSelect manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
        breakOn = manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetScaffoldBreakRun();
    }
    [SerializeField] float time = 0;
    float nextTime = .5f;
    float timeSpan = .5f;
    // Update is called once per frame
    void Update()
    {
        ScaffoldBreakRun();
    }
    [SerializeField] bool breakOn = false;
    void ScaffoldBreakRun() {
        if (!breakOn) return;

        time += Time.deltaTime;
        if(blocks != null) { blocks = GetComponent<Create_Scaffold>().GetBlocks(); }
        if (nextTime >= time) { nextTime += timeSpan;blocks[(int)(time/timeSpan)].GetComponent<PlannedBreak>().SetPlannedBreakOn(true); }
    }
}
