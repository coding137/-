using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

    //For player

    public Slider hpbar;
    private Playerstatus status;
    public GameObject HeadUpPosition;
   // private AI AI;


    // Use this for initialization
    void Start () {
        status = GetComponent<Playerstatus>(); 

	}
	
	// Update is called once per frame
	void Update () {

        hpbar.value = status.current_hp;
     //   hpbar.transform.position = HeadUpPosition.transform.position;     
	}
}
