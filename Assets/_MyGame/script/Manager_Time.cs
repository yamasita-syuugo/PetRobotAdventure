using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Time : MonoBehaviour
{
    float playTimer = 0f;
    bool timeStop = false;
    // Start is called before the first frame update
    void Start()
    {
        playTimer = 0f;

        stopTimer = stopTimerSteatTime;
    }

    // Update is called once per frame
    [SerializeField]
    float stopTimerSteatTime = 10f;
    float stopTimer;
    void Update()
    {
        if (timeStop)
        {
            if(Input.GetKeyDown(KeyCode.Space)) timeStop = false;
            if (stopTimer < 0f) timeStop = false;
            stopTimer -= Time.deltaTime;
            return;
        }
        else stopTimer = stopTimerSteatTime;

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;
        ObjectFall.eSituation situation = player.GetComponent<ObjectFall>().GetSituation();

        if (situation != ObjectFall.eSituation.fall) playTimer += Time.deltaTime;
    }

    public float GetPlayTime() { return playTimer; }
    public void SetTimeStop(bool timeStop_) {  timeStop = timeStop_; }
    public bool GetTimeStop() { return timeStop; }
}
