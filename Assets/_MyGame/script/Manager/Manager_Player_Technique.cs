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


    [InspectorName("")] max,
}

public enum ePlayerMagicType
{   //������ : Teleport    ������ : MagicMissile
    none,

    rubyRing, //�����@

    [InspectorName("")] max,
}

public enum ePlayerAttackType
{ //Jump    ��{���� : Space
    none,

    MeleeAttack,

    [InspectorName("")] max,
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
                if (one < (int)ePlayerTechniqueType.none) one = (int)ePlayerTechniqueType.max - 1;
                else if (one >= (int)ePlayerTechniqueType.max) one = (int)ePlayerTechniqueType.none;
                break;
            case ePlayerType.Werewolf:
                if (one < (int)ePlayerAttackType.none) one = (int)ePlayerAttackType.max - 1;
                else if (one >= (int)ePlayerAttackType.max) one = (int)ePlayerAttackType.none;
                break;
            case ePlayerType.WizardGhost:
                if (one < (int)ePlayerMagicType.none) one = (int)ePlayerMagicType.max - 1;
                else if (one >= (int)ePlayerMagicType.max) one = (int)ePlayerMagicType.none;
                break;

            case ePlayerType.playerTypeMax: break;
        }
    }
    public void OneLeftButton() { AddOneType(-1); }
    public void OneRightButton() { AddOneType(1); }

    [SerializeField]
    int two = 0;
    public int GetTwo() { return two; }
    public void SetTwo(int two_) { two = two_; }
    public void AddTwoType(int add = 1)
    {
        two = two + add;
        switch ((ePlayerType)GetComponent<Manager_Player>().GetPlayerTypeIndex())
        {
            case ePlayerType.none: break;

            case ePlayerType.PetRobot:
                if (two < (int)ePlayerTechniqueType.none) two = (int)ePlayerTechniqueType.max - 1;
                else if (two >= (int)ePlayerTechniqueType.max) two = (int)ePlayerTechniqueType.none;
                break;
            case ePlayerType.Werewolf:
                if (two < (int)ePlayerAttackType.none) two = (int)ePlayerAttackType.max - 1;
                else if (two >= (int)ePlayerAttackType.max) two = (int)ePlayerAttackType.none;
                break;
            case ePlayerType.WizardGhost:
                if (two <= (int)ePlayerMagicType.none) two = (int)ePlayerMagicType.max - 1;
                else if (two >= (int)ePlayerMagicType.max) two = (int)ePlayerMagicType.none;
                break;

            case ePlayerType.playerTypeMax: break;
        }
    }
    public void TwoLeftButton() { AddTwoType(-1); }
    public void TwoRightButton() { AddTwoType(1); }

    [SerializeField,Header("Technique")]
    Sprite[] techniqueImage = new Sprite[(int)ePlayerTechniqueType.max];
    public Sprite GetTechniqueImage(int index_) { return techniqueImage[index_]; }
    bool[] getTechnique = new bool[(int)ePlayerTechniqueType.max];
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
        DataLoad();

        oldPlayerType = GetComponent<Manager_Player>().GetPlayerTypeIndex();
    }

    // Update is called once per frame
    int oldPlayerType;
    void Update()
    {
        int playerTypeIndex = GetComponent<Manager_Player>().GetPlayerTypeIndex();
        if (oldPlayerType == playerTypeIndex) return; oldPlayerType = playerTypeIndex;
        SetOne(1);SetTwo(1);

    }
    public void DataSave()
    {
        PlayerPrefs.SetInt("playerTechniqueOne", GetOne());
        PlayerPrefs.SetInt("playerTechniqueTwo", GetTwo());

        Manager_Save.BoolSave("GetPlayerTechnique", (int)ePlayerTechniqueType.max, getTechnique);
        Manager_Save.BoolSave("GetPlayerMagic", (int)ePlayerMagicType.max, getMagic);
        Manager_Save.BoolSave("GetPlayerAttack", (int)ePlayerAttackType.max, getAttack);
    }
    public void DataLoad()
    {
        SetOne(PlayerPrefs.GetInt("playerTechniqueOne"));
        SetTwo(PlayerPrefs.GetInt("playerTechniqueTwo"));

        Manager_Save.BoolLoad("GetPlayerTechnique", (int)ePlayerTechniqueType.max, out getTechnique);
        Manager_Save.BoolLoad("GetPlayerMagic", (int)ePlayerMagicType.max, out getMagic);
        Manager_Save.BoolLoad("GetPlayerAttack", (int)ePlayerAttackType.max, out getAttack);
    }
}