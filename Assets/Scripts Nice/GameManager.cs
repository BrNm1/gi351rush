using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] obstacles; // Array สำหรับเก็บ Prefab ของ Obstacle
    public GameObject[] energyPrefabs;
    public float spawnInterval = 1.0f; // ความถี่ในการสร้าง Obstacle
    public float spawnRangeX = 10.0f; // ขอบเขตการเกิด Obstacle ในแนวนอน
    public float minSpawnDistance = 5.0f; // ระยะห่างขั้นต่ำระหว่าง Obstacle
    public float maxSpawnDistance = 20.0f;
    public LayerMask groundLayer; // LayerMask สำหรับ Ground
    public float despawnDistance = 20.0f; // ระยะห่างที่ Obstacle จะถูกทำลายหลังจาก Player เลยไปแล้ว
    public float obstacleHeightOffset = -2.27f;

    public Transform obstacleListTransform;
    public Transform energyListTransform;
    
    private Player player;
    private float lastSpawnX;
    private Transform playerTransform;
    private List<GameObject> activeObstacles = new List<GameObject>(); // List สำหรับเก็บ Obstacle ที่ถูกสร้าง
    public List<GameObject> activeEnergy = new List<GameObject>();
    
    [Range(0f, 1f)] public float obstacleSpawnChance = 0.7f;

    void Start()
    {
        lastSpawnX = transform.position.x;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // ค้นหา Player ใน Scene
        player = playerTransform.GetComponent<Player>();
        
        // ตรวจสอบว่า ObstacleList ถูกเชื่อมโยง
        if (obstacleListTransform == null || energyListTransform == null)
        {
            Debug.LogError("ObstacleListTransform not assigned in the Inspector.");
            return;
        }
        StartCoroutine(SpawnObstacles());
    }
    
    void Update()
    {
        // ทำลาย Obstacle ที่ด้านซ้ายของ Player
        DestroyOldObstacles();
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // ตรวจสอบตำแหน่งการเกิดใหม่
            float spawnX = lastSpawnX + Random.Range(minSpawnDistance, maxSpawnDistance + 5.0f);
            lastSpawnX = spawnX;

            // หา Y ตำแหน่งของ Ground ด้วย Raycast
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(spawnX, 10.0f), Vector2.down, Mathf.Infinity, groundLayer);
            if (hit.collider != null)
            {
                float spawnY = hit.point.y; // รับ Y ตำแหน่งจาก Raycast
                
                if (Random.Range(0f, 1f) < obstacleSpawnChance)
                {
                    // เลือก Obstacle แบบสุ่ม
                    GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
                    if (obstacle == obstacles[2])
                    {
                        obstacleHeightOffset = -0.5f;
                        spawnY = obstacleHeightOffset;
                    }
                    else
                    {
                        obstacleHeightOffset = -2.27f;
                    }
                    spawnY = obstacleHeightOffset;

                    // สร้าง Obstacle ที่ตำแหน่งใหม่
                    GameObject obstacleInstance = Instantiate(obstacle, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
                
                    // ตั้งค่า Obstacle
                    Obstacle obstacleScript = obstacleInstance.GetComponent<Obstacle>();
                    if (obstacleScript != null)
                    {
                        obstacleScript.Initialize(playerTransform, despawnDistance);
                        obstacleScript.player = player;
                    
                        // เชื่อมโยง Energybar ที่มีอยู่ใน Scene
                        Energybar energyBar = Energybar.Instance; // ใช้ Singleton Instance
                        if (energyBar != null)
                        {
                            obstacleScript.SetEnergyBar(energyBar);
                        }
                        else
                        {
                            //Debug.LogWarning("Energybar instance not found in the scene.");
                        }
                    }
                    // ทำให้ Obstacle เป็นลูกของ ObstacleList
                    obstacleInstance.transform.SetParent(obstacleListTransform);
                
                    // เพิ่ม Obstacle ที่สร้างไปยัง List
                    activeObstacles.Add(obstacleInstance);
                }
                else
                {
                    float obstacleHeightOffset = -1f;
                    spawnY = obstacleHeightOffset;
                    
                    GameObject energy = energyPrefabs[Random.Range(0, energyPrefabs.Length)];
                    GameObject energyInstance = Instantiate(energy, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
                    energyInstance.transform.SetParent(energyListTransform);
                    activeEnergy.Add(energyInstance);
                }
            }
            else
            {
                //Debug.LogWarning("Ground not found at position: " + spawnX);
            }
        }
    }
    
    void DestroyOldObstacles()
    {
        float playerX = playerTransform.position.x;
        // ทำลาย Obstacle ที่อยู่ด้านซ้ายของ Player
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = activeObstacles[i];
            if (obstacle != null)
            {
                float obstacleX = obstacle.transform.position.x;
                // เช็คว่า Obstacle อยู่ด้านซ้ายของ Player และห่างเกินระยะที่กำหนด
                if (obstacleX < playerX - despawnDistance)
                {
                    Destroy(obstacle);
                    activeObstacles.RemoveAt(i); // เอา Obstacle ออกจาก List
                }
            }
        }

        for (int i = activeEnergy.Count - 1; i >= 0; i--)
        {
            GameObject energy = activeEnergy[i];
            if (energy != null)
            {
                float energyX = energy.transform.position.x;
                if (energyX < playerX - despawnDistance)
                {
                    Destroy(energy);
                    activeEnergy.RemoveAt(i);
                }
            }
        }
    }
    
    public void UpdateSpawnInterval(float interval)
    {
        spawnInterval = Mathf.Max(0.1f, interval); // ปรับช่วงเวลาให้ไม่ต่ำกว่า 0.1 วินาที
        //Debug.Log("Updated spawnInterval to: " + spawnInterval);
    }
}
