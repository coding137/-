using UnityEngine;
using System.Collections;

public class Fire_Points : MonoBehaviour {
    public Color _color = Color.blue;
    public float _rad = 0.1f;
    public AudioClip gunfiresound;
    private AudioSource source;
    private float delay = 0.2f;
    private float ticktok;
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;
    LineRenderer gunLine;
    private float gettime;
   ParticleSystem gunParticles;
    void Awake()
    {
             gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticles = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnDrawGimos()
    {

 
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _rad);

    }
    void Update()
    {

        ticktok += Time.deltaTime;
      
            if (Input.GetMouseButtonDown(0))
            {
      
            gettime = ticktok;

          //  Debug.Log(gunLight.enabled);
                // Enable the line renderer and set it's first position to be the end of the gun.

               source.Play();

            }


            if(ticktok >= delay+gettime)
        {
            //gunLight.enabled = false;
           // Debug.Log(gunLight.enabled);
            // Enable the line renderer and set it's first position to be the end of the gun.

        }
       
        

    }
}
