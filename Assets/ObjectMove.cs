using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectMove : MonoBehaviour//“®‚©‚³‚È‚¢‚Æ“–‚½‚è”»’è‚ª‹N“®‚µ‚È‚¢‚½‚ß
{
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.FindWithTag("Manager");
    }
    float direction = 0;
    // Update is called once per frame
    void Update()
    {
        direction += Time.deltaTime;Debug.Log(parent.transform.position);
        transform.position = new Vector3(math.sin(direction) * 0.1f, math.cos(direction) * 0.1f, 0) + parent.transform.position;
    }
}
