using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI text;
    private RectTransform backgroundRect;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();
        text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRect = transform.Find("background").GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        if (canvasRectTransform == null)
        {
            Debug.LogWarning("canvasRectTransform is null. Please assign it in the Inspector.");
            return;
        }
        if (rectTransform == null || backgroundRect == null)
        {
            Debug.LogWarning("RectTransform or backgroundRect is null. Please check the initialization.");
            return;
        }
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if(anchoredPosition.x + backgroundRect.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRect.rect.width;
        }
        if(anchoredPosition.y + backgroundRect.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRect.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {
        this.text.SetText(tooltipText);
        this.text.ForceMeshUpdate();

        Vector2 textSize = this.text.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRect.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
