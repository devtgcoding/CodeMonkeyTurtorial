using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectCounterChanged += Player_OnselectedCounterChanged;
    }

    private void Player_OnselectedCounterChanged(object sender, Player.OnSelectCounterChangedEventArgs e)
    {
        if(e.selectCounter == baseCounter)
        {
             Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);

        }
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray) 
        {
            visualGameObject.SetActive(false); 
        }
    }
}
