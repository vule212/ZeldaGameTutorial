using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour{

    public GameSignal context;
    public bool playerInRange;
    
    void Update(){
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = true;
            context.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = false;
            context.Raise();
        }
    }
}
