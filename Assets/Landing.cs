using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    Vector2 beforePosision = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(beforePosision == (Vector2)transform.position) { 
            
        }
        beforePosision = (Vector2)transform.position;
    }
}
