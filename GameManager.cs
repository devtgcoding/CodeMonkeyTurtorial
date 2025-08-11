using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string PLAYER_MAINCOIN = "MainCoin";
    public static GameManager Instance { get; private set; }

    [SerializeField]private int MainCoin;
    [SerializeField]private int gameCoin;
    [SerializeField]private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI mainCoinText;
    public bool isStartGame = false;





    private void Awake()
    {
            Instance = this;
            isStartGame = true;
    }
    private void Start()
    {
        KitchenGameManager.instance.OnStateChanged += KitchenGamemanager_OnStateChanged;
        
        
    }

    private void KitchenGamemanager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.instance.IsGameOver()) {
            SetMainCoin(gameCoin);
           
        }
    }

    public void SetCoin(int coin)
    {
        gameCoin += coin;
        coinText.text = "Coin : " + gameCoin;
    }

    private void SetMainCoin(int gameCoin)
    {
        MainCoin += gameCoin;
        mainCoinText.text = "MainCoin : " + MainCoin;
      
    }

    


}
