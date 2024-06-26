using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TrashBinCoin : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public static int coinCurrent;
    public static int coinMax;

    private Image trashBinBar;
    // Start is called before the first frame update
    void Start()
    {
        trashBinBar = GetComponent<Image>();
        coinCurrent = 0;
        coinMax = 10;
    }

    // Update is called once per frame
    void Update()
    {
        trashBinBar.fillAmount = (float)coinCurrent / (float)coinMax;
        tmp.text = coinCurrent.ToString() + "/" + coinMax.ToString();
    }
}
