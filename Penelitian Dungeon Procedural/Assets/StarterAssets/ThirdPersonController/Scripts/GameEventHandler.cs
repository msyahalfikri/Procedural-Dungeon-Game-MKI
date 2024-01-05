using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventHandler : MonoBehaviour
{
    public delegate void PlayerDeath(bool isPlayerDead);
    public static event PlayerDeath onPlayerDeath;
    public delegate void PortalInteracted(bool portalInteracted);
    public static event PortalInteracted onPortalInteracted;

    public static void PlayerDied()
    {
        onPlayerDeath?.Invoke(true);
    }
    public static void PortalIsInteracted()
    {
        onPortalInteracted?.Invoke(true);
    }
}
