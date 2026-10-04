using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum ePlayerType
{
    [InspectorName("")] none = -1,

    PetRobot,
    WizardGhost,
    WereWolf,  //近距離をメインに移動に優れたキャラ

    [InspectorName("")] max,

    //Tower → Tree, //中心記固定し移動しないキャラ、樹木、Playerを増やして移動
    //レベルアップを題材としたキャラクター
    //短距離の飛行ができるキャラ
}

public class Manager_Player : MonoBehaviour
{
    Manager_Collection manager_Collection;
    Manager_Player_Technique manager_Player_Technique;
    Manager_PlayerController manager_PlayerController;

    GameObject[] playerTypeBase;
    public GameObject GetPlayerTypeBase(int index_) {  return playerTypeBase[index_]; }
    public GameObject[] GetPlayerTypeBases() {  return playerTypeBase; }
    [SerializeField] GameObject playerTypeBase_PetRpbot;
    [SerializeField] GameObject playerTypeBase_WizardGhost;
    [SerializeField] GameObject playerTypeBase_WereWolf;
    void SetPlayerTypeBase()
    {
        playerTypeBase = new GameObject[(int)ePlayerType.max];
        for(int i = 0; i < (int)ePlayerType.max; i++) switch ((ePlayerType)i)
            {
                case ePlayerType.PetRobot:playerTypeBase[i] = playerTypeBase_PetRpbot; break;
                case ePlayerType.WizardGhost:playerTypeBase[i] = playerTypeBase_WizardGhost; break;
                case ePlayerType.WereWolf:playerTypeBase[i] = playerTypeBase_WereWolf; break;
                default: Debug.Log("SetPlayerTypeBase : " + ((ePlayerType)i)); break;

            }
    }

    RuntimeAnimatorController[] playerIconAnimaterBase;
    void InitializePlayerIconAnimaterBase() {
        playerIconAnimaterBase = new RuntimeAnimatorController[(int)ePlayerType.max];

        for (int i = 0; i < (int)ePlayerType.max; i++) switch ((ePlayerType)i)
            {
                case ePlayerType.PetRobot: playerIconAnimaterBase[i] = playerIconAnimaterBase_PetRobot; break;
                case ePlayerType.WizardGhost: playerIconAnimaterBase[i] = playerIconAnimaterBase_WizardGhost; break;
                case ePlayerType.WereWolf: playerIconAnimaterBase[i] = playerIconAnimaterBase_WereWolf; break;

                default: Debug.Log("PlayerIconAnimaterBase : " + ((ePlayerType)i)); break;
            }
    }
    [SerializeField] RuntimeAnimatorController playerIconAnimaterBase_PetRobot;
    [SerializeField] RuntimeAnimatorController playerIconAnimaterBase_WizardGhost;
    [SerializeField] RuntimeAnimatorController playerIconAnimaterBase_WereWolf;
    public RuntimeAnimatorController GetPlayerIconAnimaterBase(int index)
    {
        if (index >= playerIconAnimaterBase.Length)
        {
            Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + " : " + GetPlayerTypeBase((int)playerTypeIndex).name + " none Animator"); return null;
        }
        return playerIconAnimaterBase[index];
    }
    //getSituation
    public bool GetGetSituation(int index) { return  GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.player,index); }
    public void SetGetSituation(int index, bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.player, index, getSituation_); }
    [SerializeField]
    ePlayerType playerTypeIndex = ePlayerType.none;
    public ePlayerType GetPlayerTypeIndex() { return playerTypeIndex; }
    public void SetPlayerTypeIndex(ePlayerType playerTypeIndex_) { playerTypeIndex = playerTypeIndex_;
        //manager_Player_Technique.SetOne(1); manager_Player_Technique.SetTwo(2);
    }
    void AddPlayerTypeIndex(int add = 1)
    {
        int playerTypeNum = (int)ePlayerType.max;
        ePlayerType oldOlayerType = playerTypeIndex;
        while (true)
        {
            playerTypeIndex = playerTypeIndex + add;
            if (playerTypeIndex >= ePlayerType.max) playerTypeIndex = ePlayerType.none + 1;
            if (playerTypeIndex <= ePlayerType.none) playerTypeIndex = ePlayerType.max - 1;

            if (manager_Collection.GetGetSituation(eCollectionType.player, (int)playerTypeIndex)) break;

            playerTypeNum--;
            if (playerTypeNum < 0) { playerTypeIndex = oldOlayerType;break; }
        }
        if (playerTypeIndex < 0) playerTypeIndex = ePlayerType.max - 1;
        else if (playerTypeIndex >= ePlayerType.max) playerTypeIndex = 0;

        manager_Player_Technique.SetOne(1);
        manager_Player_Technique.AddOneType(0);
        manager_Player_Technique.SetTwo(2);
        manager_Player_Technique.AddTwoType(0);
    }
    public void PlayerTypeIndexLeftButton() { AddPlayerTypeIndex(-1); }
    public void PlayerTypeIndexRightButton() { AddPlayerTypeIndex(1); }

    [Header("PlayerMoveSpeed")]
    float[] playerSpeed = new float[(int)ePlayerType.max];
    void SetPlayerSpeed()
    {
        for(int i = 0;i < (int)ePlayerType.max; i++) switch ((ePlayerType)i)
            {
                case ePlayerType.PetRobot: playerSpeed[(int)ePlayerType.PetRobot] = petRobotTypeSpeed; break;
                case ePlayerType.WizardGhost: playerSpeed[(int)ePlayerType.WizardGhost] = wizardGhostTypeSpeed; break;
                case ePlayerType.WereWolf: playerSpeed[(int)ePlayerType.WereWolf] = werewolfTypeSpeed; break;
                default:Debug.Log("<color=red>SetPlayerSpeed : </color>" + ((ePlayerType)i)); break;
            }
    }
    [SerializeField] float petRobotTypeSpeed = 1.0f;
    [SerializeField] float wizardGhostTypeSpeed = 0.6f;
    [SerializeField] float werewolfTypeSpeed = 1.6f;
    public float GetPlayerTypeSpeed(ePlayerType playerType)
    {
        return playerSpeed[(int)playerType];
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        manager_Collection = GetComponent<Manager_Collection>();
        manager_Player_Technique = GetComponent<Manager_Player_Technique>();
        manager_PlayerController = GetComponent<Manager_PlayerController>();
        SetPlayerTypeBase();
        SetPlayerSpeed();
        InitializePlayerIconAnimaterBase();
    }
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        JoystickSelect();
    }
    void JoystickSelect()
    {

        if (SceneManager.GetActiveScene().name == "Title")
        {
            //if (manager_PlayerController.JoystickButtonDown(eJoystickButton.L1)) AddStage(-1);//todo:十字キー実装後に対応、上を押しながらでoneの、下を押しながらでtwoのテクニックを切り替え
        }
    }
    public void DataSave()
    {
        PlayerPrefs.SetInt("playerTypeIndex", (int)playerTypeIndex);
    }
    public void DataLoad()
    {
        SetPlayerTypeIndex((ePlayerType)PlayerPrefs.GetInt("playerTypeIndex"));
    }
}