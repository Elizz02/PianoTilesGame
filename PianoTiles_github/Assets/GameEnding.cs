using UnityEngine;
using TMPro;  // Pastikan ini ada untuk TextMeshPro
using UnityEngine.SceneManagement; // Untuk mengelola scene
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
        // Menampilkan teks "Game Ends" jika belum ditampilkan
        if (!isShowingText)
        {
            StartCoroutine(ShowGameEndingText());
            isShowingText = true;
        }

        // Hentikan permainan jika pemain menekan tombol Escape atau dengan metode lain (opsional)
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            QuitGame();
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

        // Tunggu beberapa detik sebelum memberikan kesempatan bagi pemain untuk melihat pesan
        yield return null; // Tidak ada delay lagi di sini
    }

    // Fungsi untuk keluar dari permainan (manual)
    private void QuitGame()
    {
        // Keluar dari aplikasi jika build
        Application.Quit();

        // Jika di editor Unity, berhenti dari pemutaran permainan
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Fungsi untuk kembali ke Main Menu
    public void LoadMainMenu()
    {
        // Reset game elements (like score, music, etc.)
        ResetGame();

        // Muat scene Main Menu
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene main menu Anda
    }

    // Fungsi untuk mereset game (seperti score, musik, dll)
    private void ResetGame()
    {
        // Reset Score
        // Contoh:
        // ScoreManager.instance.ResetScore();

        // Reset Music (misalnya, hentikan musik atau reset audio source)
        // Contoh:
        // AudioManager.instance.ResetMusic();

        // Reset atau stop semua elemen yang perlu di-reset
        // Misalnya, jika Anda memiliki sistem yang melacak game time, status karakter, atau lainnya, reset di sini
    }
}
