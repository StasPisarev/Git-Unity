using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {

    // скрипт конца уровня
    [SerializeField]
    GameObject gameManager;
    GameManagerScript gameManagerScript;

    void Start() {
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

   void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player") {
            gameManagerScript.Win();
        }
    }
}
