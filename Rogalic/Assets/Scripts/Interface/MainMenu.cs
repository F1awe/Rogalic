using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Fader fader;

    public void Play()
    {
        StartCoroutine(LoadGame());
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator LoadGame()
    {
        fader.gameObject.SetActive(true);

        yield return StartCoroutine(fader.Fade(true));

        SceneManager.LoadScene(1);
    }
}
