using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    [SerializeField]
    ePlayerWeaponType attackType = ePlayerWeaponType.none;
    public ePlayerWeaponType GetAttackType() {  return attackType; }
    // Start is called before the first frame update
    void Start()
    {
        if (attackType == ePlayerWeaponType.none) Debug.Log(name + " enemyType = none");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

}
