using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyTrash;
    new public static void ResetStaticData()
    {
        OnAnyTrash = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) 
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyTrash?.Invoke(this, new EventArgs());
        }
    }
}
