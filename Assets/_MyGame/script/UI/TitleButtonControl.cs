using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void TitleButton()
    {
        GameObject.FindWithTag("Manager").GetComponent<Manager_Save>().DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
