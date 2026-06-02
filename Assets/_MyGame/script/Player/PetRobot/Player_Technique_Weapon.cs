using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Player_Technique_Weapon : Player_Technique_
{
    Manager_PlayerController manager_PlayerController;
    Manager_Player_Technique manager_Player_Technique;

    [SerializeField]
    const int weaponNum = (int)eTechnicControl.max;
    [SerializeField]
    int[] use = new int[(int)eTechnicControl.max];
    [Header("0 = none; 1 = Bullet; 2 = EarthQuake; 3 = MeleeAttack; 4 = Mirage")]
    GameObject[] weapon = new GameObject[(int)eTechnicControl.max]; 
    GameObject[] weaponUI = new GameObject[(int)eTechnicControl.max];

    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_PlayerController = manager.GetComponent<Manager_PlayerController>();
        manager_Player_Technique = manager.GetComponent<Manager_Player_Technique>();
    }
    // Start is called before the first frame update
    void Start()
    {
        use[(int)eTechnicControl.one] = manager_Player_Technique.GetOne();
        use[(int)eTechnicControl.two] = manager_Player_Technique.GetTwo();

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
            weapon[i].GetComponent<Player_Technique_Container_Base>().GetPoint();
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
            case ePushType.down: situation = manager_PlayerController.GetTechnicMouse(ePushType.down,(eTechnicControl)useNum); break;
            case ePushType.stey: situation = manager_PlayerController.GetTechnicMouse(ePushType.stey, (eTechnicControl)useNum); break;
            case ePushType.up: situation = manager_PlayerController.GetTechnicMouse(ePushType.up, (eTechnicControl)useNum); break;
        }

        if (!situation) return;

        weapon[useNum].GetComponent<Player_Technique_Play_Base>().MousePlay();
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
            case ePushType.down: situation = manager_PlayerController.GetTechnicPad(ePushType.down, (eTechnicControl)useNum); break;
            case ePushType.stey: situation = manager_PlayerController.GetTechnicPad(ePushType.stey, (eTechnicControl)useNum); break;
            case ePushType.up: situation = manager_PlayerController.GetTechnicPad(ePushType.up, (eTechnicControl)useNum); break;
        }

        if (!situation) return;
        weapon[useNum].GetComponent<Player_Technique_Play_Base>().ControllerPlay();
    }
}
