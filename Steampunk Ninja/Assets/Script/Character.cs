using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    public bool isMuve = true;
    private bool isMuveAnimation = false;
    private bool isFallAnimation = false;
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
        else
        {
            isGround = false;
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
        if(rb.velocity.y < -2)
        {
            if (isFallAnimation == false)
            {
                Animationfall();
            }
        }
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
        isMuveAnimation = true;
        isFallAnimation = false;
        Animation.Play("mixamo.com");
        Animation.Stop("mixamo.com 1");
        Animation.Stop("mixamo.com 2");
        Animation.Stop("mixamo.com 3");
    }
    public void Animationfall()
    {
        isMuveAnimation = false;
        isFallAnimation = true;
        Animation.Stop("mixamo.com 1");
        Animation.Play("mixamo.com 1");
        Animation.Stop("mixamo.com 2");
        Animation.Stop("mixamo.com 3");
    }
    public void AnimationIdl()
    {
        isMuveAnimation = false;
        isFallAnimation = false;
        Animation.Stop("mixamo.com 2");
        Animation.Stop("mixamo.com 1");
        Animation.Play("mixamo.com 2");
        Animation.Stop("mixamo.com 3");
    }
    public void AnimationRise()
    {
        isMuveAnimation = false;
        isFallAnimation = false;
        Animation.Stop("mixamo.com 2");
        Animation.Stop("mixamo.com 1");
        Animation.Stop("mixamo.com 2");
        Animation.Play("mixamo.com 3");
    }
}
