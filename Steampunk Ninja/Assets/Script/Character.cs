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
    public Animation Animation1;
    public Animation Animation2;
    public Animation Animation3;
    public Animation Animation4;

    public AudioSource fail;
    public AudioSource run;
    public AudioSource jump;
    public AudioSource build;

    public GameObject[] Skin;

    private void OnEnable()
    {
        EventManager.Muve += SetMuve;
        EventManager.MuteAudio += AudioMute;
        EventManager.PlayAudio += AudioPlay;
    }
    private void OnDisable()
    {
        EventManager.Muve -= SetMuve;
        EventManager.MuteAudio -= AudioMute;
        EventManager.PlayAudio -= AudioPlay;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.GetInt("MuteAudio") == 1)
        {
            AudioMute();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        RaycastHit hitDown;
        if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 1))
        {
            isGround = true; 
            Debug.DrawLine(transform.position, hitDown.point, Color.green);
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
        if(rb.velocity.y < -3)
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
            build.Stop();
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
                fail.Play();
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
        jump.Play();
        if (build.isPlaying)
            build.Stop();
        run.Play();

        isMuveAnimation = true;
        isFallAnimation = false;
        Animation1.Play("mixamo.com");
        Animation1.Stop("mixamo.com 1");
        Animation1.Stop("mixamo.com 2");
        Animation1.Stop("mixamo.com 3");

        Animation2.Play("mixamo.com");
        Animation2.Stop("mixamo.com 1");
        Animation2.Stop("mixamo.com 2");
        Animation2.Stop("mixamo.com 3");

        Animation3.Play("mixamo.com");
        Animation3.Stop("mixamo.com 1");
        Animation3.Stop("mixamo.com 2");
        Animation3.Stop("mixamo.com 3");

        Animation4.Play("mixamo.com");
        Animation4.Stop("mixamo.com 1");
        Animation4.Stop("mixamo.com 2");
        Animation4.Stop("mixamo.com 3");
    }
    public void Animationfall()
    {
        if (build.isPlaying)
            build.Stop();
        if (run.isPlaying)
            run.Stop();
        isMuveAnimation = false;
        isFallAnimation = true;
        Animation1.Stop("mixamo.com 1");
        Animation1.Play("mixamo.com 1");
        Animation1.Stop("mixamo.com 2");
        Animation1.Stop("mixamo.com 3");

        Animation2.Stop("mixamo.com 1");
        Animation2.Play("mixamo.com 1");
        Animation2.Stop("mixamo.com 2");
        Animation2.Stop("mixamo.com 3");

        Animation3.Stop("mixamo.com 1");
        Animation3.Play("mixamo.com 1");
        Animation3.Stop("mixamo.com 2");
        Animation3.Stop("mixamo.com 3");

        Animation4.Stop("mixamo.com 1");
        Animation4.Play("mixamo.com 1");
        Animation4.Stop("mixamo.com 2");
        Animation4.Stop("mixamo.com 3");
    }
    public void AnimationIdl()
    {
        if (build.isPlaying)
            build.Stop();
        if (run.isPlaying)
            run.Stop();
        isMuveAnimation = false;
        isFallAnimation = false;
        Animation1.Stop("mixamo.com 2");
        Animation1.Stop("mixamo.com 1");
        Animation1.Play("mixamo.com 2");
        Animation1.Stop("mixamo.com 3");

        Animation2.Stop("mixamo.com 2");
        Animation2.Stop("mixamo.com 1");
        Animation2.Play("mixamo.com 2");
        Animation2.Stop("mixamo.com 3");

        Animation3.Stop("mixamo.com 2");
        Animation3.Stop("mixamo.com 1");
        Animation3.Play("mixamo.com 2");
        Animation3.Stop("mixamo.com 3");

        Animation4.Stop("mixamo.com 2");
        Animation4.Stop("mixamo.com 1");
        Animation4.Play("mixamo.com 2");
        Animation4.Stop("mixamo.com 3");
    }
    public void AnimationRise()
    {
        if (run.isPlaying)
            run.Stop();
        build.Play();

        isMuveAnimation = false;
        isFallAnimation = false;
        Animation1.Stop("mixamo.com 2");
        Animation1.Stop("mixamo.com 1");
        Animation1.Stop("mixamo.com 2");
        Animation1.Play("mixamo.com 3");

        Animation2.Stop("mixamo.com 2");
        Animation2.Stop("mixamo.com 1");
        Animation2.Stop("mixamo.com 2");
        Animation2.Play("mixamo.com 3");

        Animation3.Stop("mixamo.com 2");
        Animation3.Stop("mixamo.com 1");
        Animation3.Stop("mixamo.com 2");
        Animation3.Play("mixamo.com 3");

        Animation4.Stop("mixamo.com 2");
        Animation4.Stop("mixamo.com 1");
        Animation4.Stop("mixamo.com 2");
        Animation4.Play("mixamo.com 3");
    }
    public void AudioMute()
    {
        fail.mute = true;
        run.mute = true;
        jump.mute = true;
        build.mute = true;
    }
    public void AudioPlay()
    {
        fail.mute = false;
        run.mute = false;
        jump.mute = false;
        build.mute = false;
    }
}
