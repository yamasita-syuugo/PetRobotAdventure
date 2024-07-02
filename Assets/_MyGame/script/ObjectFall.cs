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

    public enum eFallType
    {
        none,
        carBody,
        ocean,
        sky,

        fallTypeMax,
    }
    [SerializeField]
    eFallType fallType = eFallType.sky;

    // Start is called before the first frame update
    void Start()
    {
        situation = eSituation.normal;
        fallSound = GameObject.Find("fallSound").GetComponent<AudioSource>();
        waterSound = GameObject.Find("waterSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (situation == eSituation.fall)
        {
            switch (fallType)
            {
                case eFallType.none:
                    break;
                case eFallType.carBody:
                    CarBodyToFall();
                    break;
                case eFallType.ocean:
                    OceanToFall();
                    break;
                case eFallType.sky:
                    SkyToFall();
                    break;
            }

            if (!fallSoundCheck)
            {
                if (gameObject.tag == "Player") fallSound.Play();
                fallSoundCheck = true;
            }
        }
    }
    void CarBodyToFall()
    {

    }
    [SerializeField]
    GameObject splashOfWater = null;
    GameObject splashOfWaterTmp = null;
    [SerializeField]
    AudioSource waterSound;
    void OceanToFall()
    {
        if (transform.localScale.x > 2.0f)
        {
            transform.localScale = transform.localScale * 0.999f;
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;   //移動にRigidbodyを使用していないため
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if (transform.localScale.x > 1.2f/*サイズが3の場合*/)
        {
            if(splashOfWaterTmp == null)
            {
                splashOfWaterTmp = GameObject.Instantiate(splashOfWater);
                splashOfWaterTmp.transform.position = transform.position;

                fallSound.Stop();
                if(!waterSound.isPlaying)
                {
                    waterSound.Play();
                }
            }
            transform.localScale = transform.localScale * 0.999f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -20);
        }
        else
        {
            if (tag == "Player")
            {
                ScoreManager.ResultSend();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            }
            else Destroy(gameObject);
        }
    }
    void SkyToFall()
    {
        if (transform.localScale.x > 1/*サイズが3の場合*/)
        {
            transform.localScale = transform.localScale * 0.999f;
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;   //移動にRigidbodyを使用していないため
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            if (tag == "Player")
            {
                ScoreManager.ResultSend();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            }
            else Destroy(gameObject);
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
