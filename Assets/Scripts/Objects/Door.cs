using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorType{ 
    key, enemy, button
}
public class Door : Interactable {

    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open;
    public Inventory playerInventory;
    private SpriteRenderer doorSprite;
    private BoxCollider2D doorCollider;

    public GameObject dialogueBox;
    public Text dialogueText;

    void Start(){
        doorSprite = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
    }

    void Update(){
        if(Input.GetButtonDown("Interact")){
            if(playerInRange && thisDoorType == DoorType.key){
                //Does player have key?
                if(playerInventory.numOfKeys > 0){
                    playerInventory.numOfKeys--;
                    Open();
                } else {
                    //say don't have key
                    if(dialogueBox.activeInHierarchy){
                        dialogueBox.SetActive(false);
                    } else {
                        dialogueBox.SetActive(true);
                        dialogueText.text = "I don't have a key.";
                    }
                }
            }
        }
    }

    public void Open(){
        //Turn off door's sprite renderer
        doorSprite.enabled = false;
        //set open true
        open = true;
        //turn off door's box collider
        doorCollider.enabled = false;
    }

    public void Close(){

    }
}
