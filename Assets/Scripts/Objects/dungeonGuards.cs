using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonGuards : Sign {

    public GameObject enemyToDefeat;
    public Animator anim;
    public string enterDialogue;

    // Start is called before the first frame update
    void Start(){
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Interact") && playerInRange && enemyToDefeat.activeInHierarchy){
            if(dialogueBox.activeInHierarchy){
                dialogueBox.SetActive(false);
            } else {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue;
            }
        } else if(Input.GetButtonDown("Interact") && playerInRange){
            anim.SetBool("hasKey", true);
            if(dialogueBox.activeInHierarchy){
                dialogueBox.SetActive(false);
            } else {
                dialogueBox.SetActive(true);
                dialogueText.text = enterDialogue;
            }
        }
    }
}