using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    Vector2 beforePosision = Vector2.zero;
    [SerializeField]
    GameObject blast;
    [SerializeField]
    GameObject shadowModel;
    GameObject shadow = null;
    // Start is called before the first frame update
    void Start()
    {
        shadow = Instantiate<GameObject>(shadowModel);
    }

    // Update is called once per frame
    void Update()
    {
        if(beforePosision == (Vector2)transform.position) {
            Bomb();
        }
        beforePosision = (Vector2)transform.position;
    }
    void Bomb()
    {
        GameObject tmp = Instantiate<GameObject>(blast);
        tmp.transform.position = transform.position;
        tmp.transform.parent = transform.parent;

        Destroy(shadow.gameObject);
        Destroy(gameObject);
    }

    public void SetShadowPosision(Vector2 pos)
    {
        shadow.transform.position = pos;
    }
}
