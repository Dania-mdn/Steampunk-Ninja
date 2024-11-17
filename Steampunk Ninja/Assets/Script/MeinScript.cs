using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeinScript : MonoBehaviour
{
    private bool isHold = false;
    public void Down()
    {
        isHold = true;
    }
    public void Up()
    {
        isHold = false;
    }
    private void Update()
    {
        if (isHold)
        {
            Debug.Log(1);
        }
    }
}
