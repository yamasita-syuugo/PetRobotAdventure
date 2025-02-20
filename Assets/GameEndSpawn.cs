using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            gameObject.active = GameObject.FindWithTag ("Manager").GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.stage,(int)eStage.max - 1);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
