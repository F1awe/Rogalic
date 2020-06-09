using UnityEngine;
using UnityEngine.UI;

public class StatusInterface : MonoBehaviour
{
    [SerializeField] Text moneyText;
    private GameController gameController;
    [SerializeField] private LifeBar lifeBar;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void ShowMoneyCount(int count)
    {
        moneyText.text = count.ToString();
    }

    public void ShowLivesCount(int count)
    {
        lifeBar.SetCurrentLives(count);
    }

    public void Pause()
    {
        gameController.PauseGame();
    }
}
