using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerBulletDisplay : MonoBehaviour
{
    public GameObject bulletUI;
    GameObject[] bulletMark;

    PlayerShot playerShot;
    int bulletMax = 10;
    int bulletNum;

    public float displayDistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerShot = GetComponent<PlayerShot>();
        bulletMax = playerShot.GetMagazineSize();
        bulletNum = playerShot.GetMagazine();

        bulletMark = new GameObject[bulletMax];
        for (int i = 0; i < bulletMax; i++)
        {
            bulletMark[i] = Instantiate<GameObject>(bulletUI);
            bulletMark[i].transform.SetParent(playerShot.transform, false);
            Vector2 tmpPos = new Vector2(math.sin((float)i / bulletMax * 3.1415f * 2) * displayDistance, math.cos((float)i / bulletMax * 3.1415f * 2) * displayDistance);
            Debug.Log(tmpPos);
            bulletMark[i].transform.position = tmpPos;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BulletNumCheck()
    {
        for (int i = 0; i < bulletMax; i++)
        {
            if (i <= bulletNum)
            {
                bulletMark[i].GetComponent<Renderer>().material.color = new Vector4(255, 255, 255, 255);
            }
            else
            {
                bulletMark[i].GetComponent<Renderer>().material.color = new Vector4(255, 255, 255, 50);

            }
        }
    }
}
