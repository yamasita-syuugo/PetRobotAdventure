using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerType
{
    [InspectorName("")] none = -1,

    PetRobot,
    WizardGhost,
    Werewolf,  //近距離をメインに移動に優れたキャラ
    //Tower, //中心記固定し移動しないキャラ
    //レベルアップを題材としたキャラクター
    //短距離の飛行ができるキャラ

    [InspectorName("")] playerTypeMax,
}

public class Manager_Player : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerTypeBase;
    public GameObject GetPlayerTypeBase(int index_) {  return playerTypeBase[index_]; }
    public GameObject[] GetPlayerTypeBases() {  return playerTypeBase; }
    [SerializeField]
    bool[] getSituation = new bool[16];
    public bool GetGetSituation(int index) { return getSituation[index]; }
    public void SetGetSituation(int index, bool getSituation_) { getSituation[index] = getSituation_; }
    [SerializeField]
    int playerTypeIndex = 0;
    public int GetPlayerTypeIndex() { return playerTypeIndex; }
    public void SetPlayerTypeIndex(int playerTypeIndex_) { playerTypeIndex = playerTypeIndex_; }
    void AddPlayerTypeIndex(int add = 1)
    {
        playerTypeIndex = playerTypeIndex + add;
        if (playerTypeIndex < 0) playerTypeIndex = (int)ePlayerType.playerTypeMax - 1;
        else if (playerTypeIndex >= (int)ePlayerType.playerTypeMax) playerTypeIndex = 0;

        Manager_Player_Technique manager_Player_Technique = GetComponent<Manager_Player_Technique>();
        manager_Player_Technique.SetOne(1);
        manager_Player_Technique.AddOneType(0);
        manager_Player_Technique.SetTwo(1);
        manager_Player_Technique.AddTwoType(0);
    }
    public void PlayerTypeIndexLeftButton() { AddPlayerTypeIndex(-1); }
    public void PlayerTypeIndexRightButton() { AddPlayerTypeIndex(1); }

    [Header("PlayerMoveSpeed")]
    [SerializeField] float petRobotTypeSpeed = 1.0f;
    [SerializeField] float wizardGhostTypeSpeed = 0.6f;
    [SerializeField] float werewolfTypeSpeed = 1.6f;
    public float GetPlayerTypeSpeed(ePlayerType playerType)
    {
        switch ((ePlayerType)playerTypeIndex)
        {
            case ePlayerType.PetRobot: return petRobotTypeSpeed; break;
            case ePlayerType.WizardGhost: return wizardGhostTypeSpeed; break;
            //case ePlayerType.Werewolf: return werewolfTypeSpeed; break;
            default: return 1.0f;
        }
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        DataLoad();
    }
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void DataSave()
    {
        PlayerPrefs.SetInt("playerTypeIndex", playerTypeIndex);
        Manager_Save.BoolSave("PlayerGetSituation", playerTypeBase.Length, getSituation);
    }
    public void DataLoad()
    {
        SetPlayerTypeIndex(PlayerPrefs.GetInt("playerTypeIndex"));
        Manager_Save.BoolLoad("PlayerGetSituation", playerTypeBase.Length, out getSituation);
    }
}