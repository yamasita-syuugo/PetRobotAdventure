using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    [SerializeField]
    eTechniqueObjectType techniqueObjectType = eTechniqueObjectType.none;
    public eTechniqueObjectType GetAttackType() {  return techniqueObjectType; }
    // Start is called before the first frame update
    void Start()
    {
        if (techniqueObjectType == eTechniqueObjectType.none) Debug.Log(name + " enemyType = none");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

}
