using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action Muve; 
    public static void DoMuve()
    {
        Muve?.Invoke();
    }
}
