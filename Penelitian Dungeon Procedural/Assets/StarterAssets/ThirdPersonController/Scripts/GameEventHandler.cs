using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventHandler : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;

    public static void PlayerDied()
    {
        onPlayerDeath?.Invoke();
    }
}
