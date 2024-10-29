using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Technique_Container_BulletMagazine : Player_Technique_Container__Base
{
    [SerializeField]
    int bulletNum;
    public bool BulletCheck() { if (bulletNum > 0) return true; else return false; }
    public void AddBullet(int bulletNum_ = 1) { bulletNum += bulletNum_; if (bulletNum > magazineSize) bulletNum = magazineSize; else if (bulletNum <= 0) bulletNum = 0; }
    public int GetBulletNum() {  return bulletNum; }
    [SerializeField]
    int magazineSize = 5;
    public int GetMagazineSize() {  return magazineSize; }

    public override void GetPoint() { AddBullet(); }

    // Start is called before the first frame update
    void Start()
    {
        bulletNum = magazineSize;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
