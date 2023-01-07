using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineGen : MonoBehaviour
{
    public Spline splineGO;
    public GameObject splineRef;
    public float distance;
    public GameObject[] rocks;
    public int[] patrolEnemyNo;
    public int d;//Difficulty Level 0 --- easy  ___  1 --- Difficult
    public GameObject[] easyEnemy;
    public GameObject[] hardEnemy;
    public GameObject[] enemies;
    public GameObject pickupRef;
    public int pickupScarcity = 2;
    public PlayerController playerControllerRef;

    private int waitToSpawnPickup=2;
    private int numEnemies;  
    

    // Start is called before the first frame update
    void Start()
    {
        // get difficulty from serialized class and set it the enemies in scene, Pickup frequency and speed of enemies
        PlayerData data = LoadSystem.LoadPlayer();
        
        if (data != null && data.difficulty == 0)
        {
            enemies = easyEnemy;
            pickupScarcity = 2;
            playerControllerRef.difficultyLevel = 0;
        }
        else
        {
            enemies = hardEnemy;
            pickupScarcity = 3;
            playerControllerRef.difficultyLevel = 1;
        }

        

        //Used as a iterator to spawn the enemies from the list provided
        numEnemies = enemies.Length;
        //Debug.Log(enemies);
        // Shuffling the aray for random spawn
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject temp = enemies[i];
            int randomIndex = Random.Range(i, enemies.Length);
            enemies[i] = enemies[randomIndex];
            enemies[randomIndex] = temp;
        }
        RockSpawner();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vb;
        //if (splineGO!=null)
            //Debug.Log(SplineUtility.EvaluatePosition<Spline>(splineGO, distance));

    }

    void RockSpawner()
    {
        //Debug.Log("RockSpawner");
        
        int numEnemies = enemies.Length;

        // Interval at which each enemy has to be spawned with a hint a randomness added to the interval
        int enemySpawnInterval = (int)((splineGO.GetLength()-100) / numEnemies);
        int nextEnemySpawn=+ enemySpawnInterval+Random.Range(-25,25);


        GameObject spawnedEnemy;
        for (int i= 0;i< splineGO.GetLength(); i=i+7)
        {
            
            //Spawning the enemy at the specified interval
            if(Mathf.Abs(i-nextEnemySpawn) <7)
            {
                //Debug.Log("enemyspawner");
                spawnedEnemy = SpawnEnemy();
                
                if (spawnedEnemy != null)
                {
                    //To give extra space or reduce space for the enemy
                    if (spawnedEnemy.CompareTag("Patrol"))
                        i = i + 3;
                    else if (spawnedEnemy.CompareTag("School"))
                        i = i - 2;

                    Vector3 enemySpawnLoc = DistToWorldLoc(i);
                    //Find the rotation for the enemy to look at while spawning
                    Quaternion enemyRotation = LookAtRotation(enemySpawnLoc, DistToWorldLoc(i + 3));
                    spawnedEnemy.transform.position = enemySpawnLoc;
                    spawnedEnemy.transform.rotation = enemyRotation;
                    i = i + 2;
                    //Assign next spawn interval
                    nextEnemySpawn =nextEnemySpawn + enemySpawnInterval + Random.Range(-25, 25);
                }
            }
            

            //Rock Spawn

            Vector3 posToSpawn = DistToWorldLoc(i);
            //Generate Two random number and multiply both to get a new random number. ranInt number is be used to control the range in editor.
            //Also these give three random numbers and can be used in Assigning the xyz randomly
            float rndFloat = Random.Range(0.0f, 1.0f);
            int rndInt = Random.Range(1, 4);
            float rndPos = rndFloat * rndInt;
            //Randomly draw the rock type to spawn
            int rndSpawner = Random.Range(0, rocks.Length);
            //Generate Random number for the scale of the rocks. Range is specified taking the acceptable scaling in that direction.
            float rndScaleY = Random.Range(0.9f, 1.15f);
            float rndScaleX = Random.Range(0.75f, 2f);
            float rndScaleZ = Random.Range(0.75f, 1.8f);

            //Spawn the rock and assign the transform
            GameObject spawned=Instantiate(rocks[rndSpawner]);
            Debug.Log("spawned");
            spawned.transform.position = new Vector3(posToSpawn.x + rndPos, posToSpawn.y + rndPos, posToSpawn.z + rndPos);
            spawned.transform.localScale = new Vector3(spawned.transform.localScale.x* rndScaleX, spawned.transform.localScale.y*rndScaleY, spawned.transform.localScale.z*rndScaleZ);
            spawned.transform.Rotate(new Vector3(0, Random.Range(0, 180), 0));


            //Spawn the Pickups. The pickup pos has been predefined in the three prefab rock. Get their positinn and assign to the instance
            if(waitToSpawnPickup==0)
            {
                foreach(Transform t in spawned.transform)
                {
                    if(t.CompareTag("PickupPos"))
                    {
                        GameObject pickupObj = Instantiate(pickupRef);
                        pickupObj.transform.position = t.position;
                    }
                }
                //waitToSpawnPickup defines the scarciy at which each pickup has to be created
                waitToSpawnPickup = pickupScarcity + Random.Range(0, (int)pickupScarcity/2);
            }
            waitToSpawnPickup--;
        }
        Debug.Log("exit for loop");
    }

    //Find the world location at the given spline distance
    public Vector3 DistToWorldLoc(int distance)
    {
        Vector3 posToSpawn = SplineUtility.EvaluatePosition<Spline>(splineGO, distance / splineGO.GetLength());
        posToSpawn = transform.TransformPoint(posToSpawn);
        return posToSpawn;
    }

    //Find the look at rotation for the origin to the destination
    public Quaternion LookAtRotation(Vector3 Origin, Vector3 LookAt)
    {
        Vector3 forwardDirection = (LookAt - Origin).normalized;
        Quaternion rotation = Quaternion.LookRotation(forwardDirection, Vector3.up);
        return rotation;
    }

    //Spawn the enemy as per the given difficulty
    public GameObject SpawnEnemy()
    {
        GameObject spawned=null;
        if (numEnemies>0)
            spawned = Instantiate(enemies[numEnemies-1]);
        Debug.Log("before"+numEnemies);
        numEnemies--;
        Debug.Log("After"+numEnemies);
        //enemies.RemoveAt(spawnRnd);
        return spawned;
    }
}
