using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FlailMove : MonoBehaviour
{
    GameObject parent;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;

        player = GameObject.FindWithTag("Player");
    }

    [SerializeField]float direction = 0;
    [SerializeField]float searchDistance = 5;
    [SerializeField]float standardDistance = 1;
    [SerializeField]float adjustmentSpeed = 3;
    [SerializeField]float distance = 1;
    [SerializeField]float speed = 1;
    // Update is called once per frame
    void Update()
    {
        direction += speed * Time.deltaTime;

        Vector2 flailPos = new Vector2(math.sin(direction) * distance, math.cos(direction) * distance);
        transform.position = parent.transform.position + (Vector3)flailPos;

        DistanceAdjustment();
    }

    void DistanceAdjustment()
    {
        float playerDistance = Vector2.Distance(player.transform.position, parent.transform.position);
        if (playerDistance <= searchDistance)
        {
            if (playerDistance > distance) distance += Time.deltaTime * adjustmentSpeed;
            else if (playerDistance < distance) distance -= Time.deltaTime * adjustmentSpeed;
        }else
        {
            if (standardDistance > distance) distance += Time.deltaTime * adjustmentSpeed;
            else if (standardDistance < distance) distance -= Time.deltaTime * adjustmentSpeed;
        }
    }
    public void ReturnSpeed()
    {
        speed *= -1;
    }
}
