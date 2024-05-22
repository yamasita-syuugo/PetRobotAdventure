using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    bool shotTrigger = false;
    public bulletMove bulletBase;
    public AudioSource shotSound ;
    const int magazineSize = 10;
    [SerializeField]
    int magazine = magazineSize;
    public float moveDirectionX, moveDirectionY;

    // Start is called before the first frame update
    void Start()
    {
        shotSound = GameObject.Find("shotSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;
        ControllerShot();
        MouseShot();
    }
    int ControllerShotButton;
    void ControllerShot()
    {
        ControllerShotButton = (int)Input.GetAxisRaw("Shot");
        moveDirectionX = Input.GetAxis("AimX");
        moveDirectionY = -Input.GetAxis("AimY");

        if (ControllerShotButton == 1 && shotTrigger == false)
        {
            shotTrigger = true;

            Shot();
        }
        else if (ControllerShotButton != 1 && shotTrigger == true)
        {
            shotTrigger = false;
        }
    }
    void MouseShot()
    {
        if (Input.GetMouseButtonDown(0) == false) return;
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
        if (magazine > 0)
        {
                bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
                shotSound.Play(0);
                tmp1.transform.position = this.transform.position;
                Vector2 tmp2 = new Vector2(moveDirectionX, moveDirectionY);
                tmp1.SetMoveEnelgy(tmp2);

                magazine--;
                GetComponent<PlayerUIDisplay>().BulletNumCheck();
                GetComponentInChildren<BlockUI>().UIUpDate();

                ScoreManager.ShotNumAdd();
        }
    }

    public void AddMagazine(int add = 1)
    {
        if (magazine >= magazineSize ) return;
        magazine += add;
        GetComponent<PlayerUIDisplay>().BulletNumCheck();
    }
    public int GetMagazineSize()
    {
        return magazineSize;
    }
    public int GetMagazine()
    {
        return magazine;
    }
}
