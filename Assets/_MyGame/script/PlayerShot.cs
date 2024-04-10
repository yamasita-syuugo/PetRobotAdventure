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
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        Shot();
    }
    void Shot()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        int shotButton = (int)Input.GetAxisRaw("Shot");
        moveDirectionX = Input.GetAxis("AimX");
        moveDirectionY = -Input.GetAxis("AimY");

        if (magazine > 0)
        {
            if (shotButton == 1 && shotTrigger == false)
            {
                shotTrigger = true;

                bulletMove tmp1 = Instantiate<bulletMove>(bulletBase);
                shotSound.Play(0);
                tmp1.transform.position = this.transform.position;
                Vector2 tmp2 = new Vector2(moveDirectionX, moveDirectionY);
                tmp1.SetMoveEnelgy(tmp2);

                magazine--;

                ScoreManager.ShotNumAdd();
            }
            else if (shotButton != 1 && shotTrigger == true)
            {
                shotTrigger = false;
            }
        }
    }

}
