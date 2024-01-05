using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EventUIHandler : MonoBehaviour
{
    public GameObject gameOverUIGameObject;
    public GameObject thanksForPlayingUIGameObject;
    public CanvasGroup gameOverCanvasGroup;
    public CanvasGroup thanksForPlayingCanvasGroup;
    // public GameObject gameOverForeground;
    // public TextMeshProUGUI gameOverText;
    // public CanvasGroup restartAndMainMenuButtons;


    private void Start()
    {

    }

    public void SetGameOverScreen(bool playerIsDead)
    {
        gameOverUIGameObject.SetActive(true);
        StartCoroutine(FadeInCanvasGroup(gameOverCanvasGroup, 2.0f));
    }
    public void SetThanksForPlayingScreen(bool gameFinished)
    {
        thanksForPlayingUIGameObject.SetActive(true);
        StartCoroutine(FadeInCanvasGroup(thanksForPlayingCanvasGroup, 2.0f));
    }

    IEnumerator FadeInCanvasGroup(CanvasGroup canvasToChange, float fadeDuration)
    {
        yield return new WaitForSeconds(2.0f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            canvasToChange.alpha = alpha;
            yield return null; // Wait for the next frame
        }
    }

    //Game event subscription
    void OnEnable()
    {
        GameEventHandler.onPlayerDeath += SetGameOverScreen;
        GameEventHandler.onPortalInteracted += SetThanksForPlayingScreen;
    }

    void OnDisable()
    {
        GameEventHandler.onPlayerDeath -= SetGameOverScreen;
        GameEventHandler.onPortalInteracted -= SetThanksForPlayingScreen;
    }
}
