using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushClilinder : MonoBehaviour
{
    public float Coldawn;
    private bool isStatik = false;

    private void Update()
    {
        Coldawn = Coldawn - Time.deltaTime;
        
        if(Coldawn < 4)
        {
            if (isStatik == false)
            {
                gameObject.AddComponent<Rigidbody>();
                isStatik = true;
            }
            if(Coldawn < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
