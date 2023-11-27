using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EventUIHandler : MonoBehaviour
{
    public GameObject GameOverUI;
    public Image gameOverBackground;
    public GameObject gameOverForeground;
    public TextMeshProUGUI gameOverText;
    public CanvasGroup restartAndMainMenuButtons;


    private void Start()
    {

    }

    //Game event subscription
    void OnEnable()
    {
        GameEventHandler.onPlayerDeath += SetGameOverScreen;
    }

    void OnDisable()
    {
        GameEventHandler.onPlayerDeath -= SetGameOverScreen;
    }

    public void SetGameOverScreen()
    {
        GameOverUI.SetActive(true);
        FadeInGameOver(gameOverBackground, 3.0f);
    }

    IEnumerator FadeInGameOver(Image imageToChange, float fadeDuration)
    {
        yield return new WaitForSeconds(2.0f);
        float elapsedTime = 0f;
        Color imageAlpha = imageToChange.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageAlpha.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            imageToChange.color = imageAlpha;
        }
    }
}
