using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void EndButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
