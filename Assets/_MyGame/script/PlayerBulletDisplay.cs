using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDisplay : MonoBehaviour
{
    public int bulletMax = 10;
    int bulletNum;

    public SpriteRenderer bulletUI;
    // Start is called before the first frame update
    void Start()
    {
        bulletNum = bulletMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
