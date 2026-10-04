using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Technique_Action : Player_Technique_
{
    ObjectFall objectFall;
    Manager_PlayerController manager_PlayerController;
    Manager_Player_Technique manager_Player_Technique;
    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_PlayerController = manager.GetComponent<Manager_PlayerController>();
        manager_Player_Technique = manager.GetComponent<Manager_Player_Technique>();
    }
    // Start is called before the first frame update
    void Start()
    {
        objectFall = GameObject.FindWithTag("Player").GetComponent<ObjectFall>();
        CreateAimMark(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectFall.GetSituation() == ObjectFall.eSituation.fall) return;

        for (int i = 0; i < (int)eTechnicControl.max; i++)
        {
            int useTechnic = 0;
            switch ((eTechnicControl)i)
            {
                case eTechnicControl.one: useTechnic = manager_Player_Technique.GetOne(); break;
                case eTechnicControl.two: useTechnic = manager_Player_Technique.GetTwo(); break;
            }
            switch ((ePlayerAttackType)useTechnic)//マウス
            {
                case ePlayerAttackType.MeleeAttack: if (manager_PlayerController.GetTechnicMouse(ePushType.down, (eTechnicControl)i)) MeleeAttack_Mouse(); break;
                case ePlayerAttackType.pounce: if (manager_PlayerController.GetTechnicMouse(ePushType.stey, (eTechnicControl)i)) PounceOn_Mouse();
                    if (manager_PlayerController.GetTechnicMouse(ePushType.up, (eTechnicControl)i))Pounce_Mouse() ;
                    PounceUpDate(); break;
                default:Debug.Log("useTechnic : " + ((ePlayerAttackType)useTechnic)); break;
            }
            switch ((ePlayerAttackType)useTechnic)//コントローラー
            {
                case ePlayerAttackType.MeleeAttack: if (manager_PlayerController.JoystickButtonDown(eJoystickButton.R1 + i * 2)) MeleeAttack(); break;
                case ePlayerAttackType.pounce: if (manager_PlayerController.JoystickButton(eJoystickButton.R1 + i * 2)) PounceOn();
                    if (manager_PlayerController.JoystickButtonUp(eJoystickButton.R1 + i * 2))Pounce() ; 
                    PounceUpDate(); break;
                default:Debug.Log("useTechnic : " + ((ePlayerAttackType)useTechnic)); break;
            }
        }


    }
}
