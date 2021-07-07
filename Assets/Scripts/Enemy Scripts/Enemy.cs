using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle, walk, attack, stagger
}

public class Enemy : MonoBehaviour{

    public EnemyState currentState;
    public int baseAttack;
    public string enemyName;
    public float moveSpeed, health;
    public FloatValue maxHealth;
    public GameObject deathEffect;

    private void Awake(){
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage){
        health -= damage;
        if(health <= 0 ){
            DeathEffect();
            this.gameObject.SetActive(false);
        }
    }

    private void DeathEffect(){
        if(deathEffect != null){
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

    public void Knock(Rigidbody2D enemy, float knockTime, float damage){
        StartCoroutine(KnockCo(enemy, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D enemy, float knockTime){
        if(enemy != null){
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
            enemy.velocity = Vector2.zero;
        }
        
    }
       
}
