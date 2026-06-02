using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Technique_Play_BulletShot : Player_Technique_Play_Base
{
    Manager_Player manager_Player;
    Manager_PlayData manager_PlayData;
    Manager_PlayerController manager_PlayerController;

    [SerializeField]
    bulletMove bulletBase;
    AudioSource shotSound ;
    public float moveDirectionX, moveDirectionY;

    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_Player = manager.GetComponent<Manager_Player>();
        manager_PlayData = manager.GetComponent<Manager_PlayData>();
        manager_PlayerController = manager.GetComponent<Manager_PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shotSound = GameObject.FindWithTag("Manager").GetComponent<Manager_Sounds>().GetSound(eSoundType.shot);
        aimMark = Instantiate<GameObject>(aimMarkBase);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirectionX = manager_PlayerController.GetAim().x;
        moveDirectionY = manager_PlayerController.GetAim().y;

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

    [SerializeField]
    GameObject aimMarkBase;
    GameObject aimMark;
    bool controllerShot = false;
    public void SetControllerShot(bool shot_ = true) { controllerShot = shot_; }
    override public void ControllerPlay()
    {
        //if (!controllerShot) return; controllerShot = false;

        Shot();
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

        switch (manager_Player.GetPlayerTypeIndex())
        {
            case ePlayerType.PetRobot:manager_PlayData.AddUseTechnique(manager_Player.GetPlayerTypeIndex(), (int)ePlayerWeaponType.Bullet);break;
            default:Debug.Log("switch : error_PlayerType");break;
        }
        

        bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
        tmp1.transform.position = this.transform.position;
        Vector2 tmp2 = new Vector2(moveDirectionX, moveDirectionY);
        tmp1.SetMoveEnelgy(tmp2);

        shotSound.Play(0);

        GetComponent<Player_Technique_Container_BulletMagazine>().AddBullet(-1);
    }
}
