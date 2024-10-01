using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Chara : MonoBehaviour
{
    [SerializeField]
    ePlayerType playerType = ePlayerType.none + 1;
    public ePlayerType GetPlayerType() { return playerType; }
    public void SetPlayerType(ePlayerType playerType_) { playerType = playerType_; }
    void AddPlayerType(int add = 1)
    {
        playerType = playerType + add;
        if (playerType <= ePlayerType.none) playerType = ePlayerType.playerTypeMax - 1;
        else if (playerType >= ePlayerType.playerTypeMax) playerType = ePlayerType.none + 1;

        SetOne(1);
        SetTwo(1);
    }
    public void CharaLeftButton() { AddPlayerType(-1); }
    public void CharaRightButton() { AddPlayerType(1); }

    [SerializeField]
    int one = 1;
    public int GetOne() {  return one; }
    void SetOne(int one_) { one = one_; }
    void AddOneType(int add = 1)
    {
        one = one + add;
        switch (playerType)
        {
            case ePlayerType.none: break;

            case ePlayerType.PetRobot:
                if (one < (int)ePlayerTechniqueType.none) one = (int)ePlayerTechniqueType.playerTechniqueTypeMax - 1;
                else if (one >= (int)ePlayerTechniqueType.playerTechniqueTypeMax) one = (int)ePlayerTechniqueType.none;
                break;
            case ePlayerType.WizardGhost:
                if (one < (int)ePlayerMagicType.none) one = (int)ePlayerMagicType.playerMagicMax - 1;
                else if (one >= (int)ePlayerMagicType.playerMagicMax) one = (int)ePlayerMagicType.none;
                break;

            case ePlayerType.playerTypeMax: break;
        }
    }
    public void OneLeftButton() { AddOneType(-1); }
    public void OneRightButton() { AddOneType(1); }

    [SerializeField]
    int two = 1;
    public int GetTwo() { return two; }
    void SetTwo(int two_) { two = two_; }
    void AddTwoType(int add = 1)
    {
        two = two + add;
        switch (playerType)
        {
            case ePlayerType.none: break;

            case ePlayerType.PetRobot:
                if (two < (int)ePlayerTechniqueType.none) two = (int)ePlayerTechniqueType.playerTechniqueTypeMax - 1;
                else if (two >= (int)ePlayerTechniqueType.playerTechniqueTypeMax) two = (int)ePlayerTechniqueType.none;
                break;
            case ePlayerType.WizardGhost:
                if (two <= (int)ePlayerMagicType.none) two = (int)ePlayerMagicType.playerMagicMax - 1;
                else if (two >= (int)ePlayerMagicType.playerMagicMax) two = (int)ePlayerMagicType.none + 1;
                break;

            case ePlayerType.playerTypeMax: break;
        }
    }
    public void TwoLeftButton() { AddTwoType(-1); }
    public void TwoRightButton() { AddTwoType(1); }

    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void DataSave() { 
        PlayerPrefs.SetInt("playerType", (int)GetPlayerType()); 
        PlayerPrefs.SetInt("playerTechniqueOne", GetOne()); 
        PlayerPrefs.SetInt("playerTechniqueTwo", GetTwo()); 
    }
    public void DataLoad() { 
        SetPlayerType((ePlayerType)PlayerPrefs.GetInt("playerType"));
        SetOne(PlayerPrefs.GetInt("playerTechniqueOne")); 
        SetTwo(PlayerPrefs.GetInt("playerTechniqueTwo")); 
    }
}
