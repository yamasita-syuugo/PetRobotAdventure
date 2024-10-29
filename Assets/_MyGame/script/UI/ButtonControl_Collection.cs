using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl_Collection : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void CollectionButton()
    {
        GameObject.FindWithTag("Manager").GetComponent<Manager_Save>().DataSave();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Collection");
    }
}
