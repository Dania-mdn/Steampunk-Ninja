using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    private bool isMuve = true;
    private bool isMuveAnimation = false;
    private bool isGround;
    public bool isSpuwnBrige = false;
    private Rigidbody rb;
    private GameObject Ground;

    public MeinScript meinScript;
    public Animation Animation;

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
        if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 2))
        {
            isGround = true;
        }
        if (isSpuwnBrige)
        {
            meinScript.SetNewBright(Ground);
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
                if(isMuveAnimation == false)
                {
                    AnimationMove();
                }

                // Ограничиваем скорость по оси X
                if (Mathf.Abs(rb.velocity.x) > 1)
                {
                    rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * 1, rb.velocity.y, rb.velocity.z);
                }
            }
        }
        if(rb.velocity.magnitude > 2)
        {
            Animationfall();
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
            transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            Ground = other.gameObject.transform.parent.gameObject;
            SetNewBright();
            EventManager.DoStop(); 
            AnimationIdl();
        }
        else
        {
            if (other.gameObject.tag == "Finish")
            {
                EventManager.DoEndGame();
            }
        }
    }
    private void SetMuve()
    {
        isMuve = true;
    }
    private void SetNewBright()
    {
        isSpuwnBrige = true;
    }
    public void AnimationMove()
    {
        Debug.Log(1);
        isMuveAnimation = true;
        Animation.Play("mixamo.com");
        Animation.Stop("mixamo.com 1");
        Animation.Stop("mixamo.com 2");
    }
    public void Animationfall()
    {
        Debug.Log(2);
        isMuveAnimation = false;
        Animation.Stop("mixamo.com 1");
        Animation.Play("mixamo.com 1");
        Animation.Stop("mixamo.com 2");
    }
    public void AnimationIdl()
    {
        Debug.Log(3);
        isMuveAnimation = false;
        Animation.Stop("mixamo.com 2");
        Animation.Stop("mixamo.com 1");
        Animation.Play("mixamo.com 2");
    }
}
