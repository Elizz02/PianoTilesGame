using UnityEngine;
using TMPro;  // Pastikan ini ada untuk TextMeshPro
using System.Collections;

public class GameEnding : MonoBehaviour
{
    // Referensi ke TextMeshProUGUI untuk menampilkan pesan
    public TextMeshProUGUI gameEndingText;

    // Flag untuk menampilkan teks "Game Ends"
    private bool isShowingText = false;

    void Start()
    {
        // Menyembunyikan teks "Game Ends" pada awal
        if (gameEndingText != null)
        {
            gameEndingText.gameObject.SetActive(false);  // Menyembunyikan teks
        }
    }

    void Update()
    {
        // Menampilkan teks "Game Ends" setelah beberapa detik (misalnya 1 detik)
        if (!isShowingText)
        {
            StartCoroutine(ShowGameEndingText());
            isShowingText = true;
        }
    }

    // Fungsi untuk menampilkan pesan "Game Ends"
    private IEnumerator ShowGameEndingText()
    {
        // Tampilkan teks "Game Ends"
        if (gameEndingText != null)
        {
            gameEndingText.gameObject.SetActive(true);
            gameEndingText.text = "The End"; // Teks yang akan ditampilkan
        }

        // Tunggu beberapa detik (misalnya 3 detik) sebelum mengakhiri permainan
        yield return new WaitForSeconds(3f);

        // Akhiri permainan
        Application.Quit();

        // Jika di editor Unity, berhenti dari pemutaran permainan
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
