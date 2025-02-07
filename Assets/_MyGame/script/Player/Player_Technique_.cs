using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Technique_ : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    virtual public extern void GetPoint();


    protected void Teleport()
    {
        GameObject.FindWithTag("Player").transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    [SerializeField]
    bulletMove bulletBase;
    [SerializeField]
    AudioSource shotSound;
    public float moveDirectionX, moveDirectionY;
    protected void Shot()
    {
        Debug.Log("shot");

        //if (!GetComponent<Player_Technique_Container_BulletMagazine>().BulletCheck()) return;

        Manager_Score.ShotNumAdd();

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
        tmp1.SetCenter(transform);

        if (swordSound != null) swordSound.Play(0);

        return tmp1.gameObject;
    }
}
