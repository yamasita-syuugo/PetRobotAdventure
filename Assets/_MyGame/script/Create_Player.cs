using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Player : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerBase = new GameObject[(int)ePlayerType.playerTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = Instantiate(playerBase[PlayerPrefs.GetInt("playerType")]);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
