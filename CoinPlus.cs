using UnityEngine;
using System.Collections;

public class CoinPlus : MonoBehaviour {

    // скрипт игрока для сбора монет

    [SerializeField] GameObject GameManager;

    CoinManager coinManager;


    void Start() {
        coinManager = GameManager.GetComponent<CoinManager>();

    }


    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.tag == "Coin")
        {

            coinManager.PlusCoin(coll.gameObject);

        }

        
    }

    
}
