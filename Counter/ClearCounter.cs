using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSo KitchenObjectSO;
    
    public override void Interact(Player player)
    {   
        //ถ้าไม่มีObjectบนCounter
        if (!HasKitchenObject())
        {
            //ถ้าPlayerถือObjectอยู่ ให้วางObjectบนCounterได้
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                
            }
        }
        //ถ้ามีObjectบนCounter
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    //Player is not carrying Plate but something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSo()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }

            }
            //ถ้าPlayerไม่มีObjectอยู่ ให้รับObjectบนCounter
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

   
}
