using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash(){
        animator.SetBool("smash", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo(){
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);
    }
}
