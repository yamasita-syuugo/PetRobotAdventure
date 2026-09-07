using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlannedBreak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScaffoldPlannedBreak();
    }
    [SerializeField]bool plannedBreakOn = false;
    public void SetPlannedBreakOn(bool plannedBreakOn_) { plannedBreakOn = plannedBreakOn_; }
    float time = 0;

    float breakStartTime = 1;
    float blinkingTime = 0.3f;
    float blinkingOffTime = 0.5f;
    float breakTime = 5;
    void ScaffoldPlannedBreak()
    {
        if (!plannedBreakOn) return;
        time += Time.deltaTime;

        //if (breakStartTime < time) return;

        float blinkingLoopTime = blinkingTime + blinkingOffTime;
        if (((time*10) % (blinkingLoopTime*10)) < blinkingTime) GetComponent<SpriteRenderer>().color = Color.red;
        else GetComponent<SpriteRenderer>().color = Color.white;

        if(time > breakTime)Destroy(gameObject);
    }
}
