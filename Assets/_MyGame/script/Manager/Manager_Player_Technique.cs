using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eTechniqueObjectType
{
    [InspectorName("")] none = -1,

    Bullet,
    Inpact,
    Sword,

    [InspectorName("")] max,
}

public enum ePlayerWeaponType
{
    none,

    Bullet,
    EarthQuakeInpact,

    //Mirage,
    //Snipe,
    //Wire,


    [InspectorName("")] max,
}

public enum ePlayerMagicType
{   //Å©Å®Å© : Teleport    Å´Å®Å™ : MagicMissile
    none,

    rubyRing, //âäñÇñ@
    SapphireEarrings,   //êÖñÇñ@

    [InspectorName("")] max,
}

public enum ePlayerAttackType
{ //Jump    äÓñ{ìÆçÏ : Space
    none,

    MeleeAttack,

    [InspectorName("")] max,
}

public class Manager_Player_Technique : MonoBehaviour
{
    Manager_Player manager_Player;

    [Header("TechniqueIndex")]
    [SerializeField]
    int one = 0;
    public int GetOne() { return one; }
    public void SetOne(int one_) { one = one_; AddOneType(0); }
    public void AddOneType(int add = 1)
    {
        one = one + add;
        switch ((ePlayerType)GetComponent<Manager_Player>().GetPlayerTypeIndex())
        {
            case ePlayerType.none: break;

            case ePlayerType.PetRobot:
                if (one < (int)ePlayerWeaponType.none) one = (int)ePlayerWeaponType.max - 1;
                else if (one >= (int)ePlayerWeaponType.max) one = (int)ePlayerWeaponType.none;
                break;
            case ePlayerType.Werewolf:
                if (one < (int)ePlayerAttackType.none) one = (int)ePlayerAttackType.max - 1;
                else if (one >= (int)ePlayerAttackType.max) one = (int)ePlayerAttackType.none;
                break;
            case ePlayerType.WizardGhost:
                if (one < (int)ePlayerMagicType.none) one = (int)ePlayerMagicType.max - 1;
                else if (one >= (int)ePlayerMagicType.max) one = (int)ePlayerMagicType.none;
                break;

            case ePlayerType.max: break;
        }
    }
    public void OneLeftButton() { AddOneType(-1); }
    public void OneRightButton() { AddOneType(1); }

    [SerializeField]
    int two = 0;
    public int GetTwo() { return two; }
    public void SetTwo(int two_) { two = two_; AddTwoType(0); }
    public void AddTwoType(int add = 1)
    {
        two = two + add;
        switch ((ePlayerType)GetComponent<Manager_Player>().GetPlayerTypeIndex())
        {
            case ePlayerType.none: break;

            case ePlayerType.PetRobot:
                if (two < (int)ePlayerWeaponType.none) two = (int)ePlayerWeaponType.max - 1;
                else if (two >= (int)ePlayerWeaponType.max) two = (int)ePlayerWeaponType.none;
                break;
            case ePlayerType.Werewolf:
                if (two < (int)ePlayerAttackType.none) two = (int)ePlayerAttackType.max - 1;
                else if (two >= (int)ePlayerAttackType.max) two = (int)ePlayerAttackType.none;
                break;
            case ePlayerType.WizardGhost:
                if (two <= (int)ePlayerMagicType.none) two = (int)ePlayerMagicType.max - 1;
                else if (two >= (int)ePlayerMagicType.max) two = (int)ePlayerMagicType.none;
                break;

            case ePlayerType.max: break;
        }
    }
    public void TwoLeftButton() { AddTwoType(-1); }
    public void TwoRightButton() { AddTwoType(1); }

    [SerializeField,Header("Weapon")]
    Sprite[] WeaponImage = new Sprite[(int)ePlayerWeaponType.max];
    public Sprite GetWeaponImage(int index_) { return WeaponImage[index_]; }
    bool[] getWeapon = new bool[(int)ePlayerWeaponType.max];
    [SerializeField]
    GameObject []techniqueBase;
    public GameObject GetTechniqueBase(int index_) { return techniqueBase[index_]; }
    [SerializeField]
    GameObject []techniqueUIBase;
    public GameObject GetTechniqueUIBase(int index_) { return techniqueUIBase[index_]; }
    [SerializeField,Header("Magic")]
    Sprite[] magicImage = new Sprite[(int)ePlayerMagicType.max];
    public Sprite GetMagicImage(int index_) { return magicImage[index_]; }
    bool[] getMagic = new bool[(int)ePlayerMagicType.max];
    [SerializeField,Header("Attack")]
    Sprite[] attackImage = new Sprite[(int)ePlayerAttackType.max];
    public Sprite GetAttackImage(int index_) { return attackImage[index_]; }
    bool[] getAttack = new bool[(int)ePlayerAttackType.max];


    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GetComponent<Manager_Player>();

        oldPlayerType = (int)manager_Player.GetPlayerTypeIndex();
    }

    // Update is called once per frame
    int oldPlayerType;
    void Update()
    {
        int playerTypeIndex = (int)manager_Player.GetPlayerTypeIndex();
        if (oldPlayerType == playerTypeIndex) return; oldPlayerType = playerTypeIndex;
        SetOne(1);SetTwo(2); 

    }
    public void DataSave()
    {
        PlayerPrefs.SetInt("playerTechniqueOne", GetOne());
        PlayerPrefs.SetInt("playerTechniqueTwo", GetTwo());

        Manager_Save.BoolSave("GetPlayerWeapon", (int)ePlayerWeaponType.max, getWeapon);
        Manager_Save.BoolSave("GetPlayerMagic", (int)ePlayerMagicType.max, getMagic);
        Manager_Save.BoolSave("GetPlayerAttack", (int)ePlayerAttackType.max, getAttack);
    }
    public void DataLoad()
    {
        SetOne(PlayerPrefs.GetInt("playerTechniqueOne"));
        SetTwo(PlayerPrefs.GetInt("playerTechniqueTwo"));

        Manager_Save.BoolLoad("GetPlayerWeapon", (int)ePlayerWeaponType.max, out getWeapon);
        Manager_Save.BoolLoad("GetPlayerMagic", (int)ePlayerMagicType.max, out getMagic);
        Manager_Save.BoolLoad("GetPlayerAttack", (int)ePlayerAttackType.max, out getAttack);
    }
}