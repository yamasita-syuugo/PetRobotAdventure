using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforcementBoard : MonoBehaviour
{
    enum eReinforcementType
    {

    }
    [SerializeField]
    int boardSizeX = 2;
    [SerializeField]
    int boardSizeY = 2;
    [SerializeField]
    eReinforcementType[] panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = new eReinforcementType[boardSizeX * boardSizeY];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
