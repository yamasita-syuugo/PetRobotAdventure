using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_Button_Right_Click : MonoBehaviour
{
    Manager_Player manager_Player;
    Manager_Player_Technique manager_Player_Technique;

    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>();
        manager_Player_Technique = GameObject.FindWithTag("Manager").GetComponent<Manager_Player_Technique>();
    }

    // Update is called once per frame
    void Update()
    {
        RightClick();
    }
    ePlayerType playerType = ePlayerType.none;
    public void SetPlayerType(ePlayerType playerType_) { playerType = playerType_; }
    int TechniqueIndex = 0;
    public void SetTechniqueIndex(int techniqueIndex_) { TechniqueIndex = techniqueIndex_; }
    void RightClick()
    {
        if (playerType == ePlayerType.none) return;

        if (onMouse == true && Input.GetMouseButtonDown(1))
        {
            manager_Player.SetPlayerTypeIndex(playerType);
            manager_Player_Technique.SetTwo(TechniqueIndex);
        }
    }

    bool onMouse = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MousePointer") onMouse = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MousePointer") onMouse = false;
    }
}
