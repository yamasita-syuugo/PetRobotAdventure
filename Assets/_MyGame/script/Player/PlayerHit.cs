using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flag") {
            ScoreManager.FlagGetPointAdd();

            GameObject.Find("Player_Technique").GetComponent<Technique_Player__Control>().GetPoint();

            Destroy(collision.GameObject());
        }
    }
}
