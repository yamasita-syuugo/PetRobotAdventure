using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display_Situation_Gate_Text : MonoBehaviour
{
    Manager_Gate manager_Gate;
    Manager_Score manager_Score;
    Manager_Time manager_Time;

    TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_Gate = manager.GetComponent<Manager_Gate>();
        manager_Score = manager.GetComponent<Manager_Score>();
        manager_Time = manager.GetComponent<Manager_Time>();

        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if(manager_Gate.GetGateOpen()) { textMeshPro.text = "OPEN";return; }
        switch (manager_Gate.GetGateOpenType())
        {
            case eGateOpenType.none:textMeshPro.text = "OPEN";break;
            case eGateOpenType.scoreCheck_Posi_Destroy_ + (int)eEnemyType.bom:textMeshPro.text = manager_Score.GetScore(eScoreType.Destroy_ + (int)eEnemyType.bom).ToString() + " / " + manager_Gate.GetGateOpenNum().ToString();break;
            case eGateOpenType.time_Countdown: textMeshPro.text = ((int)manager_Time.GetPlayTime()).ToString() + " / " + manager_Gate.GetGateOpenNum().ToString(); break;
            default:Debug.Log("switch : error_" + manager_Gate.GetGateOpenType().ToString());break;
        }
        
    }
}
