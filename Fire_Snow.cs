using UnityEngine;
using System.Collections;

public class Fire_Snow : MonoBehaviour {


    public GameObject bullet;
    public Transform firepos;

    public AudioClip gunfiresound;

    private AudioSource source;
    private float delay = 1f;
    private float ticktok;

    void Start()
    {
        source = GetComponent<AudioSource>();

    }


    void Update()
    {
        ticktok += Time.deltaTime;
        if (ticktok>=delay) {
            if (Input.GetMouseButtonDown(0))
            {
                source.clip = gunfiresound;
                Fire();

            }
        }
    }

    void Fire()
    {
       //source.Play( );
        Shoot_snow();

    }
    void Shoot_snow()
    {
        Instantiate(bullet, firepos.position, firepos.rotation);
    }
}
