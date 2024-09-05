using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    Vector2 knockBackEnergy = Vector2.zero;
    public void SetKnockBackEnergy(Vector3 knockBackEnergy_) { knockBackEnergy = knockBackEnergy_; }
    public Vector3 GetKnockBackEnergy() {  return knockBackEnergy; }
    float moveSpeed = 1;
    public void AddMoveSpeed(float speed) { moveSpeed = speed * energyMagnification; }

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
        knockBackEnergy /= new Vector3(friction, friction, friction);

        Vector2 energy = knockBackEnergy * moveSpeed;
        if ((energy.x < 0.2 && energy.x > -0.2) && (energy.y < 0.2 && energy.y > -0.2)) energy = Vector2.zero; 
        transform.position += (Vector3)energy * Time.deltaTime;
    }

}

