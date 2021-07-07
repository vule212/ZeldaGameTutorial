using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour{

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public Vector2 cameraNewMax, cameraNewMin;
    public VectorValue cameraMin, cameraMax;
    public GameObject fadeInPanel, fadeOutPanel;
    public float fadeWait;

    void Start(){
        if(fadeInPanel != null){
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo(){
        if(fadeOutPanel != null){
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        ResetCameraBounds();
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOp.isDone){
            yield return null;
        }
    }

    public void ResetCameraBounds(){
        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
    }
}
 