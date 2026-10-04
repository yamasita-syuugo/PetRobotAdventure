using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Player_Technique_ : MonoBehaviour
{
    protected Manager_Player manager_Player;
    protected Manager_PlayerController manager_PlayerController;
    protected Manager_PlayData manager_PlayData;
protected Manager_MousePointerType manager_MousePointerType;
    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_Player = manager.GetComponent<Manager_Player>();
        manager_PlayerController = manager.GetComponent<Manager_PlayerController>();
        manager_PlayData = manager.GetComponent<Manager_PlayData>();
        manager_MousePointerType = manager.GetComponent<Manager_MousePointerType>();
    }
    //private void Start()
    //{

    //}
    // Update is called once per frame
    //void Update()
    //{

    //}

    virtual public extern void GetPoint();

    protected GameObject aimMark;
    [SerializeField] float aimMaxDistance = 4.5f;
    protected void CreateAimMark(float aimMaxDistance_ = 4.5f)
    {
        aimMark = Instantiate(new GameObject());
        aimMark.name = "AimMark";
        aimMark.transform.parent = transform;
        aimMark.AddComponent<SpriteRenderer>();
        manager_MousePointerType = GameObject.FindWithTag("Manager").GetComponent<Manager_MousePointerType>();
        aimMark.AddComponent<Animator>().runtimeAnimatorController = manager_MousePointerType.GetMousePointerAnimation(manager_MousePointerType.GetMousePointerIndex());
        aimMark.AddComponent<Player_AimMark_Move>().SetMaxDistance(aimMaxDistance);

        aimMaxDistance = aimMaxDistance_;
    }


    protected void Teleport_Mouse()
    {
        GameObject.FindWithTag("Player").transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    protected void Teleport(Vector2 point)
    {
        GameObject.FindWithTag("Player").transform.position = point;
    }


    [SerializeField]
    bulletMove bulletBase;
    [SerializeField]
    AudioSource shotSound;
    public float moveDirectionX, moveDirectionY;
    protected void Shot(Vector2 move)
    {
        Debug.Log("shot");

        //if (!GetComponent<Player_Technique_Container_BulletMagazine>().BulletCheck()) return;

        switch (manager_Player.GetPlayerTypeIndex())
        {
            case ePlayerType.PetRobot: manager_PlayData.AddUseTechnique(manager_Player.GetPlayerTypeIndex(), (int)ePlayerWeaponType.Bullet); break;
            default: Debug.Log("switch : error_PlayerType"); break;
        }

        bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
        tmp1.transform.position = this.transform.position;
        Vector3 moveDirection = move;
        tmp1.SetMoveEnelgy(moveDirection);

        if(shotSound != null) shotSound.Play(0);

        //GetComponent<Player_Technique_Container_BulletMagazine>().AddBullet(-1);
    }
    protected void Shot_Mouse()
    {
        Debug.Log("shot");

        //if (!GetComponent<Player_Technique_Container_BulletMagazine>().BulletCheck()) return;

        switch (manager_Player.GetPlayerTypeIndex())
        {
            case ePlayerType.PetRobot: manager_PlayData.AddUseTechnique(manager_Player.GetPlayerTypeIndex(), (int)ePlayerWeaponType.Bullet); break;
            default: Debug.Log("switch : error_PlayerType"); break;
        }

        bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
        tmp1.transform.position = this.transform.position;
        Vector3 moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - GameObject.FindWithTag("Player").transform.position);
        tmp1.SetMoveEnelgy(moveDirection);

        if(shotSound != null) shotSound.Play(0);

        //GetComponent<Player_Technique_Container_BulletMagazine>().AddBullet(-1);
    }

    [SerializeField]
    Attack_Move_Sword swordBase;
    [SerializeField]
    AudioSource swordSound;
    protected GameObject BladeSlash()
    {
        //Manager_Score.ShotNumAdd();

        Attack_Move_Sword tmp1 = Instantiate<Attack_Move_Sword>(swordBase);
        tmp1.transform.position = this.transform.position;
        tmp1.SetCenter(transform.position);

        if (swordSound != null) swordSound.Play(0);

        return tmp1.gameObject;
    }

    [SerializeField] GameObject meleeAttack_Base;
    protected void MeleeAttack_Mouse()
    {
        GameObject tmp = Instantiate<GameObject>(meleeAttack_Base);
        tmp.transform.position = this.transform.position;
        tmp.transform.parent = this.transform;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float ang = -math.atan2(mousePos.x - transform.position.x, mousePos.y - transform.position.y) * Mathf.Rad2Deg;
        tmp.transform.localRotation= Quaternion.Euler(0, 0, ang);
    }
    protected void MeleeAttack()
    {
        GameObject tmp = Instantiate<GameObject>(meleeAttack_Base);
        tmp.transform.position = this.transform.position;
        tmp.transform.parent = this.transform;
        manager_PlayerController = GameObject.FindWithTag("Manager").GetComponent<Manager_PlayerController>();
        Vector2 aim = manager_PlayerController.GetAim();
        float ang = math.atan2(aim.x, -aim.y) * Mathf.Rad2Deg + 180;
        tmp.transform.localRotation = Quaternion.Euler(0, 0, ang);
    }

    protected void PounceOn_Mouse()//”ò‚ÑŠ|‚©‚è
    {
        if(transform.parent.GetComponent<ObjectFall>().GetSituation() != ObjectFall.eSituation.chanting)
            transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.chanting);
    }
    float flyTime = 0;
    protected void Pounce_Mouse()
    {
        transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.fly);flyTime = 2;
        transform.parent.GetComponent<Player_Move>().SetMoveBuff(3);
        transform.parent.GetComponent<Player_Move>().SetMoveBuffCount(2);
    }
    protected void PounceOn()
    {
        if (transform.parent.GetComponent<ObjectFall>().GetSituation() != ObjectFall.eSituation.chanting)
            transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.chanting);

    }
    protected void Pounce()
    {
        transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.fly); flyTime = 2;
        transform.parent.GetComponent<Player_Move>().SetMoveBuff(3);
        transform.parent.GetComponent<Player_Move>().SetMoveBuffCount(2);
    }
    protected void PounceUpDate()
    {
        if(flyTime >= 0) { flyTime -= Time.deltaTime;
            if(flyTime < 0)transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.normal); }
    }

    protected void TheEnd()
    {

    }
}
