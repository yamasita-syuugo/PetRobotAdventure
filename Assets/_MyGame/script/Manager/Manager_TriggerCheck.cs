using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Manager_TriggerCheck : MonoBehaviour
{
   Player_Move player_Move;
    GameObject triggerCheck;

    private void Start()
    {
        triggerCheck = new GameObject(); triggerCheck.transform.parent = transform; triggerCheck.tag = "TriggerCheck"; triggerCheck.name = "triggerCheck";
        triggerCheck.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;triggerCheck.AddComponent<CircleCollider2D>();
    }

    bool on = false;
    float direction = 0;
    private void Update()
    {
        if (!on) { on = true; player_Move = GameObject.FindWithTag("Player").GetComponent<Player_Move>(); triggerCheck = GameObject.FindWithTag("TriggerCheck"); }

        direction += Time.deltaTime; 
        triggerCheck.transform.position = new Vector3(math.sin(direction) * 0.1f, math.cos(direction) * 0.1f, 0) + transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "TriggerCheck") return;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "TriggerCheck") return;
        player_Move.MoveBuffReset();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "TriggerCheck") return;
    }
}
