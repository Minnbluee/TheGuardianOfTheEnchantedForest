using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    [Header("Configuración de Escenas")]
    [SerializeField] private string testSceneName = "Level1";

    private void Start()
    {
     
        AudioManager.Instance?.PlayMenuMusic();
    }

   
    public void OnPlayButtonClicked()
    {
        if (string.IsNullOrEmpty(testSceneName))
        {
            Debug.LogError("[MainMenuController] No se asignó el nombre de la escena de prueba.");
            return;
        }

        SceneManager.LoadScene(testSceneName);
    }

   
    public void OnQuitButtonClicked()
    {
        Debug.Log("[MainMenuController] Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
