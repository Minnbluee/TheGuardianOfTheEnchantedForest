using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TestSceneController : MonoBehaviour
{
    [Header("Debug Info (opcional)")]
    [SerializeField] private TMP_Text debugText;

    private void Start()
    {
       
        AudioManager.Instance?.PlayGameplayMusic();

        UIManager.Instance?.ResetHearts();
        UIManager.Instance?.ResetDiamonds();
        UIManager.Instance?.ResetScore();

        UpdateDebugText();
    }

  
    public void SimulatePlayerHurt()
    {
        bool alive = UIManager.Instance.LoseHeart();
        AudioManager.Instance?.PlayPlayerHurt();

        Debug.Log(alive
            ? $"[Test] Jugador herido. Corazones restantes: {UIManager.Instance.CurrentHearts}"
            : "[Test] Jugador muerto. Reiniciando...");

        if (!alive)
            RestartScene();

        UpdateDebugText();
    }

    public void SimulateCollectDiamond()
    {
        bool portalReady = UIManager.Instance.AddDiamond();
        AudioManager.Instance?.PlayCollectDiamond();
        UIManager.Instance?.AddScore(100);

        if (portalReady)
            Debug.Log("[Test] ¡Portal activado! Suficientes diamantes recolectados.");

        UpdateDebugText();
    }

    public void SimulateAttack()
    {
        AudioManager.Instance?.PlayAttack();
        UIManager.Instance?.AddScore(50);
        Debug.Log("[Test] Ataque simulado.");
        UpdateDebugText();
    }

   
    public void SetMusicVolume(float value)
    {
        AudioManager.Instance?.SetMusicVolume(value);
    }

    
    public void SetSFXVolume(float value)
    {
        AudioManager.Instance?.SetSFXVolume(value);
    }

    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateDebugText()
    {
        if (debugText == null || UIManager.Instance == null) return;

        debugText.text =
            $"Corazones: {UIManager.Instance.CurrentHearts}\n" +
            $"Diamantes: {UIManager.Instance.CurrentDiamonds}\n" +
            $"Puntos: {UIManager.Instance.CurrentScore}\n" +
            $"Portal listo: {UIManager.Instance.HasEnoughDiamonds}";
    }
}
