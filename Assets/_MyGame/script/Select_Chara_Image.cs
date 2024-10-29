using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Chara_Image : MonoBehaviour
{
    Manager_Player manager_Player;
    //Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>();
    }

    // Update is called once per frame
    int oldPlayerType = -1;
    void Update()
    {
        int playerType = manager_Player.GetPlayerTypeIndex();
        if (oldPlayerType == playerType) return; oldPlayerType = playerType;
        GetComponent<Image>().sprite = manager_Player.GetPlayerTypeBase(manager_Player.GetPlayerTypeIndex()).GetComponent<SpriteRenderer>().sprite;
    }
}
