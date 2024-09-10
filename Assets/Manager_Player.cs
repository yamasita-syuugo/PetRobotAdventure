using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerType
{
    [InspectorName("")] none,

    PetRobot,
    WizardGhost,

    [InspectorName("")] playerTypeMax,
}

public enum ePlayerTechniqueType
{
    [InspectorName("")] none,

    Bullet,
    EarthQuakeInpact,
    MeleeAttack,

    Mirage,


    [InspectorName("")] playerTechniqueTypeMax,
}

public class Manager_Player: MonoBehaviour
{
    [SerializeField]
    GameObject []playerBase = new GameObject[(int)ePlayerType.playerTypeMax];
    // Start is called before the first frame update
    private void OnEnable()
    {
        Instantiate(playerBase[PlayerPrefs.GetInt("playerType")]);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
