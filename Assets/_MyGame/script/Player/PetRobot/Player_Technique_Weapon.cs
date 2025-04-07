using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

enum eWeaponControl
{
    [InspectorName("")] none = -1,

    one,    //左クリック
    two,    //右クリック

    [InspectorName("")] max,
}

public class Player_Technique_Weapon : Player_Technique_
{
    Manager_Player_Technique manager_Player_Technique;

    [SerializeField]
    const int weaponNum = (int)eWeaponControl.max;
    [SerializeField]
    int[] use = new int[(int)eWeaponControl.max];
    [Header("0 = none; 1 = Bullet; 2 = EarthQuake; 3 = MeleeAttack; 4 = Mirage")]
    GameObject[] weapon = new GameObject[(int)eWeaponControl.max]; 
    GameObject[] weaponUI = new GameObject[(int)eWeaponControl.max];
    // Start is called before the first frame update
    void Start()
    {
        manager_Player_Technique = GameObject.FindWithTag("Manager").GetComponent<Manager_Player_Technique>();
        use[(int)eWeaponControl.one] = manager_Player_Technique.GetOne();
        use[(int)eWeaponControl.two] = manager_Player_Technique.GetTwo();

        CreateTechniqueAndUI();
    }
    void CreateTechniqueAndUI()
    {

        GameObject playerUIParent = GameObject.Find("PlayerUI");
        for (int i = 0; i < weaponNum; i++)
        {
            GameObject useTechnique = manager_Player_Technique.GetTechniqueBase(use[i]);
            if (useTechnique == null) continue;
            weapon[i] = Instantiate<GameObject>(useTechnique);

            weapon[i].transform.parent = transform;
            weapon[i].transform.localPosition = Vector3.zero;

            GameObject useTechniqueUI = manager_Player_Technique.GetTechniqueUIBase(use[i]);

            weaponUI[i] = Instantiate<GameObject>(useTechniqueUI);

            weaponUI[i].GetComponent<UI_Display__Base>().SetConnectTechnique(weapon[i]);
            weaponUI[i].transform.parent = playerUIParent.transform;
            weaponUI[i].transform.localScale = Vector3.one;
            weaponUI[i].transform.localPosition = playerUIParent.transform.position - new Vector3(0, 45, 0) * i;

        }
    }

    // Update is called once per frame
    void Update()
    {
        UseSummary();
    }

    override public void GetPoint()
    {
        for(int i = 0;i < weaponNum; i++)
        {
            if (weapon[i] == null) continue;
            weapon[i].GetComponent<Player_Technique_Container__Base>().GetPoint();
        }
    }

    void UseSummary()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        for (int i = 0; i < weaponNum; i++)//0 = 左クリック;1 = 右クリック;
        {
            UseSelect_Mouse(i);
            UseSelect_Controller(i);
        }
    }

    void UseSelect_Mouse(int useNum)
    {
        if (weapon[useNum] == null) return;

        bool situation = false;
        PushTypeCheck(useNum);
        switch (pushType)
        {
            case ePushType.down: situation = Input.GetMouseButtonDown(useNum); break;
            case ePushType.stey: situation = Input.GetMouseButton(useNum); break;
            case ePushType.up: situation = Input.GetMouseButtonUp(useNum); break;
        }

        if (!situation) return;

        weapon[useNum].GetComponent<Player_Technique_Play__Base>().MousePlay();
    }
    enum ePushType
    {
        down,
        stey,
        up,
    }
    ePushType pushType = ePushType.down;
    void PushTypeCheck(int useNum)
    {
        switch (use[useNum])
        {
            case (int)ePlayerWeaponType.none: break;

            case (int)ePlayerWeaponType.Bullet: pushType = ePushType.down; break;
            case (int)ePlayerWeaponType.EarthQuakeInpact: pushType = ePushType.down; break;

            //case ePlayerTechniqueType.Mirage: pushType = ePushType.down; break;
            //case ePlayerTechniqueType.MeleeAttack: pushType = ePushType.down; break;
            default:Debug.Log("PushTypeCheck " + useNum + ": error");break;
        }
    }
    int ControllerShotButton;
    bool []techniqueTrigger = new bool[3];
    void UseSelect_Controller(int useNum)
    {
        bool situation = false;
        string keyString = "Error";
        keyString = "joystick button " + (5 + useNum * 2).ToString() ;

        PushTypeCheck(useNum);
        switch (pushType)
        {
            case ePushType.down: situation = Input.GetKeyDown(keyString); break;
            case ePushType.stey: situation = Input.GetKey(keyString); break;
            case ePushType.up: situation = Input.GetKeyUp(keyString); break;
        }

        if (!situation) return;
        weapon[useNum].GetComponent<Player_Technique_Play__Base>().ControllerPlay();
    }
}
