using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MonsterStatus : MonoBehaviour {

    public float monsterrad = 3f;
    public float max_hp = 200f;
    public float current_hp = 200f;
    public float damage = 10f;
    public float Atk_delay = 2f;
    private Transform mon_me;
    public Slider hpbar;
    private Playerstatus status;
    private MonsterStatus ms;
    public float spd=20f;
    public GameObject ObjectType; 
        public GameObject HeadUpPosition;
     private AI ai;
    public lvmgr mgr;

    // Use this for initialization
    void Start () {
        ai = GetComponent<AI>();
        mon_me = GetComponent<Transform>();


    }
	
	// Update is called once per frame
	void Update () {
        //hpbar.value = status.current_hp;
        // hpbar.transform.position = HeadUpPosition.transform.position;
        if (current_hp < 0)
        {

            Destroy(mon_me.gameObject);
            mgr.killcivil++;
            ScoreManager.score += 50; 
        }

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Snow")
        {
            Destroy(coll.gameObject);
            current_hp -= 50;
            Debug.Log("dddddddddddddddddddd");
         //  mon_me.position -= coll.gameObject.transform.position/10;
       // StartCoroutine("TransStateToMove", 0.5f);
        }

    }
}
