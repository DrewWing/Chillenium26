using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    public Image backgroundImage;

    public Color highHealthColor = new Color(0.29f, 0.93f, 0.55f);
    public Color midHealthColor = new Color(1f, 0.76f, 0.18f);
    public Color lowHealthColor = new Color(1f, 0.27f, 0.27f);

    public float animationSpeed = 8f;

    private float targetFill = 1f;
    private float currentFill = 1f;
    private int maxHealth = 5;

    public void Initialize(int max)
    {
        maxHealth = max;
        targetFill = 1f;
        currentFill = 1f;
        fillImage.fillAmount = 1f;
        fillImage.color = highHealthColor;
    }

    public void SetHealth(int current)
    {
        targetFill = Mathf.Clamp01((float)current / maxHealth);
    }

    void Update()
    {
        if (!Mathf.Approximately(currentFill, targetFill))
        {
            currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * animationSpeed);
            fillImage.fillAmount = currentFill;
            fillImage.color = GetHealthColor(currentFill);
        }
    }

    private Color GetHealthColor(float pct)
    {
        if (pct > 0.6f)
            return Color.Lerp(midHealthColor, highHealthColor, (pct - 0.6f) / 0.4f);
        else if (pct > 0.3f)
            return Color.Lerp(lowHealthColor, midHealthColor, (pct - 0.3f) / 0.3f);
        else
            return lowHealthColor;
    }

    public IEnumerator ShakeRoutine()
    {
        Vector3 origin = transform.localPosition;
        float elapsed = 0f;
        float duration = 0.35f;
        float magnitude = 6f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = origin + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = origin;
    }

    public void TakeDamage(int current)
    {
        SetHealth(current);
        StartCoroutine(ShakeRoutine());
    }
}