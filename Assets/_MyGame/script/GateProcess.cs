using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateProcess : MonoBehaviour
{
    Manager_Gate manager_GateOpen;
    // Start is called before the first frame update
    void Start()
    {
        manager_GateOpen = GameObject.FindWithTag("Manager").GetComponent<Manager_Gate>();
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

        Manager_Score.DataSave();
        GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().SetGameSituation(eGameSituation.clear);
        GameObject.FindWithTag("Manager").GetComponent<Manager_GameSituation>().DataSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }
}
