using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static event Action OnBlockDestroed;
    public static event Action OnGameOver;

    public static void SentBlockDestroed()
    {
        OnBlockDestroed?.Invoke();
    }

    public static void SentGameOver()
    {
        OnGameOver?.Invoke();
    }

}
