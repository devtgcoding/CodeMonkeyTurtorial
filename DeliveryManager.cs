using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeComplete;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;

    public static DeliveryManager instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    [SerializeField] private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private int successfulRecipesAmount;



    private void Awake()
    {
        instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
               
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateCobtebtMatchesRecipe = true;
                foreach (KitchenObjectSo recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSo plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateCobtebtMatchesRecipe = false;
                    }
                }
                if (plateCobtebtMatchesRecipe)
                {
                    //Player Delivery the correct recipe
                    successfulRecipesAmount++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeComplete?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    GameManager.Instance.SetCoin(5);
                    return;
                }
            }
        }
        //No matches found!
        //Player not delivery
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        Debug.Log("Player not delivery");
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
