using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour , IKitchenObjectParent
{
    public static Player Instance { get; private set; }


    public event EventHandler OnPickSomething;
    public event EventHandler<OnSelectCounterChangedEventArgs> OnSelectCounterChanged;
    public class OnSelectCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectCounter;
    }

    [SerializeField]private int playerSpeed = 5;
    [SerializeField]private GameInput gameInput;
    private string gameInputString = "GameInput";
    [SerializeField] LayerMask layermask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectCounter;
    private KitchenObject kitchenObject;
    

    private void Awake()
    {
        if (Instance == null)
        {
            lock (typeof(Player))
            {
                if (Instance == null)
                {
                    Instance = this;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }

        if (gameInput == null)
        {
            gameInput = GameObject.FindAnyObjectByType<GameInput>().GetComponent<GameInput>();
        }

    }

    private void Start()
    {
        
        
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.instance.IsGamePlaying()) return;
        if (selectCounter != null)
            selectCounter.InteractAlternate(this);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.instance.IsGamePlaying()) return;
        if (selectCounter != null)
            selectCounter.Interact(this);
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
       HandleInteractions();
    }

    void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2.0f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, interactDistance, layermask))
        {
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectCounter)
                {
                  SetSelectCounter(baseCounter);
                }
            }
            else
            {
               SetSelectCounter(null);
            }
        }
        else
        {
            SetSelectCounter(null);
        }

    }

    void PlayerMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = playerSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) moveDir = moveDirX;
            else
            {
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                
                if(canMove) moveDir = moveDirZ;
                else { }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        isWalking = moveDir != Vector3.zero;
        PlayerRotation(moveDir);
    }

    void PlayerRotation(Vector3 moveDir)
    {
        float rotateSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    void SetSelectCounter(BaseCounter selectCounter)
    {
        this.selectCounter = selectCounter;
        OnSelectCounterChanged?.Invoke(this, new OnSelectCounterChangedEventArgs
        {
            selectCounter = selectCounter
        });
    }

    void CheckMoveDiagonal(bool canMove)
    {
       
    }

    public bool IsWaking()
    {
        return isWalking;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null) 
        { 
            OnPickSomething?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;

    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
