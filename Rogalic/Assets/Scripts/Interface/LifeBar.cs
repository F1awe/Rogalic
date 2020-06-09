using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    private List<GameObject> lives;
    private int maxLivesCount;

    private void Awake()
    {
        maxLivesCount = transform.childCount;

        lives = new List<GameObject>();

        for (int i = 0; i < maxLivesCount; i++)
        {
            lives.Add(transform.GetChild(i).gameObject);
            lives[i].SetActive(false);
        }
    }

    public void SetCurrentLives(int count)
    {
        for (int i = 0; i < maxLivesCount; i++)
        {
            lives[i].SetActive(i < count);
        }
    }


}
