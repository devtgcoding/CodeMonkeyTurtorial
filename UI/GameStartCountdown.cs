using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
   
    private void Start()
    {
        KitchenGameManager.instance.OnStateChanged += Instance_OnStateChanged;
        Hide();
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
            
        }
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(KitchenGameManager.instance.GetCountDownToStartTimer()).ToString();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
