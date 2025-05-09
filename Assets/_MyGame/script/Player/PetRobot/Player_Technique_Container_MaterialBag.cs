using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum eMaterialType
{
    None,

    block,
    grass,
    ice,

    materialTypeMax,
}
public class Player_Technique_Container_MaterialBag : Player_Technique_Container__Base
{
    [SerializeField]
    eMaterialType materialType;
    [SerializeField]
    GameObject[] materialBase = new GameObject[(int)eMaterialType.materialTypeMax];

    [SerializeField]
    int earthQuakeMaterialsMax = 5;
    [SerializeField]
    int earthQuakeMaterials = 3;
    public bool MaterialsCheck() { if (earthQuakeMaterials > 0) return true; return false; }
    public int GetEarthQuakeMaterials() { return earthQuakeMaterials; }
    public void AddEarthQuakeMaterials(int add = 1) { earthQuakeMaterials += add; /*GetComponent<TextMeshProUGUI>().text = earthQuakeMaterials.ToString("00");*/ }

    public override void GetPoint() { AddEarthQuakeMaterials(); }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

}
