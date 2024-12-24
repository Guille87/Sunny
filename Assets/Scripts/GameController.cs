using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] TMP_Text itemsText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] int totalSeconds;

    float elapsedTime;
    
    void Update()
    {
        // Actualizamos el contador de tiempo
        elapsedTime += Time.deltaTime;
        int secondsLeft = totalSeconds - (int)elapsedTime;
        if (secondsLeft < 0) secondsLeft = 0;
        timerText.text = secondsLeft.ToString();

        // Actualizamos el contador de gemas
        int items = GameObject.FindGameObjectsWithTag("Gem").Length;
        itemsText.text = items.ToString();

        if (items == 0 || secondsLeft == 0)
        {
            Invoke(nameof(Restart), 1f);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
