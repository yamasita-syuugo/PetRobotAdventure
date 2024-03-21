using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    public enum eSituation
    {
        none,
        normal,
        fall,
    }
    [SerializeField]
    eSituation situation = eSituation.normal;
    public AudioSource fallSound;

    [SerializeField] bool fallCheck = false;

    bool oneFrame = true;
    // Start is called before the first frame update
    void Start()
    {
        situation = eSituation.normal;
    }

    // Update is called once per frame
    void Update()
    {

        if(situation == eSituation.fall)
        {
            if(transform.localScale.x > 0.1)
            {
                transform.localScale = transform.localScale * 0.999f;
                //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;   //移動にRigidbodyを使用していないため
            }
            else
            {
               Destroy(gameObject);
            }
            if (!fallSound.isPlaying)
            {
                fallSound.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Block")
        {
            fallCheck = false;
        }
    }
    private void FixedUpdate()
    {
        if (oneFrame)   //1フレーム目にfallCheckを通過してしまうため
        {
            oneFrame = false;
            return;
        }
        if (fallCheck == true)
        {
            situation = eSituation.fall;
        }
        else
        {
            fallCheck = true;
        }
    }

    public eSituation GetSituation() { return situation; }
}
