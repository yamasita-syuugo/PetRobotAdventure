using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Display_BulletShotMagazine : UI_Display__Base
{
    int magazineSize;

    [SerializeField]
    GameObject bulletIconBase;
    GameObject[] bulletIcon;
    [SerializeField, Header("アイコンの表示幅")]
    float wide = 150f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "";

        magazineSize = connectTechnique.GetComponent<Player_Technique_Container_BulletMagazine>().GetMagazineSize();

        bulletIcon = new GameObject[magazineSize];
        for (int i = 0; i < magazineSize; i++)
        {
            bulletIcon[i] = Instantiate(bulletIconBase, gameObject.transform);
            bulletIcon[i].transform.localPosition = new Vector3(i * wide / magazineSize - wide / 2, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletIconDisplay();
    }

    void BulletIconDisplay()
    {
        int bulletNum = connectTechnique.GetComponent<Player_Technique_Container_BulletMagazine>().GetBulletNum();
        for (int i = 0; i < bulletIcon.Length; i++)
        {
            if (i < bulletNum) bulletIcon[i].GetComponent<Image>().color = Color.yellow;
            else bulletIcon[i].GetComponent<Image>().color = Color.clear;
        }
    }
}
