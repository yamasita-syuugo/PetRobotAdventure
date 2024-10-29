using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerTechniqueType
{
    none,

    Bullet,
    EarthQuakeInpact,

    //Mirage,
    //Snipe,
    //Wire,


    [InspectorName("")] playerTechniqueTypeMax,
}

public enum ePlayerMagicType
{
    none,

    rubyRing, //âäñÇñ@

    [InspectorName("")] playerMagicMax,
}

public enum ePlayerAttackType
{ //Jump    äÓñ{ìÆçÏ : Space
    none,

    MeleeAttack,

    [InspectorName("")] playerAttackMax,
}

public class Manager_Player_Technique : MonoBehaviour
{

    [Header("TechniqueIndex")]
    [SerializeField]
    int one = 0;
    public int GetOne() { return one; }
    public void SetOne(int one_) { one = one_; }
    public void AddOneType(int add = 1)
    {
        one = one + add;
        switch ((ePlayerType)GetComponent<Manager_Player>().GetPlayerTypeIndex())
        {
            case ePlayerType.none: break;

            case ePlayerType.PetRobot:
                if (one < (int)ePlayerTechniqueType.none) one = (int)ePlayerTechniqueType.playerTechniqueTypeMax - 1;
                else if (one >= (int)ePlayerTechniqueType.playerTechniqueTypeMax) one = (int)ePlayerTechniqueType.none;
                break;
            case ePlayerType.Werewolf:
                if (one < (int)ePlayerAttackType.none) one = (int)ePlayerAttackType.playerAttackMax - 1;
                else if (one >= (int)ePlayerAttackType.playerAttackMax) one = (int)ePlayerAttackType.none;
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
    int two = -1;
    public int GetTwo() { return two; }
    public void SetTwo(int two_) { two = two_; }
    public void AddTwoType(int add = 1)
    {
        two = two + add;
        switch ((ePlayerType)GetComponent<Manager_Player>().GetPlayerTypeIndex())
        {
            case ePlayerType.none: break;

            case ePlayerType.PetRobot:
                if (two < (int)ePlayerTechniqueType.none) two = (int)ePlayerTechniqueType.playerTechniqueTypeMax - 1;
                else if (two >= (int)ePlayerTechniqueType.playerTechniqueTypeMax) two = (int)ePlayerTechniqueType.none;
                break;
            case ePlayerType.Werewolf:
                if (two < (int)ePlayerAttackType.none) two = (int)ePlayerAttackType.playerAttackMax - 1;
                else if (two >= (int)ePlayerAttackType.playerAttackMax) two = (int)ePlayerAttackType.none;
                break;
            case ePlayerType.WizardGhost:
                if (two <= (int)ePlayerMagicType.none) two = (int)ePlayerMagicType.playerMagicMax - 1;
                else if (two >= (int)ePlayerMagicType.playerMagicMax) two = (int)ePlayerMagicType.none;
                break;

            case ePlayerType.playerTypeMax: break;
        }
    }
    public void TwoLeftButton() { AddTwoType(-1); }
    public void TwoRightButton() { AddTwoType(1); }

    [SerializeField,Header("Technique")]
    Sprite[] techniqueImage = new Sprite[(int)ePlayerTechniqueType.playerTechniqueTypeMax];
    public Sprite GetTechniqueImage(int index_) { return techniqueImage[index_]; }
    bool[] getTechnique = new bool[(int)ePlayerTechniqueType.playerTechniqueTypeMax];
    [SerializeField]
    GameObject []techniqueBase;
    public GameObject GetTechniqueBase(int index_) { return techniqueBase[index_]; }
    [SerializeField]
    GameObject []techniqueUIBase;
    public GameObject GetTechniqueUIBase(int index_) { return techniqueUIBase[index_]; }
    [SerializeField,Header("Magic")]
    Sprite[] magicImage = new Sprite[(int)ePlayerMagicType.playerMagicMax];
    public Sprite GetMagicImage(int index_) { return magicImage[index_]; }
    bool[] getMagic = new bool[(int)ePlayerMagicType.playerMagicMax];
    [SerializeField,Header("Attack")]
    Sprite[] attackImage = new Sprite[(int)ePlayerAttackType.playerAttackMax];
    public Sprite GetAttackImage(int index_) { return attackImage[index_]; }
    bool[] getAttack = new bool[(int)ePlayerAttackType.playerAttackMax];
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();


    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    public void DataSave()
    {
        PlayerPrefs.SetInt("playerTechniqueOne", GetOne());
        PlayerPrefs.SetInt("playerTechniqueTwo", GetTwo());

        Manager_Save.BoolSave("GetPlayerTechnique", (int)ePlayerTechniqueType.playerTechniqueTypeMax, getTechnique);
        Manager_Save.BoolSave("GetPlayerMagic", (int)ePlayerMagicType.playerMagicMax, getMagic);
        Manager_Save.BoolSave("GetPlayerAttack", (int)ePlayerAttackType.playerAttackMax, getAttack);
    }
    public void DataLoad()
    {
        SetOne(PlayerPrefs.GetInt("playerTechniqueOne"));
        SetTwo(PlayerPrefs.GetInt("playerTechniqueTwo"));

        Manager_Save.BoolLoad("GetPlayerTechnique", (int)ePlayerTechniqueType.playerTechniqueTypeMax, out getTechnique);
        Manager_Save.BoolLoad("GetPlayerMagic", (int)ePlayerMagicType.playerMagicMax, out getMagic);
        Manager_Save.BoolLoad("GetPlayerAttack", (int)ePlayerAttackType.playerAttackMax, out getAttack);
    }
}