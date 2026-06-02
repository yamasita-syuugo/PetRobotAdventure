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
        if (oldGateOpen == manager_GateOpen.GetGateOpen()) return;oldGateOpen = manager_GateOpen.GetGateOpen();

            GetComponent<SpriteRenderer>().sprite = gateBase[oldGateOpen?1:0];
        if (oldGateOpen)
        {
            GameObject player = GameObject.FindWithTag("Player");
            float x = transform.position.x - player.transform.position.x;
            float y = transform.position.y - player.transform.position.y;
            float distance = Mathf.Sqrt(x * x + y * y);
            if (player.GetComponent<CircleCollider2D>().radius + GetComponent<CircleCollider2D>().radius > distance) Clear(player.GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Clear(collision);
    }

    void Clear(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (manager_GateOpen == null || !manager_GateOpen.GetGateOpen()) return;

        manager.GetComponent<Manager_Score>().DataSave();
        Manager_GameSituation manager_GameSituation = manager.GetComponent<Manager_GameSituation>();
        manager_GameSituation.SetGameSituation(eGameSituation.clear);
        manager_GameSituation.DataSave();
        Manager_Collection manager_Collection = manager.GetComponent<Manager_Collection>();
        Manager_StageSelect manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        if(manager_StageSelect.GetStage() != eStage.max - 1 || !manager_StageSelect.GetRandomStage())
            manager_Collection.SetGetSituation(eCollectionType.stage, (int)manager.GetComponent<Manager_StageSelect>().GetStage() + 1, true);
        manager_Collection.DataSave();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }
}
