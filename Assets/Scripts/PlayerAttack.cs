using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour{

    public float thrust, knockTime, damage;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("Player")){
            other.GetComponent<Breakable>().Smash();
        } else if((other.CompareTag("Enemy")&&this.CompareTag("Player")) || other.CompareTag("Player")){
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null){
                Vector2 difference = hit.transform.position - transform.position;
                difference.Normalize();
                difference *= thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if(other.CompareTag("Enemy") && other.isTrigger){
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                } 
                else if(other.CompareTag("Player")){
                    if(other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger){
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                        
                }
                
            }
        }
    }

    
}
