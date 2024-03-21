using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    [SerializeField]
    Vector3 moveEnelgy = new Vector3(1.0f, 0.0f, 0.0f);
    public float moveSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveEnelgy * moveSpeed * Time.deltaTime;

    }

    public void SetMoveEnelgy(Vector2 newMoveEnelgy)
    {
        moveEnelgy = newMoveEnelgy;
    }
}
