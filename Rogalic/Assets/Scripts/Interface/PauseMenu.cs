using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void MainMenu()
    {
        gameController.ToMainMenu();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        gameController.ResumeGame();
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        gameController.Restart();
    }
}
