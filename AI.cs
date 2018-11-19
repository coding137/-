using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    //public GameObject Target { get; set; }  //무언가를 할 대상
    public Vector3 TargetPosition { get; set; }  //대상의 위치

    int hidingpointindex = 0;
    
    public GameObject[] HPoints;
    public float sleepinghp = 0;
    public GameObject Target;
    private float idlehp;
    public Transform monster;
    public float spd = 10f;
    public MonsterStatus mon;
  
    public int count = 0;
    Playerstatus plyerrad;
    TestCode td;
    public GameObject HTarget;
    private AI ai;
    public enum State
    {
            Move,
            Attack,
            Damaged,
            Hiding,
            Idle,
            Died,
            Sleeping
        

    };
    private pointsatt hidingpoints;
    private State current_state = State.Move;
    
    
    
    
    // Use this for initialization
    void Start () {
        ai = GetComponent<AI>();
        hidingpoints = new pointsatt();


    
    }

    // Update is called once per frame
    void Update () {

        plyerrad = Target.GetComponent<Playerstatus>();
        mon = transform.GetComponent<MonsterStatus>();
        td = GetComponent<TestCode>();
        
        float distance = Vector3.Distance(Target.transform.position, transform.position);// target과 나의 distance 를  잡아준다 .

        //  Debug.Log(" x:       "+td.objEndCube.transform.position.x + "z :"+td.objEndCube.transform.position.z);



        if (count == 0 && mon.current_hp <= 50)
        {
            Debug.Log("countup");
            count++;

            SetState(State.Hiding);
        }

        switch (current_state)
        {
            case State.Idle: Idle(); break;
            case State.Move: Move(); break;
            case State.Attack: Attack(); break;
            case State.Died: DIED(); break;
            case State.Hiding: Hiding(); break;
            case State.Sleeping: Sleeping(); break;
        }

        

     

        // 몬스터 죽었을때 컨트롤 파츠 
        if (plyerrad.current_hp < 0)
        {


            SetState(State.Died);
        }
    }
    void Sleeping()
    {
        if (mon.current_hp<sleepinghp+5&&sleepinghp+5<mon.current_hp)
        {
            return;

        }
        else
        {
            StartCoroutine("TransfertoIdle", 0.5f);
        }
    }

    void Move()
    {

        plyerrad = Target.GetComponent<Playerstatus>();
        mon = transform.GetComponent<MonsterStatus>();
        td = GetComponent<TestCode>();
        Vector3 dir = Target.transform.position - transform.position;
        dir.Normalize();

       // Debug.Log("move");



        float distance = Vector3.Distance(Target.transform.position, transform.position);// target과 나의 distance 를  잡아준다 .

        if (distance > mon.monsterrad + plyerrad.playerRadius)
        {
            transform.LookAt(Target.transform.position);


            SetState(State.Move);
            td.enabled = true;
            td.objEndCube = GameObject.FindGameObjectWithTag("End");
            td.objStartCube = monster;
            // StopAllCoroutines();
        }
        else
        {
            td.enabled = false;
            Debug.Log("transfer");

            StartCoroutine("TransfertoIdle", 0.5f);
        }
       

        // 몬스터 죽었을때 컨트롤 파츠 
        if (plyerrad.current_hp < 0)
        {


            SetState(State.Died);
        }

    }

   
    void Attack()
    {

        Debug.Log("attk");

        MonsterStatus mon = gameObject.GetComponent<MonsterStatus>();

        plyerrad.current_hp -= mon.damage;

        StartCoroutine("TransfertoIdle", mon.Atk_delay/10);// monster의 기본 대미지 시간과 몬스터의 대미지의 크기에 따른 딜레이
    }

 
    void Hiding()
    {

    
        mon = transform.GetComponent<MonsterStatus>();
        td = GetComponent<TestCode>();

        td.spd = 10f;
      //  Debug.Log("hiding");



        float distance = Vector3.Distance(td.objEndCube.transform.position, transform.position);// target과 나의 distance 를  잡아준다 .

        if (distance > mon.monsterrad + 3f)
        {
            transform.LookAt(td.objEndCube.transform.position);


            SetState(State.Hiding);
            td.enabled = true;
            td.objEndCube = HPoints[hidingpointindex];
            td.objStartCube = monster;
            // StopAllCoroutines();
        }
        else
        {
            td.enabled = false;
            Debug.Log("transfer");

            StartCoroutine("TransfertoSleeping", 0.5f);
        }

    }

    void Idle()
    {
          
        if (current_state==State.Died)
        {
            return;

        }

     
        Debug.Log("NOwIDle");
        bool isTargetAlive = false;
        MonsterStatus mon = gameObject.GetComponent<MonsterStatus>();

        if (Target == null)
        {
            plyerrad = Target.GetComponent<Playerstatus>();
            if (plyerrad.current_hp >= 0)
            {
                isTargetAlive = true;
            }
        }

        if(!isTargetAlive)
        {
            float distance = Vector3.Distance(Target.transform.position, transform.position);// target과 나의 distance 를  잡아준다 .

            if (distance > 5.0f)
            {


                transform.LookAt(Target.transform.position);
                TargetPosition = Target.transform.position;
                SetState(State.Move);

            }
            else
            {
                Debug.Log("attakc!!");

                td.enabled = false;

                 SetState(State.Attack);
     
            }



            // 몬스터 죽었을때 컨트롤 파츠 
            if (plyerrad.current_hp < 0)
            {


                SetState(State.Died);
            }
        }


    }


    void StartHiding()
    {
        Debug.Log("Start hididnfg");
        hidingpointindex = Random.Range(0, 5);

        td.objEndCube = HPoints[hidingpointindex];
       mon.current_hp = 200;
        Hiding();
    }
    void SetState(State newState)
    {

        if (newState == current_state)
            return;

        if (current_state == State.Died)
            return;


            current_state = newState;

            switch (newState)
            {
                case State.Attack:
                    {
                        
                    }break;
                case State.Move: {}break;

                case State.Hiding: {
                    StartHiding();
                } break;

                case State.Idle: { } break;
                case State.Died: { } break;

            }
   

    }



    void DIED()

    {
        td.enabled = false;
        //  AI ai = GetComponent<AI>().enabled = false;
        ai.enabled = false;
    }




    IEnumerator TransfertoMove(float delayT)
    {

        yield return new WaitForSeconds(delayT);// thread;
        SetState(State.Move);
    }

    IEnumerator TransfertoSleeping(float t)
    {
        yield return new WaitForSeconds(2.0f);
        sleepinghp = mon.current_hp;
        SetState(State.Sleeping);

    }
    
    IEnumerator TransfertoIdle(float delayT)
    {
        yield return new WaitForSeconds(1.0f);
        SetState(State.Idle);

    }

}
