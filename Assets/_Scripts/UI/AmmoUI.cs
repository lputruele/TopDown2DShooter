using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void UpdateAmmoText(int bulletCount)
    {
        if (bulletCount == 0)
        {
            text.color = Color.red;
        }
        else{
            text.color = Color.white;
        }
        text.SetText(bulletCount.ToString());
    }
}
