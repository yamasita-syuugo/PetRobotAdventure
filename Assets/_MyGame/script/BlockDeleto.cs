using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
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
        if (collision.tag == "Scaffold")
        {
            Destroy(collision.GameObject());
        }
        //else if(collision.tag == "Player")
        //{
        //    Vector3 energy = collision.transform.position - transform.position;
        //    collision.GetComponent<KnockBack>().SetKnockBackEnergy(energy);
        //    collision.GetComponent<KnockBack>().AddMoveSpeed(2);
        //}
    }
}
