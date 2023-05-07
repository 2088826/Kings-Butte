using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject powerUpContainer;
    [SerializeField] GameObject[] powerups;
    [SerializeField] float spawnTimer = 5f;

    private GameObject container;
    private GameObject tileContainer;
    private Transform[] map;
    private float rng;
    private float nextSpawn;
    private GameObject nextPowerUp;
    private Vector3 spawnPosition;
    private bool isFirst = true;
    private float yOffset = 0.25f;

    private void Awake()
    {
        container = Instantiate(powerUpContainer);
        container.name = "PowerUpContainer";
    }
    // Start is called before the first frame update
    void Start()
    {
        tileContainer = GameObject.Find("TileContainer");
        map = tileContainer.GetComponentsInChildren<Transform>();
        Debug.Log(map.Length);
        nextSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.IsStart && !GameManager.IsPaused && nextSpawn <= Time.time)
        {
            SpawnPowerUp();
        }

        if (GameManager.IsPaused)
        {
            nextSpawn = Time.time + spawnTimer;
        }
    }

    private void SpawnPowerUp()
    {
        nextSpawn = Time.time + spawnTimer;

        rng = Random.Range(0, 100);

        if(rng < 50f)
        {
            nextPowerUp = powerups[0];
        }
        else
        {
            nextPowerUp = powerups[1];
        }

        if (isFirst)
        {
            isFirst = false;

            int middle = (int) Mathf.Round((map.Length) / 2);

            spawnPosition = map[middle].position;

            spawnPosition.y += yOffset;      

            GameObject newPowerUp = Instantiate(nextPowerUp, spawnPosition, Quaternion.identity, container.transform);

            string tag = DetermineTag(nextPowerUp);

            Transform childTransform = newPowerUp.transform.Find("PickupCollider");
            if (childTransform != null)
            {
                childTransform.gameObject.tag = tag;
            }

        }
        else
        {

            spawnPosition = map[Random.Range(0, map.Length - 1)].transform.position;

            spawnPosition.y += yOffset;

            GameObject newPowerUp = Instantiate(nextPowerUp, spawnPosition, Quaternion.identity, container.transform);

            string tag = DetermineTag(nextPowerUp);

            Transform childTransform = newPowerUp.transform.Find("PickupCollider");
            if (childTransform != null)
            {
                childTransform.gameObject.tag = tag;
            }
        }
    }

    private string DetermineTag(GameObject powerup)
    {
        if (powerup.name.Contains("Speed"))
        {
            return "Haste";
        }
        else if (powerup.name.Contains("CoolDown"))
        {
            return "Cooldown";
        }

        return null;
    }
}
