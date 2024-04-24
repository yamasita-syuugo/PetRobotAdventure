using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLanding : MonoBehaviour
{
    GameObject parentObject;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if(parentObject == null)Destroy(gameObject);
    }

    public void SetParentObject(GameObject tmpObject) { 
        parentObject = tmpObject;
    }
}
