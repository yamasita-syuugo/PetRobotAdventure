using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject target;

    public float speedDelay = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);

        move.x = (target.transform.position.x - transform.position.x) / speedDelay * Time.deltaTime;
        move.y = (target.transform.position.y - transform.position.y) / speedDelay * Time.deltaTime;

        transform.position += move;
    }
}
