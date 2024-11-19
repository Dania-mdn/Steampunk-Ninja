using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeinScript : MonoBehaviour
{
    private bool isMove = false;
    private bool isHold = false;
    private bool isBuild = true;
    public GameObject Character;
    public GameObject Weight;
    public GameObject PositionSpuwn;
    public GameObject PositionDown;
    public GameObject PrefabPush;
    public GameObject MeinPush;
    public GameObject MeinPushCilindr;
    private float speed = 150;
    public GameObject LastPush;
    private Rigidbody rb;

    private void Start()
    {
        rb = Character.GetComponent<Rigidbody>();
    }
    public void Down()
    {
        isHold = true;
    }
    public void Up()
    {
        isHold = false;
    }
    private void FixedUpdate()
    {
        if (isMove)
        {
            rb.velocity = new Vector3(rb.velocity.x + 0.2f, rb.velocity.y, rb.velocity.z);
        }
        if (isHold)
        {
            Character.GetComponent<Rigidbody>().velocity = Vector3.up * (speed * Time.deltaTime);
            if(LastPush.transform.position.y < PositionDown.transform.position.y)
            {
                GameObject newObjekt = Instantiate(PrefabPush, PositionSpuwn.transform.position, Quaternion.identity, MeinPushCilindr.transform);
                newObjekt.transform.position = LastPush.transform.position + new Vector3(0, 0.36f, 0);
                LastPush = newObjekt;
            }
            isBuild = false;
        }
        else
        {
            if(isBuild == false)
            {
                DropBright();
                isBuild = true;
            }
        }
    }
    public void DropBright()
    {
        MeinPush.transform.parent = null;
        MeinPushCilindr.GetComponent<Rigidbody>().AddTorque(Vector3.forward * -100, ForceMode.Force);
    }
    public void Move()
    {
        isMove = true;
    }
}
