using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Technique_Image : MonoBehaviour
{
    [SerializeField]
    eTechniqueControl techniqueControl = eTechniqueControl.none;
    Select_Chara select_Chara;
    // Start is called before the first frame update
    void Start()
    {
        select_Chara = GameObject.Find("Serect_Chara").GetComponent<Select_Chara>();
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
            case eTechniqueControl.one:technique = select_Chara.GetOne();break; 
            case eTechniqueControl.two:technique = select_Chara.GetTwo();break; 
        }
        if (technique == oldTechnique && select_Chara.GetPlayerType() == oldPlayerType) return;

        switch (select_Chara.GetPlayerType())
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
