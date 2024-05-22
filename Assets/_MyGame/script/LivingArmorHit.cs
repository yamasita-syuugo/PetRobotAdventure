using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingArmorHit : MonoBehaviour
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

        }
        else if (collision.tag == "Attack")
        {
            GetComponent<KnockBack>().SetKnockBackEnergy(collision.GetComponent<bulletMove>().GetMoveEnelgy());
        }
        else if (collision.tag == "Enemy")
        {

        }
    }
}
