using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject platekitchenObject;
    [SerializeField] private Transform iconTemplate;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        platekitchenObject.OnIngredientAdded += PlatekitchenObject_OnIngredientAdded;
    }
    private void PlatekitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        //ทำลายIconก่อนหน้า
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        //สร้างIcon
        foreach (KitchenObjectSo kitchenObjectSo in platekitchenObject.GetKitchenObjectSOList()) 
        {
            Transform iconTranform = Instantiate(iconTemplate, transform);
            iconTranform.gameObject.SetActive(true);
            iconTranform.GetComponent<PlateIconSingleUI>().SetkitchenObjectSO(kitchenObjectSo);
        }
    }
}
