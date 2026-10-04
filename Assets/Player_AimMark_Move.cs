using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AimMark_Move : MonoBehaviour
{
    Manager_PlayerController manager_PlayerController;

    float maxDistance = 4.5f;
    public void SetMaxDistance(float maxDistance_) { maxDistance = maxDistance_; }
    // Start is called before the first frame update
    void Start()
    {


        manager_PlayerController = GameObject.FindWithTag("Manager").GetComponent<Manager_PlayerController>();
        transform.localScale = new Vector2(0.4f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + (Vector3)manager_PlayerController.GetAim() * maxDistance;
    }
}
