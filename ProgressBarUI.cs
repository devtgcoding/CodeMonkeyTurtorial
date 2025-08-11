 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameOBject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameOBject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressGameOBject + " does not have a Component thah implements IHasProgress");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized == 1f) 
        {
            Hide();
        }
        else
        {
            show();
        }
    }

    private void show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
