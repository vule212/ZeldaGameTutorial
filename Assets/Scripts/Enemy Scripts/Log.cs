using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy{

    public Rigidbody2D myRigidbody;
    public Transform target, homePosition;
    public float chaseRadius, attackRadius;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetBool("wake", true);
    }

    // Update is called once per frame
    void FixedUpdate(){
        CheckDistance();
    }

    public virtual void CheckDistance(){
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) >= attackRadius){
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger){
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
                myRigidbody.MovePosition(temp);
                ChangeAnim(temp - transform.position);
                ChangeState(EnemyState.walk);
                animator.SetBool("wake", true);
            }
            
        } else if(Vector3.Distance(target.position, transform.position) > chaseRadius){
            animator.SetBool("wake", false);
        }
    }

    public void ChangeAnim(Vector2 direction){
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
             if(direction.x > 0){
                animator.SetFloat("moveX", 1);
                animator.SetFloat("moveY", 0);
             } else {
                animator.SetFloat("moveX", -1);
                animator.SetFloat("moveY", 0);
             }
        } else {
            if(direction.y > 0){
                animator.SetFloat("moveY", 1);
                animator.SetFloat("moveX", 0);
             } else {
                animator.SetFloat("moveY", -1);
                animator.SetFloat("moveX", 0);
             }
        }
    }

    private void ChangeState(EnemyState newState){
        if(currentState != newState){
            currentState = newState;
        }
    }
}
