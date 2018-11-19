using UnityEngine;
using System.Collections;

public class FireSnow : MonoBehaviour {
    Transform tran;
    private float ticktok=0;
    private float ball_aliveTime= 3f;
    private float dec_period = 0;
    public int damage = 40;
    public float speed = 1000.0f;

    ParticleSystem gunParticles;

    void Awake()
    {
    //    gunParticles = GetComponent<ParticleSystem>();

    }
    // Use this for initialization
    void Start()
    {
        dec_period = Time.time;

        tran = GetComponent<Transform>();

        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
       // tran.localScale{transform, transform. };
       // gunParticles.Stop();
        //gunParticles.Play();
        ticktok += Time.deltaTime;

        Decreasing_size();



        
    }

    void Decreasing_size()
    {


        if (Time.time > dec_period)
        {
            dec_period = Time.time + 0.1f;
            Debug.Log("Decreasing snow");
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if(transform.localScale.x<=0)
            {
                Destroy(tran.gameObject);
            }
        }
        }
}
