using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timer = 0.5f;

    GameObject Object;

    // Start is called before the first frame update
    void Start()
    {
        Object = this.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
