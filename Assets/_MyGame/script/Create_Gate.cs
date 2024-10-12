using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Gate : MonoBehaviour
{
    [SerializeField]
    GameObject gate;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gate);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
