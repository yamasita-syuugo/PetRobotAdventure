using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    [SerializeField]
    eAttackType attackType = eAttackType.none;
    // Start is called before the first frame update
    void Start()
    {
        if (attackType == eAttackType.none) Debug.Log(name + " enemyType = none");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public eAttackType GetAttackType() {  return attackType; }
}
