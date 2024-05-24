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

    bool fallSoundCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        situation = eSituation.normal;
        fallSound = GameObject.Find("fallSound").GetComponent<AudioSource>();
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
                if(tag == "Player")UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
                else Destroy(gameObject);
            }

            if (!fallSoundCheck)
            {
                if(gameObject.tag == "Player")fallSound.Play();
                fallSoundCheck = true;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }

    bool fallCheck = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Scaffold")
        {
            fallCheck = false;
        }
    }
    bool oneFrame = true;
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
