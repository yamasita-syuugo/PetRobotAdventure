using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Medal_Catch : MonoBehaviour
{
    Manager_Medal manager_Medal;
    // Start is called before the first frame update
    void Start()
    {
        manager_Medal = GameObject.FindWithTag("Manager").GetComponent<Manager_Medal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && onMouse) MedalCatch(GetComponent<Medal_Type>().GetMedalType());
        if (Input.GetMouseButtonUp(0)) MedalRelease();
        Move();

        CameraOutReSpawn();
    }
    bool medalCatch;
    public void MedalCatch(eMedalType medalType) { medalCatch = true; }
    public void MedalRelease() {  medalCatch = false;if (uI_MedalSet != null) { manager_Medal.SetMedalType(uI_MedalSet.GetPos(), GetComponent<Medal_Type>().GetMedalType());Destroy(gameObject); } }
    Vector3 oldPos = new Vector3();
    void Move()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (medalCatch) transform.position = mousePos;
        oldPos = mousePos;
    }

    UI_MedalSet uI_MedalSet = null;
    bool onMouse = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<UI_MedalSet>() != null) uI_MedalSet = collision.GetComponent<UI_MedalSet>();
        if(collision.tag == "MousePointer")onMouse = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<UI_MedalSet>() != null) uI_MedalSet = null;
        if (collision.tag == "MousePointer") onMouse = false;
    }

    void CameraOutReSpawn()
    {
        Vector2 leftDown = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 rightUp = Camera.main.ViewportToWorldPoint(Vector2.one);
        if(transform.position.x < leftDown.x || transform.position.y < leftDown.y || transform.position.x > rightUp.x || transform.position.y > rightUp.y)
        {

            Vector2 randomPos = new Vector2(Random.RandomRange(-0.5f, 0.5f), Random.RandomRange(-0.5f, 0.5f));
            transform.position = randomPos;
        }
    }
}
