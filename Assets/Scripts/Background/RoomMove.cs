using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraMin, cameraMax;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool newArea;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start(){
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            cam.minPosition = cameraMin;
            cam.maxPosition = cameraMax;
            other.transform.position += playerChange;
            if(newArea){
                StartCoroutine(showPlaceName());
            }
        }
    }

    private IEnumerator showPlaceName(){
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
}
