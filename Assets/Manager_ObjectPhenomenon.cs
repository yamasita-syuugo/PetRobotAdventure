using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_ObjectPhenomenon : MonoBehaviour
{
    [SerializeField]
    int num = 15;
    GameObject[] objects;
    public void SetObject(GameObject object_)
    {
        for(int i = 0;i < num; i++)
        {
            if (objects[i] != null) return;

            objects[i] = object_;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        objects = new GameObject[num];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < num; i++)
        {
            if (objects[i] != null)
            {
                Debug.Log(objects[i].name);
                if (objects[i].GetComponent<KnockBack>().GetKnockBackEnergy().x == 0 && objects[i].GetComponent<KnockBack>().GetKnockBackEnergy().y == 0) { 
                    GameObject.Find("GameManager").GetComponent<Manager_Hit>().Explosion(objects[i]); 
                }
            }
        }
    }
}
