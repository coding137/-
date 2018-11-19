using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	NavMeshAgent nav;
    Transform offset;
    bool isTracking = false;

    void Start()
    {
        offset = player.transform;
      
    }
	
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}
	
	
	void Update ()
	{
		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 )
		{
		nav.SetDestination (player.position);
		}
		else
		{
		    nav.enabled = false;
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isTracking = true;
        }
        else
        {
            isTracking = false;
        }
    }
}