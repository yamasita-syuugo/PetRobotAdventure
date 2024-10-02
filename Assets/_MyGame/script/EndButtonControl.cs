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
        GameObject.FindWithTag("Manager").GetComponent<Manager_Save>().DataSave();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
