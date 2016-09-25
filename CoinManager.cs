using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CoinManager : MonoBehaviour
{


    public List<GameObject> liveCoins = new List<GameObject>(); // активные монеты

    public List<GameObject> disableCoins = new List<GameObject>(); // не активные(отключенные) монеты

    [SerializeField]
    GameObject coinPrefab;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float distance = 10;

    [SerializeField]
    float respavnTime = 5;
    

    float timer = 0; // для респавна монет

    GameManagerScript gameManagerScript;


    void Start() {

        gameManagerScript = gameObject.GetComponent<GameManagerScript>();
    }

    public void AddCoin(GameObject coin) // добавление монеты к активным
    {

        liveCoins.Add(coin);

    }

    public void DisableCoin(GameObject coin) // отключение и перемещение монеты к отключенным
    {
        coin.SetActive(false);
        liveCoins.Remove(coin);
        disableCoins.Add(coin);

    }

    void CreateCoin(Vector3 place) // создание новой монеты (место)
    {
        GameObject coin = Instantiate(coinPrefab, place, Quaternion.Euler(0f, Random.Range(0f, 360f), 90f)) as GameObject;
        AddCoin(coin);
    }


    void RespavnCoin(GameObject coin, Vector3 place) // реактивация монеты
    {

        coin.transform.position = place;
        coin.SetActive(true);
        liveCoins.Add(coin);
        disableCoins.Remove(coin);

    }


    void NewCoin(Vector3 place) // выбор использования отключенной, при отсутствии такой, создание новой монеты 
    {
        if (disableCoins.Count > 0)
        {
            
            RespavnCoin(disableCoins.Last(), place);
        }
        else {
            CreateCoin(place);
        }

    }


    void FindPlace() // нахождение место для появления новой монеты
    {
        Vector2 rand = Random.insideUnitCircle;
        Vector3 place = new Vector3(player.transform.position.x + rand.x * distance, 0f, player.transform.position.z + rand.y * distance);
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(place.x, 100f, place.z), Vector3.down, out hit, 150f))
        {
            place = new Vector3(place.x, 100f - hit.distance + 1f, place.z);

            NewCoin(place);

        }
        else {
            FindPlace();
        }




    }

    public void PlusCoin(GameObject coin) {
        DisableCoin(coin);
        gameManagerScript.PlusScore();
    }


    void Update()
    {

        timer += Time.deltaTime;
        if (timer > respavnTime)
        {
            timer = 0;
            FindPlace();
        }
    }
}
