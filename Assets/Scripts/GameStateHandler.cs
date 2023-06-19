using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private const string MENU_SCENE = "MenuScene";
    private const string MAIN_SCENE = "MainScene";

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene(MENU_SCENE);
    }

    public static void PlayGame()
    {
        SceneManager.LoadScene(MAIN_SCENE);
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
    }
}
