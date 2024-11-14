using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    [SerializeField]
    ePlayerTechniqueType attackType = ePlayerTechniqueType.none;
    // Start is called before the first frame update
    void Start()
    {
        if (attackType == ePlayerTechniqueType.none) Debug.Log(name + " enemyType = none");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public ePlayerTechniqueType GetAttackType() {  return attackType; }
}
