using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    public enum eDestroyType
    {
        none,
        time,
        distance,
    }
    public eDestroyType destroyType = eDestroyType.time;

    public float deleteTime = 3.0f;
    float survivalTime;

    public float deleteDistance = 5.0f;
    Vector2 spaunPosition = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        survivalTime = deleteTime;
        spaunPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyType == eDestroyType.time)
        {
            if ((survivalTime -= Time.deltaTime) < 0.0f)
            {
                survivalTime = deleteTime;
                Destroy(this.gameObject);
            }
        }
        else if (destroyType ==  eDestroyType.distance)
        {
            if (Vector2.Distance(spaunPosition, transform.position) > deleteDistance)
            {
                Destroy(this.gameObject);
            } 
        }
    }
}
