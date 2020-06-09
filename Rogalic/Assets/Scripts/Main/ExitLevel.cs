using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private int nextLevelIndex;
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        StartCoroutine(gameController.SelectMap(nextLevelIndex));
    }
}
