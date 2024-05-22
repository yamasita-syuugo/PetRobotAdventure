using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlailMove : MonoBehaviour
{
    GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<EnemyMove>().gameObject;
    }

    [SerializeField]
    float direction = 0;
    [SerializeField]
    float distance = 1;
    [SerializeField]
    float speed = 1;
    // Update is called once per frame
    void Update()
    {
        direction += speed * Time.deltaTime;

        Vector2 flailPos = new Vector2(math.sin(direction) * distance, math.cos(direction) * distance);
        transform.position = parent.transform.position + (Vector3)flailPos;
    }

    public void ReturnSpeed()
    {
        speed *= -1;
    }
}
