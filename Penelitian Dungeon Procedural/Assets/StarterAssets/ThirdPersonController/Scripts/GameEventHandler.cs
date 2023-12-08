using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventHandler : MonoBehaviour
{
    public delegate void PlayerDeath(bool isPlayerDead);
    public static event PlayerDeath onPlayerDeath;

    public static void PlayerDied()
    {
        onPlayerDeath?.Invoke(true);
    }
}
