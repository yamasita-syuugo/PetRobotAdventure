using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void StartButton()
    {
        DataSave();

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void DataSave()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            GameObject.Find("TitleManager").GetComponent<Manager_StageSelect>().DataSave();
            GameObject.Find("Serect_Chara").GetComponent<Select_Chara>().DataSave();
        }

    }
}
