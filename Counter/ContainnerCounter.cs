using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainnerCounter : BaseCounter 
{
    public event EventHandler OnPlayerGrabbedObject;
   
    [SerializeField] private KitchenObjectSo KitchenObjectSO;
    

    public override void Interact(Player player)
    {
        //ถ้าPlayerไม่ได้ถือObjectอยู่ ให้Objectแก่player
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(KitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);    
        }
            
    }
   
}
