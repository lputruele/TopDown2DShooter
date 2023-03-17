using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagesUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void ShowMessage(string message)
    {        
        text.SetText(message);
        StartCoroutine(RemoveMessageCoroutine());
    }

    IEnumerator RemoveMessageCoroutine()
    {
        yield return new WaitForSeconds(2f);
        text.SetText("");
    }

}
