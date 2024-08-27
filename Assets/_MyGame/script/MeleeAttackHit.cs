using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackHit : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
    enum eEffect
    {
        destroy,
        knockBack,
    }
    [SerializeField]
    eEffect effect = eEffect.destroy;

    [SerializeField]
    //float knockBackPower = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            switch (effect)
            {
                case eEffect.destroy:
                    Destroy(collision.gameObject);
                    break;
                case eEffect.knockBack:
                    float x = collision.gameObject.transform.position.x - gameObject.transform.position.x;
                    float y = collision.gameObject.transform.position.y - gameObject.transform.position.y;
                    collision.GetComponent<KnockBack>().SetKnockBackEnergy(new Vector3 (x,y,0));
                    collision.GetComponent<KnockBack>().AddMoveSpeed(2);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
