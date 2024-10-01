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
     none,

    Bullet,
    EarthQuakeInpact,
    MeleeAttack,

    Mirage,


    [InspectorName("")] playerTechniqueTypeMax,
}

public enum ePlayerMagicType
{
    none,

    tmp,

    [InspectorName("")] playerMagicMax,
}

public class Manager_Player: MonoBehaviour
{
    [SerializeField]
    GameObject []playerBase = new GameObject[(int)ePlayerType.playerTypeMax];

    [SerializeField]
    GameObject gate;
    [SerializeField]
    GameObject mousePointer;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Instantiate(playerBase[PlayerPrefs.GetInt("playerType")]);
        Instantiate(gate);
        Instantiate(mousePointer);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
