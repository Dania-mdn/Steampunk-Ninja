using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    private bool isMuve = true;
    private bool isGround;
    private Rigidbody rb;
    private GameObject Ground;

    public MeinScript meinScript;

    private void OnEnable()
    {
        EventManager.Muve += SetMuve;
    }
    private void OnDisable()
    {
        EventManager.Muve -= SetMuve;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision collision)
    {
        RaycastHit hitDown;
        if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 0.1f))
        {
            Ground = hitDown.transform.gameObject;
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }
    private void FixedUpdate()
    {
        if (isMuve)
        {
            if(isGround)
            {
                float moveDirection = 1;  // Например, движение вправо
                rb.AddForce(new Vector3(moveDirection * 1, 0, 0), ForceMode.VelocityChange);

                // Ограничиваем скорость по оси X
                if (Mathf.Abs(rb.velocity.x) > 1)
                {
                    rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * 1, rb.velocity.y, rb.velocity.z);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, Vector3.down * 0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "stop")
        {
            isMuve = false;
            rb.velocity = Vector3.zero;
            SetNewBright();
        }
    }
    private void SetMuve()
    {
        isMuve = true;
    }
    private void SetNewBright()
    {
        meinScript.SetNewBright(Ground);
    }
}
