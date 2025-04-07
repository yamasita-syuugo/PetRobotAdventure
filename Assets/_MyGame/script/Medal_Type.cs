using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal_Type : MonoBehaviour
{
    [SerializeField] eMedalType medalType;
    public eMedalType GetMedalType() { return medalType; }
    public void SetMedalType(eMedalType medalType_) {  medalType = medalType_; }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
