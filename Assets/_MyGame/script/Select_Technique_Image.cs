using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Technique_Image : MonoBehaviour
{
    [SerializeField]
    eWeaponControl techniqueControl = eWeaponControl.none;
    Manager_Player manager_Player;
    Manager_Player_Technique manager_Player_Technique;
    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.Find("Manager").GetComponent<Manager_Player>();
        manager_Player_Technique = GameObject.Find("Manager").GetComponent<Manager_Player_Technique>();
    }

    // Update is called once per frame
    void Update()
    {
        ImageChange();
    }
    int oldPlayerTypeIndex = -1;
    int oldTechniqueIndex = -1;
    void ImageChange()
    {
        int technique = 0;
        switch (techniqueControl)
        {
            case eWeaponControl.one:technique = manager_Player_Technique.GetOne();break; 
            case eWeaponControl.two:technique = manager_Player_Technique.GetTwo();break; 
        }
        int playerTypeIndex = (int)manager_Player.GetPlayerTypeIndex();
        if (oldTechniqueIndex == technique && oldPlayerTypeIndex == playerTypeIndex) return;
        oldTechniqueIndex = technique; oldPlayerTypeIndex = playerTypeIndex;

        GetComponent<Image>().color = Color.white;
        switch ((ePlayerType)manager_Player.GetPlayerTypeIndex())
        {
            case ePlayerType.PetRobot:
                GetComponent<Image>().sprite = manager_Player_Technique.GetTechniqueImage(technique);
                break;
            case ePlayerType.Werewolf:
                GetComponent<Image>().sprite = manager_Player_Technique.GetAttackImage(technique);
                break;
            case ePlayerType.WizardGhost:
                GetComponent<Image>().sprite = manager_Player_Technique.GetMagicImage(technique);
                break;
        }

        if (technique == 0) GetComponent<Image>().color = Color.clear;
        else GetComponent<Image>().color = Color.white;
    }
}
