using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Select_Chara_Text : MonoBehaviour
{
    Manager_Player manager_Player;

    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>();
    }

    // Update is called once per frame
    ePlayerType oldPlayerType = ePlayerType.none;
    void Update()
    {
        ePlayerType newPlayerType = manager_Player.GetPlayerTypeIndex();
        if (oldPlayerType == newPlayerType) return;oldPlayerType = newPlayerType;

        GetComponent<TextMeshProUGUI>().text = oldPlayerType.ToString();
    }
}
