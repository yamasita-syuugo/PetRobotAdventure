using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletNumDisplay : MonoBehaviour
{
    PlayerShot player;
    int bulletNum;
    int bulletMaxNum;
    [SerializeField]
    GameObject bulletIconBase;
    GameObject[] bulletIcon;
    [SerializeField]
    float wide = 150f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerShot>();
        bulletMaxNum = player.GetMagazineSize();
        bulletIcon = new GameObject[bulletMaxNum];
        for (int i = 0; i < bulletMaxNum; i++)
        {
            bulletIcon[i] = Instantiate(bulletIconBase, gameObject.transform);
            bulletIcon[i].transform.localPosition = new Vector3(i * wide / bulletMaxNum - wide / 2, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletIconDisplay();
    }

    void BulletIconDisplay()
    {
        if (bulletNum == player.GetMagazine()) return;
        bulletNum = player.GetMagazine();
        for(int i = 0;i < bulletIcon.Length; i++)
        {
            if(i < bulletNum) bulletIcon[i].GetComponent<Image>().color = Color.yellow;
            else bulletIcon[i].GetComponent<Image>().color = Color.clear;
         }
    }
}
