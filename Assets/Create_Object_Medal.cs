using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Object_Medal : MonoBehaviour
{
    Manager_Medal manager_Medal;
    Manager_Collection manager_Collection;

    [SerializeField] GameObject medalObject;
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_Medal = manager.GetComponent<Manager_Medal>();
        manager_Collection = manager.GetComponent<Manager_Collection>();

        int count = 0;
        for (int i = (int)eMedalType.none + 1; i < (int)eMedalType.max; i++)
        {
            if (manager_Collection.GetGetSituation(eCollectionType.medal, i)) continue;
            bool continue_ = false;
            for (int j = 0; j < manager_Medal.GetMedalPocketNum(); j++) if (manager_Medal.GetMedalType(j) == (eMedalType)i) { oldMedalType[j] = (eMedalType)i; continue_ = true; }
            if (continue_) continue;

            GameObject tmp = Instantiate<GameObject>(medalObject);
            tmp.transform.parent = transform;
            tmp.GetComponent<SpriteRenderer>().sprite = manager_Medal.GetMedalImageBase(i);
            tmp.transform.position = new Vector3(i % 14 - 7, -i / 14 + 1, 0);
            tmp.GetComponent<Medal_Type>().SetMedalType((eMedalType)i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        MedalCreateSystem();
    }

    eMedalType[] oldMedalType = new eMedalType[Manager_Medal.medalPocketNum];
    void MedalCreateSystem()
    {
        for (int i = 0; i < oldMedalType.Length; i++)
        {
            eMedalType newMedalType = manager_Medal.GetMedalType(i);
            if (oldMedalType[i] == newMedalType) continue;eMedalType tmp_OldMedalType = oldMedalType[i]; oldMedalType[i] = newMedalType;

            if (tmp_OldMedalType == eMedalType.none) continue;
            GameObject tmp = Instantiate<GameObject>(medalObject);
            tmp.transform.parent = transform;
            tmp.GetComponent<SpriteRenderer>().sprite = manager_Medal.GetMedalImageBase((int)tmp_OldMedalType);
            Vector2 randomPos = new Vector2(Random.RandomRange(-0.5f, 0.5f), Random.RandomRange(-0.5f, 0.5f));
            tmp.transform.position = randomPos;
            tmp.GetComponent<Medal_Type>().SetMedalType(tmp_OldMedalType);
        }
    }
}
