using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Text uiText;
    public TextMeshProUGUI tmp;

    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        string formatted = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (tmp != null)
            tmp.text = formatted;
        else if (uiText != null)
            uiText.text = formatted;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetTime()
    {
        return elapsedTime;
    }
}
