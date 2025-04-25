using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_PlayData : MonoBehaviour
{
    int[][] playStage = new int[(int)ePlayerType.max][];
    int[][] clearStage = new int[(int)ePlayerType.max][];
    int[][] failureStage = new int[(int)ePlayerType.max][];

    int[][] useTechnique = new int[(int)ePlayerType.max][];
    void instantiate()
    {
        for (int i = 0; i < (int)ePlayerType.max; i++) playStage[i] = new int[(int)eStage.max];
        for (int i = 0; i < (int)ePlayerType.max; i++) clearStage[i] = new int[(int)eStage.max];
        for (int i = 0; i < (int)ePlayerType.max; i++) failureStage[i] = new int[(int)eStage.max];

        for (int i = 0; i < (int)ePlayerType.max; i++) switch ((ePlayerType)i)
            {
                case ePlayerType.PetRobot: useTechnique[i] = new int[(int)ePlayerWeaponType.max]; break;
                case ePlayerType.WizardGhost: useTechnique[i] = new int[(int)ePlayerMagicType.max]; break;
                case ePlayerType.WereWolf: useTechnique[i] = new int[(int)ePlayerAttackType.max]; break;
                default: Debug.Log("switchError" + ((ePlayerType)i).ToString()); break;
            }
    }
    public void DataSave()
    {
        for (int i = 0; i < (int)ePlayerType.max; i++)
        {
            for (int j = 0; j < (int)eStage.max; j++)
            {
                PlayerPrefs.SetInt("PlayData_PlayStage_" + ((ePlayerType)i).ToString() + "_" + ((eStage)j).ToString(), playStage[i][j]);
                PlayerPrefs.SetInt("PlayData_ClearStage_" + ((ePlayerType)i).ToString() + "_" + ((eStage)j).ToString(), clearStage[i][j]);
                PlayerPrefs.SetInt("PlayData_Failure?Stage_" + ((ePlayerType)i).ToString() + "_" + ((eStage)j).ToString(), failureStage[i][j]);
            }
            for(int j = 0;j < useTechnique[i].Length; j++)
            {
                string techniqueName = "";
                switch ((ePlayerType)i)
                {
                    case ePlayerType.PetRobot: techniqueName = "PlayerWeaponType"; break;
                    case ePlayerType.WizardGhost: techniqueName = "PlayerMagicType"; break;
                    case ePlayerType.WereWolf: techniqueName = "PlayerAttackType"; break;
                    default: Debug.Log("switchError_" + ((ePlayerType)i).ToString());continue; 
                }
                PlayerPrefs.SetInt("PlayData_UseTechnique_" + ((ePlayerType)i).ToString() + "_" + techniqueName, playStage[i][j]);
            }
        }
    }
    public void DataLoad()
    {
        for (int i = 0; i < (int)ePlayerType.max; i++)
        {
            for (int j = 0; j < (int)eStage.max; j++)
            {
                playStage[i][j] =  PlayerPrefs.GetInt("PlayData_PlayStage_" + ((ePlayerType)i).ToString() + "_" + ((eStage)j).ToString());
                clearStage[i][j] = PlayerPrefs.GetInt("PlayData_ClearStage_" + ((ePlayerType)i).ToString() + "_" + ((eStage)j).ToString());
                failureStage[i][j] = PlayerPrefs.GetInt("PlayData_Failure?Stage_" + ((ePlayerType)i).ToString() + "_" + ((eStage)j).ToString());
            }
            for(int j = 0;j < useTechnique[i].Length; j++)
            {
                string techniqueName = "";
                switch ((ePlayerType)i)
                {
                    case ePlayerType.PetRobot: techniqueName = "PlayerWeaponType"; break;
                    case ePlayerType.WizardGhost: techniqueName = "PlayerMagicType"; break;
                    case ePlayerType.WereWolf: techniqueName = "PlayerAttackType"; break;
                    default: Debug.Log("switchError_" + ((ePlayerType)i).ToString());continue; 
                }
                playStage[i][j] = PlayerPrefs.GetInt("PlayData_UseTechnique_" + ((ePlayerType)i).ToString() + "_" + techniqueName);
            }
        }
    }

    private void OnEnable()
    {
        instantiate();
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}
