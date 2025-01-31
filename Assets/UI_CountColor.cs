using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum eColor
{
    [InspectorName("")]none = -1,

    red,
    green,
    blue,
    alpha,

    [InspectorName("")]max,
}

public class UI_CountColor : MonoBehaviour
{
    Manager_Player manager_Player;
    Manager_Player_Technique manager_Player_Technique;

    [SerializeField, Header("one = true;twe = false")]
    bool one;
    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>();
        manager_Player_Technique = GameObject.FindWithTag("Manager").GetComponent<Manager_Player_Technique>();

        ColorChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Color color = Color.black;
    void ColorChange()
    {
        int techniqueIndex;
        if(one) techniqueIndex = manager_Player_Technique.GetOne();else techniqueIndex = manager_Player_Technique.GetTwo();
        Color color = GetComponent<Image>().color;
        switch ((ePlayerType)manager_Player.GetPlayerTypeIndex())
        {
            case ePlayerType.PetRobot:
                switch ((ePlayerWeaponType)techniqueIndex)
                {
                    case ePlayerWeaponType.Bullet:
                        color = Color.yellow;
                        break;
                    case ePlayerWeaponType.EarthQuakeInpact:
                        color = new Color(0.7f, 0.2f, 0, 1);
                        break;
                }
                break;
            case ePlayerType.WizardGhost:
                switch ((ePlayerMagicType)techniqueIndex)
                {
                    case ePlayerMagicType.rubyRing:
                        color = Color.red;
                        break;
                }
                break;
            case ePlayerType.Werewolf:
                switch ((ePlayerAttackType)techniqueIndex)
                {
                    case ePlayerAttackType.MeleeAttack: 
                        color = Color.red;
                        break;
                }
                break;
        }
        color.a = 0.3f;

        GetComponent<Image>().color = color;


        if(false)GetComponent<Image>().color = Color.gray;
    }
}
