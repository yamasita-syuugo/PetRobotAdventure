using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerUIDisplay : MonoBehaviour
{
    public GameObject blockUI;
    public GameObject bulletUI;
    GameObject[] playerUIMark;

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

        playerUIMark = new GameObject[bulletMax];
        PosisionSet();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void PosisionSet()
    {
        for (int i = 0; i < bulletMax + 1; i++)
        {
            if (i == 0)
            {
                playerUIMark[i] = Instantiate<GameObject>(blockUI);
            }
            else
            {
                playerUIMark[i] = Instantiate<GameObject>(bulletUI);
            }
            playerUIMark[i].transform.SetParent(playerShot.transform, false);
            Vector2 tmpPos = new Vector2(math.sin((float)i / bulletMax * 3.1415f * 2) * displayDistance, math.cos((float)i / bulletMax * 3.1415f * 2) * displayDistance);
            playerUIMark[i].transform.position = tmpPos;
        }
    }

    [SerializeField]
    Color bulletColor = new Color(1, 1, 1, 1);
    [SerializeField]
    Color unBulletColor = new Color(1, 1, 1, 1);
    public void BulletNumCheck()
    {
        bulletNum = playerShot.GetMagazine();
        SpriteRenderer spriteRenderer;
        for (int i = 0; i < bulletMax; i++)
        {
            spriteRenderer = playerUIMark[i].GetComponent<SpriteRenderer>();
            if (i < bulletNum)
            {
                spriteRenderer.color = new Color(bulletColor.r, bulletColor.g, bulletColor.b, bulletColor.a);
            }
            else
            {
                spriteRenderer.color = new Color(unBulletColor.r, unBulletColor.g, unBulletColor.b, unBulletColor.a);
            }
        }
    }
}
