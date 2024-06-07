using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    Vector2 knockBackEnergy = Vector2.zero;
    float moveSpeed = 1;

    [SerializeField] float frictionBase = 3;
    [SerializeField] float energyMagnification = 2;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        float friction = (frictionBase / 1000) + 1;
        transform.position += (Vector3)knockBackEnergy * moveSpeed * Time.deltaTime;
        knockBackEnergy /= new Vector3(friction, friction, friction);
        if ((knockBackEnergy.x < 0.2 && knockBackEnergy.x > -0.2) && (knockBackEnergy.y < 0.2 && knockBackEnergy.y > -0.2)) knockBackEnergy = Vector2.zero; 
    }

    public void SetKnockBackEnergy(Vector3 knockBackEnergy_)
    {
        knockBackEnergy = knockBackEnergy_;
    }
    public void AddMoveSpeed(float speed)
    {
        moveSpeed += speed * energyMagnification;
    }
}
