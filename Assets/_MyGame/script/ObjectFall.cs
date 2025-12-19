using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    GameObject manager;
    Manager_GameSituation manager_GameSituation;
    Manager_Score manager_Score;
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

    private void OnEnable()
    {
        manager = GameObject.FindWithTag("Manager");
        manager_GameSituation = manager.GetComponent<Manager_GameSituation>();
        manager_Score = manager.GetComponent<Manager_Score>();
    }
    // Start is called before the first frame update
    void Start()
    {
        situation = eSituation.normal;
        Manager_Sounds manager_Sounds = manager.GetComponent<Manager_Sounds>();
        fallSound = manager_Sounds.GetSound(eSoundType.fall);
        waterSound = manager_Sounds.GetSound(eSoundType.waterSound);

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
                GetComponent<SpriteRenderer>().sortingOrder = -2;
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
                manager_Score.DataSave();
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
                manager_Score.DataSave();
                manager_GameSituation.SetGameSituation(eGameSituation.failure);
                manager_GameSituation.DataSave();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            }
            else if(tag == "Enemy")
            {
                GameObject.FindWithTag("Create").GetComponent<Create_Flag>().FlagSpaun();

                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }

    int fallFrame = 10;
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
