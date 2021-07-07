using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk, attack, interact, stagger, idle
}

public class PlayerMovement : MonoBehaviour{

    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public GameSignal playerHealthSignal, cameraKick, playerDeath;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    // Start is called before the first frame update
    void Start(){
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update(){

        if(currentState == PlayerState.interact){
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        
        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger){
            StartCoroutine(AttackCoroutine());
        } 
    }
    
    void FixedUpdate(){
        if (currentState == PlayerState.walk || currentState == PlayerState.idle){
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCoroutine(){
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.25f);
        if(currentState != PlayerState.interact){
            currentState = PlayerState.walk;
        } 
    }

    public void RaiseItem(){
        if(playerInventory.currentItem != null){
            if(currentState != PlayerState.interact){
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            } else {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove(){
        if(change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter(){
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed*(Screen.width/Screen.height) * Time.deltaTime);
    }

    public void Knock(float knockTime, float damage){
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        cameraKick.Raise();
        if(currentHealth.RuntimeValue > 0){
            StartCoroutine(KnockCo(knockTime));
        } else {
            this.gameObject.SetActive(false);
            playerDeath.Raise();
        }
    }

    private IEnumerator KnockCo(float knockTime){
        yield return new WaitForSeconds(knockTime);
        myRigidbody.velocity = Vector2.zero;
        currentState = PlayerState.idle;
        myRigidbody.velocity = Vector2.zero;
    }
}
