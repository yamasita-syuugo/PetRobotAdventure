using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Technique_Image : MonoBehaviour
{
    [SerializeField]
    eTechniqueControl techniqueControl = eTechniqueControl.none;
    Manager_Player manager_Player;
    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.Find("Manager").GetComponent<Manager_Player>();
    }

    [SerializeField]
    Sprite[] techniqueImage = new Sprite[(int)ePlayerTechniqueType.playerTechniqueTypeMax];
    [SerializeField]
    Sprite[] magicImage = new Sprite[(int)ePlayerMagicType.playerMagicMax];
    // Update is called once per frame
    ePlayerType oldPlayerType;
    int oldTechnique = 0;
    void Update()
    {
        int technique = 0;
        switch (techniqueControl)
        {
            case eTechniqueControl.one:technique = manager_Player.GetOne();break; 
            case eTechniqueControl.two:technique = manager_Player.GetTwo();break; 
        }
        if (technique == oldTechnique && manager_Player.GetPlayerType() == oldPlayerType) return;

        switch (manager_Player.GetPlayerType())
        {
            case ePlayerType.PetRobot:
                GetComponent<Image>().sprite = techniqueImage[technique];
                break;
            case ePlayerType.WizardGhost:
                GetComponent<Image>().sprite = magicImage[technique]; 
                break;
        }

        if (technique == 0) GetComponent<Image>().color = Color.clear;
        else GetComponent<Image>().color = Color.white;
    }
}
