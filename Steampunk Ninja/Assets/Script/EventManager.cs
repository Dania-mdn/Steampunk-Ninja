using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action Muve; 
    public static event Action Stop; 
    public static event Action EndGame;
    public static event Action MuteAudio;
    public static event Action PlayAudio;
    public static void DoMuve()
    {
        Muve?.Invoke();
    }
    public static void DoStop()
    {
        Stop?.Invoke();
    }
    public static void DoEndGame()
    {
        EndGame?.Invoke();
    }
    public static void DoMuteAudio()
    {
        MuteAudio?.Invoke();
    }
    public static void DoPlayAudio()
    {
        PlayAudio?.Invoke();
    }
}
