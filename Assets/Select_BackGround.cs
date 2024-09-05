using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBackGroundType
{
    sea,
    forest,

    backGroundTypeMax,
}

public class Select_BackGround : MonoBehaviour
{
    [SerializeField]
    eBackGroundType backGroundType = eBackGroundType.sea;
    [SerializeField]
    Sprite[] backGroundBase = new Sprite[(int)eBackGroundType.backGroundTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = backGroundBase[(int)backGroundType];
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

}
