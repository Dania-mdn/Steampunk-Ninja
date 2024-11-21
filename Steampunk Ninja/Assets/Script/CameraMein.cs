using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMein : MonoBehaviour
{
    private bool isStop = false;
    public GameObject Character;
    private void OnEnable()
    {
        EventManager.Stop += SetStop;
    }
    private void OnDisable()
    {
        EventManager.Stop -= SetStop;
    }
    private void Update()
    {
        if (isStop)
        {
            transform.position = Vector3.Lerp(transform.position, Character.transform.position, 2 * Time.deltaTime);
            if (Character.transform.position.x - transform.position.x < 0.1f)
            {
                isStop = false;
            }
        }
    }
    private void SetStop()
    {
        isStop = true;
    }
}
