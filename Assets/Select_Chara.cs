using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Chara : MonoBehaviour
{
    [SerializeField]
    ePlayerType playerType = ePlayerType.none + 1;
    public ePlayerType GetPlayerType() { return playerType; }
    void AddPlayerType(int add = 1) { 
        playerType = playerType + add;
        if (playerType <= ePlayerType.none) playerType = ePlayerType.playerTypeMax - 1; 
        else if (playerType >= ePlayerType.playerTypeMax) playerType = ePlayerType.none + 1;
    }
    public void LeftButton() { AddPlayerType(-1); }
    public void RightButton() { AddPlayerType(1); }
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DataSave() { PlayerPrefs.SetInt("playerType", (int)playerType); }
    public void DataLoad() { playerType = (ePlayerType)PlayerPrefs.GetInt("playerType"); }
    
}
