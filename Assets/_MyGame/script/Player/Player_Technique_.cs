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

    protected void Teleport()
    {
        GameObject.FindWithTag("Player").transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    [SerializeField]
    bulletMove bulletBase;
    AudioSource shotSound;
    public float moveDirectionX, moveDirectionY;
    protected void Shot()
    {
        if (!GetComponent<Player_Technique_Container_BulletMagazine>().BulletCheck()) return;

        Manager_Score.ShotNumAdd();

        bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
        tmp1.transform.position = this.transform.position;
        Vector2 tmp2 = /*new Vector2(moveDirectionX, moveDirectionY);*/ (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tmp1.SetMoveEnelgy(tmp2);

        shotSound.Play(0);

        GetComponent<Player_Technique_Container_BulletMagazine>().AddBullet(-1);
    }
}
