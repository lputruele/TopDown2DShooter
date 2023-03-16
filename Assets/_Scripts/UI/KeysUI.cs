using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void UpdateKeysText(int keys)
    {
        text.SetText(keys.ToString());
    }

}
