using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Playerstatus: MonoBehaviour {

    // Use this for initialization

    public float playerRadius = 1f;
    public float max_hp = 100f;
    public float current_hp;
    public float damage = 100f;
    PlayerController pc;
    Fire_Snow fs;
    public AudioClip playerDeathclip;
    int count = 0;

    AudioSource audios;
    void Awake()
    {
        audios = GetComponent<AudioSource>();
    }

    void Start () {
        pc = GetComponent<PlayerController>();
        fs = GetComponent<Fire_Snow>();
     
        current_hp = max_hp;

	}
	
	// Update is called once per frame
	void Update () {

        if (current_hp < 0 &&count>=0)
        {
            audios.clip = playerDeathclip;
            count--;
           audios.Play();
            pc.enabled = false;
            fs.enabled = false;
            StopAllCoroutines();
        }
	}
}
