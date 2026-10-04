using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Technique_Magic : Player_Technique_
{
    Manager_PlayerController manager_PlayerController;

    [SerializeField]
    GameObject magicCircleBase;
    GameObject magicCircle;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_PlayerController = manager.GetComponent<Manager_PlayerController>();
        magicCircle = Instantiate(magicCircleBase);

        CreateAimMark();
    }

    // Update is called once per frame
    int chanting = 0;
    void Update()
    {
        if (transform.parent.GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        Magic__Base();
    }

    bool standby = false;
    bool []transferCheck = new bool[(int)eDirecttion.max];
    float stickInclinationCheck = 0.75f;
    int magicUseNum = 0;
    private void Magic__Base()
    {
        bool technicOn = false;
        for (int i = 0; i < Manager_PlayerController.padNumMax; i++)
        {
            if (manager_PlayerController.GetTechnicPad(ePushType.stey, eTechnicControl.one) || manager_PlayerController.GetTechnicPad(ePushType.stey, eTechnicControl.two))
            { technicOn = true; break; }
        }
        if (Input.GetKey(KeyCode.Mouse0) ||technicOn)
        {
            magicCircle.SetActive(true);
            magicCircle.transform.position = transform.position;

            if (standby)
            {
                transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.chanting);

                Vector2 transfer = manager_PlayerController.GetTransfer();
                for (int i = 0; i < (int)eDirecttion.max; i++)
                {
                    switch ((eDirecttion)i)
                    {
                        case eDirecttion.north:
                            if (transfer.y >= stickInclinationCheck) { if (transferCheck[i] == false) { transferCheck[i] = true; chanting = chanting * 10 + (1 + i); } }
                            else transferCheck[i] = false;
                            break;
                        case eDirecttion.west:
                            if (transfer.x <= -stickInclinationCheck) { if (transferCheck[i] == false) { transferCheck[i] = true; chanting = chanting * 10 + (1 + i); } }
                            else transferCheck[i] = false;
                            break;
                        case eDirecttion.south:
                            if (transfer.y <= -stickInclinationCheck) { if (transferCheck[i] == false) { transferCheck[i] = true; chanting = chanting * 10 + (1 + i); } }
                            else transferCheck[i] = false;
                            break;
                        case eDirecttion.east:
                            if (transfer.x >= stickInclinationCheck) { if (transferCheck[i] == false) { transferCheck[i] = true; chanting = chanting * 10 + (1 + i); } }
                            else transferCheck[i] = false;
                            break;
                    }
                }

                //if (Input.GetKeyDown(KeyCode.W)) chanting = chanting * 10 + 1;
                //if (Input.GetKeyDown(KeyCode.A)) chanting = chanting * 10 + 2;
                //if (Input.GetKeyDown(KeyCode.S)) chanting = chanting * 10 + 3;
                //if (Input.GetKeyDown(KeyCode.D)) chanting = chanting * 10 + 4;

                if (chanting > 500000) standby = false;
            }
            else transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.normal);
        }
        else
        {
            if (chanting != 0) Debug.Log(magicUseNum++ + " - マジックコード : " + chanting);
            magicCircle.SetActive(false);

            standby = true;

            switch (chanting)
            {
                case 0: transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.normal); break;
                case 1: Magic_(); break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 14: break;
                case 341:
                    if (Input.GetKeyUp(KeyCode.Mouse0)) Shot_Mouse();
                    if (manager_PlayerController.JoystickButtonUp(eJoystickButton.R1) || manager_PlayerController.JoystickButtonUp(eJoystickButton.R2))
                        Shot((aimMark.transform.position - transform.position));
                    break;
                case 242:
                    if (Input.GetKeyUp(KeyCode.Mouse0)) Teleport_Mouse();
                    if (manager_PlayerController.JoystickButtonUp(eJoystickButton.R1) || manager_PlayerController.JoystickButtonUp(eJoystickButton.R2))
                        Teleport(aimMark.transform.position); break;//todo:コントローラーでのテレポートの修正
                case 4123: BladeSlash().GetComponent<SpriteRenderer>().color = Color.red; break;
            }

            chanting = 0;
        }
    }
    override public void GetPoint()
    {
        //todo:フラグゲット処理
    }
    private void Magic_()
    {
        Debug.Log("MagicBase");
    }
}
