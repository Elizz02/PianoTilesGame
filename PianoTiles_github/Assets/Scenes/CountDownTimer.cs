using UnityEngine;
using TMPro;  // Jangan lupa untuk mengimpor TMP
using UnityEngine.SceneManagement;

public class CountdownController : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Variabel harus bertipe TextMeshProUGUI

    private float countdownTime = 5f;

    void Start()
    {
        countdownText.text = countdownTime.ToString();  // Set awal countdown
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;  // Kurangi waktu countdown

        countdownText.text = Mathf.Ceil(countdownTime).ToString();  // Update UI countdown

        if (countdownTime <= 0)
        {
            SceneManager.LoadScene("GameScene");  // Pindah ke scene Game
        }
    }
}
