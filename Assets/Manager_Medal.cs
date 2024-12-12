using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMedalType
{
    none,

    speedBoots,

    [InspectorName("")]max,
}

public class Manager_Medal : MonoBehaviour
{
    [SerializeField]
    Sprite[] medalImageBase;
    public Sprite GetMedalImageBase(int index) { return medalImageBase[index]; }

    public const int medalPocketNum = 3;
    [SerializeField]
    eMedalType[] medalType = new eMedalType[medalPocketNum];
    public eMedalType GetMedalType(int medalPocketNum_) { if (medalPocketNum_ < 0 || medalPocketNum_ >= medalPocketNum) return eMedalType.none; return medalType[medalPocketNum_];}
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
