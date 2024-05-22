using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour
{
    [SerializeField]
    GameObject impact;
    // Start is called before the first frame update
    void Start()
    {
        GameObject imp = Instantiate<GameObject>(impact);
        imp.transform.position = transform.position;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
