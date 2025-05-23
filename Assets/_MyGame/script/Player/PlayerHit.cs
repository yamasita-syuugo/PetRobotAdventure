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
        if (collision.tag == "Flag")
        {
            Manager_Score.FlagGetPointAdd();
            switch (GetComponent<PlayerType>().GetPlayerType())
            {
                case ePlayerType.PetRobot:
                    GameObject.FindWithTag("Player").GetComponent<Player_Technique_>().GetPoint();
                    break;
                case ePlayerType.WizardGhost:
                    GameObject.FindWithTag("Player").GetComponent<Player_Technique_>().GetPoint(); 
                    break;
                    case ePlayerType.WereWolf: break;
                    default: Debug.Log("PlayerHit : " + GetComponent<PlayerType>().GetPlayerType().ToString()); break;
            }

            Destroy(collision.GameObject());
        }
    }
}
