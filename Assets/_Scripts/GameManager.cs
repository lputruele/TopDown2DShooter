using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture = null;

    public GameObject loadingScreen;
    public GameObject gameOverUI;
    public Image loadingBarFill;

    [SerializeField]
    private GameObject pauseMenu;

    private void Start()
    {
        SetCursorIcon();
    }

    private void SetCursorIcon()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f), CursorMode.Auto);
        }
    }

    public void LoadTargetScene(string sceneName)
    {
        DOTween.KillAll();
        StartCoroutine(LoadTargetSceneAsync(sceneName));
    }
    IEnumerator LoadTargetSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = operation.progress;
            loadingBarFill.fillAmount = Mathf.Clamp01(progressValue);
            yield return null;
        }
        loadingScreen.SetActive(false);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (pauseMenu != null)
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                //DOTween.timeScale = 1f;
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenu.SetActive(true);
                //DOTween.timeScale = 0f;
                Time.timeScale = 0f;
            }
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
}
