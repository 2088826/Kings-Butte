using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject powerUpContainer;
    [SerializeField] GameObject[] powerups;
    [SerializeField] float powerUpSpawnTimer = 5f;
    [SerializeField] GameObject rock;
    [SerializeField] float rockSpawnTimer = 20f;

    private GameObject container;
    private GameObject tileContainer;
    private Transform[] map;
    private float rng;
    private float nextSpawn;
    private float nextRock;
    private GameObject nextPowerUp;
    private Vector3 spawnPosition;
    private bool isFirst = true;
    private float yOffset = 0.25f;

    private void Awake()
    {
        container = Instantiate(powerUpContainer);
        container.name = "PowerUpContainer";
        nextSpawn = powerUpSpawnTimer;
        nextRock = rockSpawnTimer;
    }

    private void OnEnable()
    {
        tileContainer = GameObject.Find("TileContainer");

        int childCount = tileContainer.transform.childCount;
        map = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            map[i] = tileContainer.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused && GameManager.IsStart)
        {
            // First Powerup
            if (isFirst)
            {
                FirstPowerUp();
            }

            // Powerup Spawner
            if(nextSpawn > 0.01f)
            {
                nextSpawn -= Time.deltaTime;

            }
            else
            {
                SpawnPowerUp();
            }

            // Rock Spawner
            if(nextRock > 0.01f)
            {
                nextRock -= Time.deltaTime;
            }
            else
            {
                SpawnRock();
            }

        }
    }

    /// <summary>
    /// Spawn a rock that falls from the sky.
    /// </summary>
    private void SpawnRock()
    {
        nextRock = rockSpawnTimer;

        spawnPosition = map[Random.Range(0, map.Length)].transform.position;

        //spawnPosition.y += 6f;

        GameObject newPowerUp = Instantiate(rock, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Spawns the first powerup in the middle of the arena
    /// </summary>
    private void FirstPowerUp()
    {
        isFirst = false;

        nextSpawn = powerUpSpawnTimer;

        rng = Random.Range(0, 100);

        if (rng < 50f)
        {
            nextPowerUp = powerups[0];
        }
        else
        {
            nextPowerUp = powerups[1];
        }

        int middle = (int)Mathf.Round((map.Length) / 2);

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

    /// <summary>
    /// Spawns powerups randomly on the arena.
    /// </summary>
    private void SpawnPowerUp()
    {
        nextSpawn = powerUpSpawnTimer;

        rng = Random.Range(0, 100);

        if(rng < 50f)
        {
            nextPowerUp = powerups[0];
        }
        else
        {
            nextPowerUp = powerups[1];
        }

        spawnPosition = map[Random.Range(0, map.Length)].transform.position;

        Debug.Log(map.Length);
        spawnPosition.y += yOffset;

        GameObject newPowerUp = Instantiate(nextPowerUp, spawnPosition, Quaternion.identity, container.transform);

        string tag = DetermineTag(nextPowerUp);

        Transform childTransform = newPowerUp.transform.Find("PickupCollider");
        if (childTransform != null)
        {
            childTransform.gameObject.tag = tag;
        }
    }

    /// <summary>
    /// Determine the tag of the spawned powerup
    /// </summary>
    /// <param name="powerup">Name of the powerup</param>
    /// <returns></returns>
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
