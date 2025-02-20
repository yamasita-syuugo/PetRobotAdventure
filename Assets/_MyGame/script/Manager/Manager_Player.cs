using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public enum ePlayerType
{
    [InspectorName("")] none = -1,

    PetRobot,
    WizardGhost,
    WereWolf,  //近距離をメインに移動に優れたキャラ
    //Tower, //中心記固定し移動しないキャラ
    //レベルアップを題材としたキャラクター
    //短距離の飛行ができるキャラ

    [InspectorName("")] max,
}

public class Manager_Player : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerTypeBase;
    public GameObject GetPlayerTypeBase(int index_) {  return playerTypeBase[index_]; }
    public GameObject[] GetPlayerTypeBases() {  return playerTypeBase; }
    [SerializeField]
    RuntimeAnimatorController[] playerIconAnimaterBase;
    void InitializePlayerIconAnimaterBase() { }
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
    public void SetPlayerTypeIndex(ePlayerType playerTypeIndex_) { playerTypeIndex = playerTypeIndex_; }
    void AddPlayerTypeIndex(int add = 1)
    {
        playerTypeIndex = playerTypeIndex + add;
        if (playerTypeIndex < 0) playerTypeIndex = ePlayerType.max - 1;
        else if (playerTypeIndex >= ePlayerType.max) playerTypeIndex = 0;

        Manager_Player_Technique manager_Player_Technique = GetComponent<Manager_Player_Technique>();
        manager_Player_Technique.SetOne(1);
        manager_Player_Technique.AddOneType(0);
        manager_Player_Technique.SetTwo(1);
        manager_Player_Technique.AddTwoType(0);
    }
    public void PlayerTypeIndexLeftButton() { AddPlayerTypeIndex(-1); }
    public void PlayerTypeIndexRightButton() { AddPlayerTypeIndex(1); }

    [Header("PlayerMoveSpeed")]
    float[] playerSpeed = new float[(int)ePlayerType.max];
    void SetPlayerSpeed()
    {
        playerSpeed[(int)ePlayerType.PetRobot] = petRobotTypeSpeed;
        playerSpeed[(int)ePlayerType.WizardGhost] = wizardGhostTypeSpeed;
        playerSpeed[(int)ePlayerType.WereWolf] = werewolfTypeSpeed;
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
        SetPlayerSpeed();
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
        PlayerPrefs.SetInt("playerTypeIndex", (int)playerTypeIndex);
    }
    public void DataLoad()
    {
        SetPlayerTypeIndex((ePlayerType)PlayerPrefs.GetInt("playerTypeIndex"));
    }
}