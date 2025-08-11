using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.instance.ToggleGamePasue();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionButton.onClick.AddListener(() =>  {
            OptionsUI.instance.Show();
        });
    }
    private void Start()
    {
        KitchenGameManager.instance.OnGamePause += KitchenGameManager_OnGamePause;
        KitchenGameManager.instance.OnGameUnPause += KitchenGameManager_OnGameUnPause;

        Hide();
    }

    private void KitchenGameManager_OnGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePause(object sender, System.EventArgs e)
    {
       Show();
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
