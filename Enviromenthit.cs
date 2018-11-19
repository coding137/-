using UnityEngine;
using System.Collections;

public class Enviromenthit : MonoBehaviour {

    public GameObject sparkEffect;
    void OnCollisionEnter(Collision get_hit)
    {

        if (get_hit.collider.tag == "Snow")
        {
           // Instantiate(sparkEffect, get_hit.transform.position, Quaternion.identity);

            Destroy(get_hit.gameObject);



        }
    }
}
