using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    public Image loadingBarFill;

    public void SetLoadingPercentage(float progressValue)
    {
        loadingBarFill.fillAmount = Mathf.Clamp01(progressValue);
    }
}
