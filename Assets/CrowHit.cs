using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowHit : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision .tag == "Player") {
            if (collision.GetComponent<ObjectFall>().GetSituation() != ObjectFall.eSituation.normal) return;
            EnemyMove enemyMove = GetComponent<EnemyMove>();
            collision.GetComponent<playerMove>().AddPosition(enemyMove.GetMove() * enemyMove.moveSpeed * Time.deltaTime);
        }
    }
}
