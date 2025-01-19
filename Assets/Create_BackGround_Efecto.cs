using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEfectoType
{
    none,

    cloud,

    [InspectorName("")] max,
}

public class Create_BackGround_Efecto : MonoBehaviour
{
    [SerializeField, Range(0, (int)eEfectoType.max - 1)]
    eEfectoType efectoType;
    public void SetEfectoType(eEfectoType efectoType_) {  efectoType = efectoType_; }

    GameObject []efectoObjectBase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EfectoSpawn();
    }
    void EfectoSpawn()
    {
        switch (efectoType)
        {
            case eEfectoType.none:break;
            case eEfectoType.cloud:break;

                default:Debug.Log("error : efectoType"); break;
        }
    }
}
