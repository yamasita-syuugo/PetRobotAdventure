using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateProcess : MonoBehaviour
{
    GameObject manager;
    Manager_Gate manager_GateOpen;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        manager_GateOpen = manager.GetComponent<Manager_Gate>();
    }

    // Update is called once per frame
    [SerializeField]
    Sprite []gateBase = new Sprite[2];
    bool oldGateOpen;
    void Update()
    {
        if(oldGateOpen != manager_GateOpen.GetGateOpen())
        {oldGateOpen = manager_GateOpen.GetGateOpen();
            GetComponent<SpriteRenderer>().sprite = gateBase[oldGateOpen?1:0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (manager_GateOpen == null || !manager_GateOpen.GetGateOpen()) return;

        manager.GetComponent<Manager_Score>().DataSave();
        Manager_GameSituation manager_GameSituation = manager.GetComponent<Manager_GameSituation>();
        manager_GameSituation.SetGameSituation(eGameSituation.clear);
        manager_GameSituation.DataSave();
        Manager_Collection manager_Collection = manager.GetComponent<Manager_Collection>();
        manager_Collection.SetGetSituation(eCollectionType.stage, (int)manager.GetComponent<Manager_StageSelect>().GetStage() + 1, true);
        manager_Collection.DataSave();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }
}
