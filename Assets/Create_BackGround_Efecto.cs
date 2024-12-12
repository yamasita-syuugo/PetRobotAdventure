using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eEfectoType
{
    none,

    cloud,

    [InspectorName("")] max,
}

public class Create_BackGround_Efecto : MonoBehaviour
{
    [SerializeField, Range(0, (int)eEfectoType.max - 1)]
    eEfectoType efectoType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
