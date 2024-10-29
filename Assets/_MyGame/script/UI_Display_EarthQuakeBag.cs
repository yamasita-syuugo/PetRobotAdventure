using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Display_EarthQuakeBag : UI_Display__Base
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        int materialsNum = connectTechnique.GetComponent<Player_Technique_Container_MaterialBag>().GetEarthQuakeMaterials();
        GetComponent<TextMeshProUGUI>().text = materialsNum.ToString();
    }
}
