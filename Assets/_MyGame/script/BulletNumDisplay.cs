using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletNumDisplay : MonoBehaviour
{
    [SerializeField]
    PlayerShot player;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        int bulletNum = player.GetMagazine();
        int bulletMaxNum = player.GetMagazineSize();
        GetComponent<TextMeshProUGUI>().text = bulletNum.ToString("00") + "/" + bulletMaxNum.ToString("00");
    }
}
