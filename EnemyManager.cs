using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
 // public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 0.01f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int num_of_enemy = 10;
    public int current_numof_enemy = 0;
    private bool exist_player  = true;
    public GameObject player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("End");
    }

    void Update()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.



        if(true)
      //  if (exist_player==true)
        {
           InvokeRepeating("Spawn", spawnTime, spawnTime);


        }
            
         
        }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("getplayer\n");
            exist_player = true;
        }
    }

    void Spawn()
    {
        // payer Health가 없는지 있는지 체크 
       // if(true)
      //  if (playerHealth.currentHealth <= 0f)
       // {
            // ... exit the function.
         
       // }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);


        //  enemy를 생성한다. 첫번째 파라미터에는 enemy의 prefeb 두번째는 생성위치 ..

        if (current_numof_enemy < num_of_enemy&& (transform.position.x-player.transform.position.x)<4&&(transform.position.z-player.transform.position.z)<4)
        {

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            current_numof_enemy++;

   
            StopAllCoroutines();

        }
    }


}