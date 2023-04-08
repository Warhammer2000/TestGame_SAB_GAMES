using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode pickUpItemKey = KeyCode.E;

    public static PlayerController instance;

    public float speed = 5.0f;

    private Vector3 movement;

    private Rigidbody playerRigidbody;

    private  IInventoryService inventoryService;

    public IEnemyService ienemyService;

    private JumpAction jumpAction = new JumpAction();
    private AttackAction attackAction = new AttackAction();
    private UseItemAction useItemAction = new UseItemAction();

    //public PlayerController(IEnemyService enemyService, IInventoryService inventoryService, AttackAction attackAction)
    //{
    //    this.ienemyService = enemyService;
    //    this.inventoryService = inventoryService;
    //    this.attackAction = attackAction;

    //}
    private void OnEnable()
    {
        instance = this;
    }
    private void Start()
    {
        instance = this;
        inventoryService = GetComponent<IInventoryService>();
        ienemyService = GetComponent<IEnemyService>();   
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            jumpAction.Execute(gameObject);
        }

        if (Input.GetKeyDown(attackKey))
        {
            attackAction.Execute(gameObject);
        }

        if (Input.GetKeyDown(pickUpItemKey))
        {
            useItemAction.Execute(gameObject);
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontalInput, 0.0f, verticalInput);
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(movement * speed);
    }
}


