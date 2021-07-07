using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable{

    public Item contents;
    public int isOpen = 0;  //0-not opened, 1-open, 2-done
    public Inventory playerInventory;
    public GameSignal raiseItem;
    public GameObject dialogueBox;
    public Text dialogueText;
    public Animator anim;


    // Start is called before the first frame update
    void Start(){
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Interact") && playerInRange){
            if(isOpen == 0){
                OpenChest();
            } else if(isOpen == 1){
                AlreadyOpened();
            }
        }
    }

    public void OpenChest(){
        dialogueBox.SetActive(true);
        dialogueText.text = contents.description;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        context.Raise();
        anim.SetBool("opened", true);
        raiseItem.Raise();
        isOpen = 1;
    }

    public void AlreadyOpened(){
        dialogueBox.SetActive(false);
        raiseItem.Raise();
        isOpen = 2;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger && isOpen==0){
            playerInRange = true;
            context.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger && isOpen==0){
            playerInRange = false;
            context.Raise();
        }
    }
}
