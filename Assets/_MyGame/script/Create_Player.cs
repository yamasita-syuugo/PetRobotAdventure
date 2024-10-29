using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Manager_Player manager_Player = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>();
        GameObject Player = Instantiate(manager_Player. GetPlayerTypeBase(PlayerPrefs.GetInt("playerTypeIndex")));
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
