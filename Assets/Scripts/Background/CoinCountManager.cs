using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountManager : MonoBehaviour{
    public Inventory playerInventory;
    public Text coinCount;

    public void UpdateCoinCount(){
        coinCount.text = "" + playerInventory.coins;
    }
}
