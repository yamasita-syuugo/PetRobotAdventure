using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Chara : MonoBehaviour
{
    [SerializeField]
    ePlayerType playerType = ePlayerType.none + 1;
    void AddPlayerType(int add = 1) { 
        playerType = playerType + add;
        if (playerType <= ePlayerType.none) playerType = ePlayerType.playerTypeMax - 1; 
        else if (playerType >= ePlayerType.playerTypeMax) playerType = ePlayerType.none + 1;
    }
    public void LeftButton() { AddPlayerType(-1); }
    public void RightButton() { AddPlayerType(1); }

    [SerializeField]
    Sprite []playerImage = new Sprite[(int)ePlayerType.playerTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    int oldPlayerType = 0;
    // Update is called once per frame
    void Update()
    {
        if (oldPlayerType == (int)playerType) return; oldPlayerType = (int)playerType;
        if(oldPlayerType == (int)ePlayerType.none) return;if (oldPlayerType == (int)ePlayerType.playerTypeMax) return;
        GetComponent<Image>().sprite = playerImage[(int)playerType];
    }

    public void DataSave() { PlayerPrefs.SetInt("playerType", (int)playerType); }
    public void DataLoad() { playerType = (ePlayerType)PlayerPrefs.GetInt("playerType"); }
    
}
