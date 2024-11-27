using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> rooms = new List<GameObject>();
    public BoundsInt gateBounds;
    public Tilemap gateTiles;

    public GameObject enemyBulletsFolder;

    private int currentRoomIndex = 0;
    private GameObject currentRoom;
    private RoomManager roomManager;

    [SerializeField]
    private List<TileBase> openGateTiles;
    [SerializeField]
    private List<TileBase> closedGateTiles;

    public List<GameObject> aliveEnemies;

    private int currentWave = 0;

    private float timeSinceLastSpawn = 0f;
    private float spawnSpeed = 1f;
    private int spawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure waves list is ordered properly
        rooms.OrderByDescending(obj => obj.gameObject.name);

        currentRoom = rooms[0];
        roomManager = currentRoom.GetComponent<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roomManager.waves <= 0 && currentRoomIndex != rooms.Count - 1)
        {
            currentRoom.transform.Find("Spawner").gameObject.layer = 15;
            gateTiles.SetTilesBlock(gateBounds, openGateTiles.ToArray());
            currentWave = 0;
        } else if (roomManager.waves > 0 && spawned < roomManager.enemyCount[currentWave]) 
        {
            Vector3 spawnPoint = currentRoom.transform.Find("Spawner").transform.position + new Vector3(0, -2, 0);

            if (timeSinceLastSpawn + spawnSpeed < Time.time) {
                GameObject enemy = Instantiate(roomManager.enemies[Random.Range(0, roomManager.enemies.Count)]);
                enemy.transform.position = spawnPoint;
                enemy.GetComponent<EnemyHealth>().waveManagerObject = gameObject;
                if (enemy.GetComponent<EnemyShoot>() != null) { enemy.GetComponent<EnemyShoot>().enemyBulletsFolder = enemyBulletsFolder; }
                aliveEnemies.Add(enemy);

                timeSinceLastSpawn = Time.time;
                spawned++;
            }
        } else if (spawned == roomManager.enemyCount[currentWave] && aliveEnemies.Count <= 0 && currentRoomIndex != rooms.Count - 1)
        {
            currentWave++;
            roomManager.waves--;
            spawned = 0;
        }
    }

    public void changeRoom()
    {
        currentRoomIndex++;
        currentRoom = rooms[currentRoomIndex];
        roomManager = currentRoom.GetComponent<RoomManager>();
        rooms[currentRoomIndex - 1].transform.Find("Spawner").gameObject.layer = 14;
        gateTiles.SetTilesBlock(gateBounds, closedGateTiles.ToArray());
        gateBounds = new BoundsInt(new Vector3Int(gateBounds.position.x, gateBounds.position.y + 11, gateBounds.z), gateBounds.size);
        currentWave = 0;
    }
}
