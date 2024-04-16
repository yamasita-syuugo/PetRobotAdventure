using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    float playTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        if (player[0] == null) return;
        ObjectFall.eSituation situation = player[0].GetComponent<ObjectFall>().GetSituation();

        if (situation == ObjectFall.eSituation.normal) playTimer += Time.deltaTime;
    }

    public float GetPlayTime() { return playTimer; }
}
