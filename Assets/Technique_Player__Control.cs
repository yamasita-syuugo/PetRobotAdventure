using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class Technique_Player__Control : MonoBehaviour
{
    [SerializeField]
    int techniqueNum = 2;
    [SerializeField]
    ePlayerTechniqueType[] use = new ePlayerTechniqueType[3];
    [SerializeField,Header("0 = none;1 = Bullet;2 = EarthQuake;3 = MeleeAttack;4 = Mirage")]
    GameObject[] techniqueBase = new GameObject[(int)ePlayerTechniqueType.playerTechniqueTypeMax]; 
    GameObject[] technique = new GameObject[3]; 
    // Start is called before the first frame update
    void Start()
    {
        CreateTechniqueAndUI();
    }
    [SerializeField]
    GameObject[] techniqueUIBase = new GameObject[(int)ePlayerTechniqueType.playerTechniqueTypeMax];
    GameObject[] techniqueUI = new GameObject[3];
    void CreateTechniqueAndUI()
    {
        GameObject playerUIParent = GameObject.Find("PlayerUI");
        for (int i = 0; i < techniqueNum; i++)
        {
            technique[i] = Instantiate<GameObject>(techniqueBase[(int)use[i]]);

            technique[i].transform.parent = transform;
            technique[i].transform.localPosition = Vector3.zero;

            techniqueUI[i] = Instantiate<GameObject>(techniqueUIBase[(int)use[i]]);

            techniqueUI[i].GetComponent<UI_Display__Base>().SetConnectTechnique(technique[i]);
            techniqueUI[i].transform.parent = playerUIParent.transform;
            techniqueUI[i].transform.localScale = Vector3.one;
            techniqueUI[i].transform.localPosition = playerUIParent.transform.position - new Vector3(0, 45, 0) * i;

        }
    }

    // Update is called once per frame
    void Update()
    {
        UseSummary();
    }

    public void GetPoint()
    {
        for(int i = 0;i < techniqueNum;i++)
        {
            technique[i].GetComponent<Technique_Player__ContainerBase>().GetPoint();
        }
    }

    void UseSummary()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        for (int i = 0;i < techniqueNum;i++)//0 = 左クリック;1 = 右クリック;2 = ホイールクリック;
        {
        UseSelect_Mouse(i);

        UseSelect_Controller(i);

        }
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
            case ePlayerTechniqueType.Bullet: pushType = ePushType.down; break;
            case ePlayerTechniqueType.MeleeAttack: pushType = ePushType.down; break;
            case ePlayerTechniqueType.EarthQuakeInpact: pushType = ePushType.down; break;

            case ePlayerTechniqueType.Mirage: pushType = ePushType.down; break;
        }
    }
    void UseSelect_Mouse(int useNum)
    {
        bool situation = false;
        PushTypeCheck(useNum);
        switch (pushType)
        {
            case ePushType.down: situation = Input.GetMouseButtonDown(useNum); break;
            case ePushType.stey: situation = Input.GetMouseButton(useNum); break;
            case ePushType.up: situation = Input.GetMouseButtonUp(useNum); break;
        }

        if (!situation) return;

        switch (use[useNum])
        {
            case ePlayerTechniqueType.Bullet:
                technique[useNum].GetComponent<Technique_Player_BulletShot>().MouseShot();
                break;
            case ePlayerTechniqueType.MeleeAttack:
                technique[useNum].GetComponent<Technique_Player_MeleeAttack>().MouseMeleeAttack();
                    break;
            case ePlayerTechniqueType.EarthQuakeInpact:
                technique[useNum].GetComponent<Technique_Player_EarthQuake>().MouseCreatBlock();
                break;


            case ePlayerTechniqueType.Mirage: break;
        }
    }
    int ControllerShotButton;
    bool []techniqueTrigger = new bool[3];
    void UseSelect_Controller(int useNum)
    {
        bool situation = false;
        string keyString = "Error";
        switch (useNum)
        {
            case 0: keyString = "joystick button 7"; break;
            case 1: keyString = "joystick button 5"; break;
            case 2: keyString = "joystick button 6"; break;
        }
        PushTypeCheck(useNum);
        switch (pushType)
        {
            case ePushType.down: situation = Input.GetKeyDown(keyString); break;
            case ePushType.stey: situation = Input.GetKey(keyString); break;
            case ePushType.up: situation = Input.GetKeyUp(keyString); break;
        }

        if (!situation) return;

        switch (use[useNum])
        {
            case ePlayerTechniqueType.Bullet:
                technique[useNum].GetComponent<Technique_Player_BulletShot>().SetControllerShot();
                break;
            case ePlayerTechniqueType.MeleeAttack:
                technique[useNum].GetComponent<Technique_Player_MeleeAttack>().ControllerMeleeAttack();
                break;
            case ePlayerTechniqueType.EarthQuakeInpact:
                technique[useNum].GetComponent<Technique_Player_EarthQuake>().ControllerCreatBlock();
                break;

            case ePlayerTechniqueType.Mirage: break;
        }

    }
}
