using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efecto_Move_Cloud : MonoBehaviour
{
    Vector2 move = Vector2.zero;
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
