using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHit : MonoBehaviour
{
    ObjectFall objectFall;
    // Start is called before the first frame update
    void Start()
    {
        objectFall = GetComponent<ObjectFall>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectFall.GetSituation() != ObjectFall.eSituation.normal) return;
        if (collision.transform.tag == "Player")
        {
            Vector3 enelgy = collision.transform.position - transform.position;
            collision.GetComponent<KnockBack>().SetKnockBackEnergy(enelgy * 3);
        }
        else if (collision.transform.tag == "Attack")
        {
            GetComponent<KnockBack>().SetKnockBackEnergy(collision.gameObject.GetComponent<bulletMove>().GetMoveEnelgy());
            GetComponent<KnockBack>().AddMoveSpeed(collision.gameObject.GetComponent<bulletMove>().GetMoveSpeed());
            Destroy(collision.gameObject);
        }
    }
}
