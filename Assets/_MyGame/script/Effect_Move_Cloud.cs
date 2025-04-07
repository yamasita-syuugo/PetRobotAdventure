using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Move_Cloud : MonoBehaviour
{
    Vector2 move = new Vector2(.5f,.5f);
    public void SetMove(Vector2 move_) {  move = move_; }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)move * Time.deltaTime;
    }
}
