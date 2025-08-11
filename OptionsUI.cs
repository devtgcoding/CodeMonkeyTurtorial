using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance{ get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        instance = this;
        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.instance.ChangeVolume();
            UpdateVisual() ;
        });

        closeButton.onClick.AddListener(() => {
            Hide();
        });
    }

    private void Start()
    {
        KitchenGameManager.instance.OnGamePause += KitchenGameManager_OnGamePause;
        UpdateVisual();
        Hide();
    }

    private void KitchenGameManager_OnGamePause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.instance.GetVolume() * 10);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        
    }
}
