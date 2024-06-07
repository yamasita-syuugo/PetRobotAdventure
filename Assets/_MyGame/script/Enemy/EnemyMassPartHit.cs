using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMassPartHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector3 enelgy = transform.position - transform.parent.position;
            collision.GetComponent<KnockBack>().SetKnockBackEnergy(enelgy * 3);
        }else if(collision.tag == "Attack")
        {


            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
