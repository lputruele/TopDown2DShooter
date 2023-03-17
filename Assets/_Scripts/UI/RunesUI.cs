using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunesUI : MonoBehaviour
{

    [SerializeField]
    private Image[] runesEquipedIcons = new Image[3];
    private int slotIndex = 0;

    public void UpdateRuneUI(RuneDataSO runeData)
    {
        if (slotIndex < 3)
        {
            runesEquipedIcons[slotIndex].gameObject.SetActive(true);
        }
        runesEquipedIcons[slotIndex % runesEquipedIcons.Length].sprite = runeData.Icon;
        slotIndex++;
    }
}
