using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Scaffold_Base : MonoBehaviour
{
    public enum eScaffold
    {
        none,

        block,
        ice,
        grass,

        scaffoldMax,
    }
    protected eScaffold scaffold = eScaffold.none;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public int GetScaffold()
    {
        return (int)scaffold;
    }
}
