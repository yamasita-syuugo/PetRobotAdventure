using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

enum eTechniqueControl
{
    [InspectorName("")] none = -1,

    one,    //左クリック
    two,    //右クリック

    [InspectorName("")] techniqueControlMax,
}

public class Player_Technique__Control : MonoBehaviour
{
    Manager_Player_Technique manager_Player_Technique;

    [SerializeField]
    const int techniqueNum = (int)eTechniqueControl.techniqueControlMax;
    [SerializeField]
    int[] use = new int[(int)eTechniqueControl.techniqueControlMax];
    [Header("0 = none;1 = Bullet;2 = EarthQuake;3 = MeleeAttack;4 = Mirage")]
    GameObject[] technique = new GameObject[(int)eTechniqueControl.techniqueControlMax]; 
    GameObject[] techniqueUI = new GameObject[(int)eTechniqueControl.techniqueControlMax];
    // Start is called before the first frame update
    void Start()
    {
        manager_Player_Technique = GameObject.FindWithTag("Manager").GetComponent<Manager_Player_Technique>();
        use[(int)eTechniqueControl.one] = manager_Player_Technique.GetOne();
        use[(int)eTechniqueControl.two] = manager_Player_Technique.GetTwo();

        CreateTechniqueAndUI();
    }
    void CreateTechniqueAndUI()
    {

        GameObject playerUIParent = GameObject.Find("PlayerUI");
        for (int i = 0; i < techniqueNum; i++)
        {
            GameObject useTechnique = manager_Player_Technique.GetTechniqueBase(use[i]);

            technique[i] = Instantiate<GameObject>(useTechnique);

            technique[i].transform.parent = transform;
            technique[i].transform.localPosition = Vector3.zero;

            GameObject useTechniqueUI = manager_Player_Technique.GetTechniqueUIBase(use[i]);

            techniqueUI[i] = Instantiate<GameObject>(useTechniqueUI);

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
            if (technique[i] == null) continue;
            technique[i].GetComponent<Player_Technique_Container__Base>().GetPoint();
        }
    }

    void UseSummary()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        for (int i = 0; i < techniqueNum; i++)//0 = 左クリック;1 = 右クリック;2 = ホイールクリック;
        {
            UseSelect_Mouse(i);
            //UseSelect_Controller(i);
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
            case (int)ePlayerTechniqueType.none: break;

            case (int)ePlayerTechniqueType.Bullet: pushType = ePushType.down; break;
            case (int)ePlayerTechniqueType.EarthQuakeInpact: pushType = ePushType.down; break;

            //case ePlayerTechniqueType.Mirage: pushType = ePushType.down; break;
            //case ePlayerTechniqueType.MeleeAttack: pushType = ePushType.down; break;
            default:Debug.Log("PushTypeCheck " + useNum + ": error");break;
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

        technique[useNum].GetComponent<Player_Technique_Play__Base>().MousePlay();
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
        technique[useNum].GetComponent<Player_Technique_Play__Base>().ControllerPlay();
    }
}
