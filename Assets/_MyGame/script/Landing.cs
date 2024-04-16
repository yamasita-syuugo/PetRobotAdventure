using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    Vector2 beforePosision = Vector2.zero;
    public GameObject blast;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(beforePosision == (Vector2)transform.position) {
            Bomb();
        }
        beforePosision = (Vector2)transform.position;
    }
    void Bomb()
    {
        GameObject tmp = Instantiate<GameObject>(blast);
        tmp.transform.position = transform.position;

        Destroy(gameObject);
    }
}
