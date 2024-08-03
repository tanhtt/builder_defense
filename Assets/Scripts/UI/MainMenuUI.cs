using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("StartGameBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadSceneManager.LoadScene(LoadSceneManager.Scene.GameScene);
        });

        transform.Find("QuitGameBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
