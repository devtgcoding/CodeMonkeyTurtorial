using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSo kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSo> validKitchenObjectSOList;

    private List<KitchenObjectSo> kitchenObjectSOList;
     private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSo>();
    }
    public bool TryAddIngredient(KitchenObjectSo kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;

        }
       
    }

    public List<KitchenObjectSo> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
