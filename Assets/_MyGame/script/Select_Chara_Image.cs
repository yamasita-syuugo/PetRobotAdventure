using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Chara_Image : MonoBehaviour
{
    [SerializeField]
    Sprite[] playerImage = new Sprite[(int)ePlayerType.playerTypeMax];
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    ePlayerType oldPlayerType = 0;
    void Update()
    {
        ePlayerType playerType = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>().GetPlayerType();
        if (oldPlayerType == playerType) return; oldPlayerType = playerType;
        if (oldPlayerType == ePlayerType.none) return; if (oldPlayerType == ePlayerType.playerTypeMax) return;
        GetComponent<Image>().sprite = playerImage[(int)playerType];
    }
}
