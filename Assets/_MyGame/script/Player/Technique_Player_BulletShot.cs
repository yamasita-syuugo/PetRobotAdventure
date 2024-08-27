using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technique_Player_BulletShot : MonoBehaviour
{
    GameObject magazine;

    [SerializeField]
    bulletMove bulletBase;
    AudioSource shotSound ;
    public float moveDirectionX, moveDirectionY;

    // Start is called before the first frame update
    void Start()
    {
        shotSound = GameObject.Find("shotSound").GetComponent<AudioSource>();
        aimMark = Instantiate<GameObject>(aimMarkPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        ControllerShot();
    }

    [SerializeField]
    GameObject aimMarkPrefab;
    GameObject aimMark;
    bool controllerShot = false;
    public void SetControllerShot(bool shot_ = true) { controllerShot = shot_; }
    void ControllerShot()
    {
        moveDirectionX = Input.GetAxis("AimX");
        moveDirectionY = -Input.GetAxis("AimY");

        if (controllerShot)
        {
            controllerShot = false;

            Shot();
        }

        float distance = Mathf.Sqrt(moveDirectionX * moveDirectionX + moveDirectionY * moveDirectionY);
        if (distance > 0.3f)
        {
            aimMark.transform.position = new Vector3(moveDirectionX / distance * 3, moveDirectionY / distance * 3, transform.position.z) + transform.position;//todo:x,y‚ªNan‚É‚È‚é
            aimMark.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        else
        {
            aimMark.GetComponent<SpriteRenderer>().material.color = Color.clear;
        }
    }
    public void MouseShot()
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDirectionX = mousePos.x - playerPos.x;
        moveDirectionY = mousePos.y - playerPos.y;
        float distance = Mathf.Sqrt(moveDirectionX * moveDirectionX + moveDirectionY * moveDirectionY);
        moveDirectionX /= distance;
        moveDirectionY /= distance;

        Shot();
    }
    void Shot()
    {
        if (!GetComponent<Technique_Player_BulletMagazine>().BulletCheck()) return;

        ScoreManager.ShotNumAdd();

        bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
        tmp1.transform.position = this.transform.position;
        Vector2 tmp2 = new Vector2(moveDirectionX, moveDirectionY);
        tmp1.SetMoveEnelgy(tmp2);

        shotSound.Play(0);

        GetComponent<Technique_Player_BulletMagazine>().AddBullet(-1);
    }
}
