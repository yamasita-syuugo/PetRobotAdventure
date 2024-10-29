using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Technique_Play_BulletShot : Player_Technique_Play__Base
{
    GameObject magazine;

    [SerializeField]
    bulletMove bulletBase;
    AudioSource shotSound ;
    public float moveDirectionX, moveDirectionY;

    // Start is called before the first frame update
    void Start()
    {
        shotSound = GameObject.FindWithTag("Manager").GetComponent<Manager_Sounds>().GetSound("shotSound");
        aimMark = Instantiate<GameObject>(aimMarkPrefab);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    [SerializeField]
    GameObject aimMarkPrefab;
    GameObject aimMark;
    bool controllerShot = false;
    public void SetControllerShot(bool shot_ = true) { controllerShot = shot_; }
    override public void ControllerPlay()
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
    override public void MousePlay()
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
        if (!GetComponent<Player_Technique_Container_BulletMagazine>().BulletCheck()) return;

        Manager_Score.ShotNumAdd();

        bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
        tmp1.transform.position = this.transform.position;
        Vector2 tmp2 = new Vector2(moveDirectionX, moveDirectionY);
        tmp1.SetMoveEnelgy(tmp2);

        shotSound.Play(0);

        GetComponent<Player_Technique_Container_BulletMagazine>().AddBullet(-1);
    }
}
