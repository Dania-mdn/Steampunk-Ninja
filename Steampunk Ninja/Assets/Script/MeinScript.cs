using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeinScript : MonoBehaviour
{
    private bool isHold = false;
    private bool isHoldAnimation = false;
    private bool isBuild = true;
    public GameObject character;
    public GameObject PositionSpuwn;
    public GameObject PositionDown;
    public GameObject PrefabPush;
    public GameObject MeinPush;
    public GameObject PrefabMeinPush;
    public GameObject MeinPushCilindr;
    public GameObject[] PushCilindrArray;
    private float speed = 150;
    public GameObject LastPushCilindr;

    private void Start()
    {
        PushCilindrArray = new GameObject[30];
    }
    public void Down()
    {
        isHold = true;
        isHoldAnimation = true;
    }
    public void Up()
    {
        isHold = false;
    }
    private void FixedUpdate()
    {
        if (character.GetComponent<Character>().isMuve) return;

        if (isHold)
        {
            character.GetComponent<Rigidbody>().velocity = Vector3.up * (speed * Time.deltaTime);
            if(LastPushCilindr.transform.position.y < PositionDown.transform.position.y)
            {
                GameObject newObjekt = Instantiate(PrefabPush, PositionSpuwn.transform.position, Quaternion.identity, MeinPushCilindr.transform);
                newObjekt.transform.position = LastPushCilindr.transform.position + new Vector3(0, 0.18f, 0);
                LastPushCilindr = newObjekt;
            }
            isBuild = false;
            if (isHoldAnimation)
            {
                character.GetComponent<Character>().AnimationRise();
                isHoldAnimation = false;
            }
        }
        else
        {
            if(isBuild == false)
            {
                DropBright();
                isBuild = true;
                isHoldAnimation = true;
            }
        }
    }
    public void DropBright()
    {
        EventManager.DoMuve();
        MeinPush.transform.parent = null;
        MeinPushCilindr.GetComponent<Rigidbody>().freezeRotation = false;
        MeinPushCilindr.GetComponent<Rigidbody>().AddTorque(Vector3.forward * -100, ForceMode.Force);
    }
    public void SetNewBright(GameObject Ground)
    {
        MeinPush = Instantiate(PrefabMeinPush, PositionDown.transform.position, Quaternion.identity, character.transform);
        MeinPushCilindr = MeinPush.GetComponent<Parametrer>().MeinPushCilindr;
        LastPushCilindr = MeinPush.GetComponent<Parametrer>().LastPushCilindr;
        MeinPushCilindr.GetComponent<HingeJoint>().connectedBody = Ground.GetComponent<Rigidbody>();
        character.GetComponent<Character>().isSpuwnBrige = false;
    }
}
