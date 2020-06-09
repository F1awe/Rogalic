using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private const string MAP_INDEX_KEY = "MAP_INDEX";

    [SerializeField] private Fader fader;
    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private List<GameObject> maps;
    private GameObject currentMap;
    private int currentMapIndex;

    void Start()
    {
        LoadData();
        StartCoroutine(SelectMap(currentMapIndex));
    }

    public IEnumerator SelectMap(int levelIndex)
    {
        currentMapIndex = levelIndex;

        fader.gameObject.SetActive(true);
        yield return StartCoroutine(fader.Fade(true));

        if (currentMap != null)
        {
            SaveData();

            Destroy(currentMap);
        }

        currentMap = Instantiate(maps[levelIndex], transform.position, Quaternion.identity);

        StartGame();

    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(MAP_INDEX_KEY, currentMapIndex);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SaveData();
    }

    private void LoadData()
    {
        currentMapIndex = PlayerPrefs.HasKey(MAP_INDEX_KEY) ? PlayerPrefs.GetInt(MAP_INDEX_KEY) : 0;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey(MAP_INDEX_KEY);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ResetData();
        StartCoroutine(SelectMap(0));
    }

    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void LoseGame()
    {
        StartCoroutine(LoseRoutine());
    }

    private IEnumerator StartRoutine()
    {
        yield return StartCoroutine(fader.Fade(false));

        fader.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    private IEnumerator LoseRoutine()
    {
        Time.timeScale = 0.4f;

        fader.gameObject.SetActive(true);

        yield return StartCoroutine(fader.Fade(true));

        StartCoroutine(fader.StartBlinkRetryBtn());
    }
}
