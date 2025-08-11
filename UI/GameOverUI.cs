using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        KitchenGameManager.instance.OnStateChanged += Instance_OnStateChanged;
        Hide();
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.instance.GetSuccessfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
            
        }
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
