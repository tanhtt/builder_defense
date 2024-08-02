using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadSceneManager.LoadScene(LoadSceneManager.Scene.GameScene);
        });

        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadSceneManager.LoadScene(LoadSceneManager.Scene.MainScene);
        });

        Hide();
    }

    public void Show()
    {
        transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>().SetText("You survived " + EnemyWaveManager.Instance.GetWaveNumber() + " waves");
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
