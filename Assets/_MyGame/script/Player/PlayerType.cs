using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    [SerializeField]
    ePlayerType playerType = ePlayerType.none;
    public ePlayerType GetPlayerType() { return playerType; }
    // Start is called before the first frame update
    void Start()
    {
        if (playerType == ePlayerType.none) Debug.Log(gameObject.name + " playerTypd = none");
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
