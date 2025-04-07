using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Attack_Move_Sword : MonoBehaviour
{
    Transform center;
    public void SetCenter(Transform center_) {  center = center_; }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    float movePoint = 0;
    float moveSpeed = 12;
    public void SetMoveSpeed(float moveSpeed_) {  moveSpeed = moveSpeed_; }
    float deleteTime = 6.24f;
    public void SetDeleteTime(float deleteTime_) {  deleteTime = deleteTime_; }
    void Move()
    {
        float x = math.cos(movePoint);
        float y = math.sin(movePoint);
        transform.position = center.position + new Vector3(x,y);
        float z = -Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z );

        if(deleteTime >= 0 && movePoint > deleteTime)Destroy(gameObject);

        movePoint += Time.deltaTime * moveSpeed;
    }
}
