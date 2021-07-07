using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour{

    public GameObject contextClue, interactionButton;
    public bool contextActive = false;

    public void ChangeContext(){
        contextActive = !contextActive;
        if(contextActive){
            contextClue.SetActive(true);
            interactionButton.SetActive(true);
        } else {
            contextClue.SetActive(false);
            interactionButton.SetActive(false);
        }
    }

}
