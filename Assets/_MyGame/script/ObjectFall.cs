using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    public enum eSituation
    {
        [InspectorName("")] none,

        normal,
        fall,       //落下中
        fly,        //浮遊中
        chanting,   //詠唱中
    }
    [SerializeField]
    eSituation situation = eSituation.normal;
    public eSituation GetSituation() { return situation; }
    public void SetSituation(eSituation situation_) { situation = situation_; }
    AudioSource fallSound;

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
        fallSound = GameObject.FindWithTag("Manager").GetComponent<Manager_Sounds>().GetSound("fallSound");
        waterSound = GameObject.FindWithTag("Manager").GetComponent<Manager_Sounds>().GetSound("waterSound");

        baseSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
    }
    float baseSize;
    void Fall()
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
    AudioSource waterSound;
    void OceanToFall()
    {
        if (transform.localScale.x > baseSize * (2f/3))
        {
            transform.localScale = transform.localScale * 0.999f;
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;   //移動にRigidbodyを使用していないため
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if (transform.localScale.x > baseSize * (1.2f / 3))
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
                Manager_Score.DataSave();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            }
            else Destroy(gameObject);
        }
    }
    void SkyToFall()
    {
        if (transform.localScale.x > baseSize * (1f / 3))
        {
            float down = 1 - 0.6f * Time.deltaTime;
            transform.localScale = transform.localScale * down;//todo: FPSによって速度が変わる
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;   //移動にRigidbodyを使用していないため
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            if (tag == "Player")
            {
                Manager_Score.DataSave();
                GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().SetGameSituation(eGameSituation.failure);
                GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().DataSave();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            }
            else Destroy(gameObject);
        }
    }

    int fallFrame = 3;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Scaffold")
        {
            fallFrame = 3;
        }
    }
    private void FixedUpdate()
    {
        if(situation == eSituation.fly) fallFrame = 3;

        if (fallFrame <= 0)
        {
            situation = eSituation.fall;
        }
        else
        {
            fallFrame--;
        }
    }
}
