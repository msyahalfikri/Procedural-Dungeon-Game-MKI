using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBarAndEmotion : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Image backgroundImage;
    public Image foregroundImage;
    public Image emotionUI;
    public Sprite[] emotionsUITypes;
    public TextMeshProUGUI enemyNameUI;
    AIAgent agent;
    [HideInInspector] float InitialWidth;
    AIEmotionSimulator emotionSimulator;

    public string enemyName;
    private void Awake()
    {
        agent = GetComponentInParent<AIAgent>();
        foregroundImage = transform.Find("Foreground").GetComponent<Image>();
        backgroundImage = transform.Find("Background").GetComponent<Image>();
        enemyNameUI = transform.Find("EnemyName").GetComponent<TextMeshProUGUI>();
        InitialWidth = transform.Find("Foreground").GetComponent<RectTransform>().rect.width;
        emotionSimulator = GetComponentInParent<AIEmotionSimulator>();
        emotionUI = transform.Find("EmotionUI").GetComponent<Image>();

    }
    private void Start()
    {
        enemyNameUI.text = enemyName;
    }

    void LateUpdate()
    {
        SetEmotionUI();
        if (!agent.isInChaseRange)
        {
            foregroundImage.enabled = false;
            backgroundImage.enabled = false;
            emotionUI.enabled = false;
            enemyNameUI.enabled = false;
        }
        else
        {
            Vector3 direction = (target.position - Camera.main.transform.position).normalized;
            bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;
            foregroundImage.enabled = !isBehind;
            backgroundImage.enabled = !isBehind;
            emotionUI.enabled = !isBehind;
            enemyNameUI.enabled = !isBehind;
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }

    public void SetHealthBarPercentage(float percentage)
    {
        float width = InitialWidth * percentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    private void SetEmotionUI()
    {
        switch (emotionSimulator.currentEmotion)
        {
            case AIEmotionTypes.Calm:
                emotionUI.sprite = emotionsUITypes[0];
                break;
            case AIEmotionTypes.Annoyed:
                emotionUI.sprite = emotionsUITypes[1];
                break;
            case AIEmotionTypes.Furious:
                emotionUI.sprite = emotionsUITypes[2];
                break;
            case AIEmotionTypes.Apprehensive:
                emotionUI.sprite = emotionsUITypes[3];
                break;
            case AIEmotionTypes.Terrified:
                emotionUI.sprite = emotionsUITypes[4];
                break;
            case AIEmotionTypes.Firm:
                emotionUI.sprite = emotionsUITypes[5];
                break;
            case AIEmotionTypes.Determined:
                emotionUI.sprite = emotionsUITypes[6];
                break;
            default:
                emotionUI.sprite = emotionsUITypes[0];
                break;
        }
    }
}
