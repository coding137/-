using UnityEngine;
using System.Collections;

public class CharacterStateMachine : MonoBehaviour
{
    public GameObject Target { get; set; }  //무언가를 할 대상
    public Vector3 TargetPosition { get; set; }  //대상의 위치

    public enum State
    {
        INITIALIZE,
        IDLE,
        MOVE,
        ATTACK,
        DAMAGED,
        Dead,
    };

    private State currentState_ = State.INITIALIZE;
    private float nextAttackCooltime_ = 0.0f;


    void Awake()
    {
        Target = null;
        SetState(State.IDLE);
    }

    /// <summary>
    /// 상태를 변경한다. 같은 상태로 바뀌지는 않는다.
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(State newState)
    {
        if (newState == currentState_)
            return;

        if (currentState_ == State.Dead)
            return;

        currentState_ = newState;

        switch (newState)
        {
            case State.IDLE: { Enter_IdleState(); } break;
            case State.MOVE: { Enter_MoveState(); } break;
            case State.ATTACK: { Enter_AttackState(); } break;
            case State.DAMAGED: { Enter_DamagedState(); } break;
            case State.Dead: { Enter_DeadState(); } break;
        }
    }

    void Update()
    {
        switch (currentState_)
        {
            case State.IDLE: Update_IdleState(); break;
            case State.MOVE: Update_MoveState(); break;
            case State.ATTACK: Update_AttackState(); break;
            case State.DAMAGED: Update_DamagedState(); break;
            case State.Dead: Update_DeadState(); break;
        }
    }

    /// <summary>
    /// 내가 사망했는지 체크하고, 사망한 경우는 코루틴으로 지연되는 처리를 모두 중지시키고, 사망 상태로만 전이시킨다.
    /// </summary>
    /// <param name="myAttribute"></param>
    /// <returns></returns>
    bool ProcessDeadState(MonsterStatus myAttribute)
    {
        if ( myAttribute.current_hp<= 0)
        {
            StopAllCoroutines();
            SetState(State.Dead);
            return true;
        }
        return false;
    }
    //----------------------------------------------------------------------------
    /// <summary>
    /// 타겟에게 나의 능력치를 기반하여 HP를 감소시킨다
    /// </summary>
    void Enter_AttackState()
    {
        MonsterStatus myAttribute = gameObject.GetComponent<MonsterStatus>();
        if (ProcessDeadState(myAttribute))
            return;

        //애니메이션 실행

        Playerstatus targetAttribute = Target.GetComponent<Playerstatus>();
        targetAttribute.current_hp -= myAttribute.damage;

        Debug.Log("Attack : " + gameObject.name + "->" + Target.name);
        Debug.Log("Hp : " + targetAttribute.current_hp.ToString());

        CharacterStateMachine targetStateMachine = Target.GetComponent<CharacterStateMachine>();
        targetStateMachine.SetState(State.DAMAGED);

        //대상이 죽은 경우는 타겟 해제
        if (targetAttribute.current_hp <= 0)
        {
            Target = null;
        }

        StartCoroutine("TransStateToIdle", myAttribute.Atk_delay);
    }

    void Update_AttackState()
    {
    }
    //----------------------------------------------------------------------------
    void Enter_MoveState()
    {
        MonsterStatus myAttribute = gameObject.GetComponent<MonsterStatus>();
        if (ProcessDeadState(myAttribute))
            return;
//걷는 애니메이션 실행
//
        float distance = Vector3.Distance(TargetPosition, transform.position);
        float needSeconds = distance / myAttribute.spd;

        StartCoroutine("TransStateToIdle", needSeconds);
   }

    void Update_MoveState()
    {
        MonsterStatus myAttribute = gameObject.GetComponent<MonsterStatus>();
        if (ProcessDeadState(myAttribute))
            return;

        Vector3 dir = TargetPosition - transform.position;
        dir.Normalize();

        transform.LookAt(TargetPosition);
        transform.position += (dir * myAttribute.spd * Time.deltaTime);
    }
    //----------------------------------------------------------------------------
    void Enter_DamagedState()
    {
        Debug.Log("DamagedState : " + gameObject.name);

        //죽는 애니메이션 실행

        MonsterStatus myAttribute = gameObject.GetComponent<MonsterStatus>();
        if (ProcessDeadState(myAttribute))
            return;


        StartCoroutine("TransStateToIdle", 0.5f);
    }

    void Update_DamagedState()
    {

    }
    //----------------------------------------------------------------------------
    void Enter_IdleState()
    {
        MonsterStatus myAttribute = gameObject.GetComponent<MonsterStatus>();
        if (ProcessDeadState(myAttribute))
            return;

    //idle 애니메이션 실행

        Update_IdleState();
    }

    void Update_IdleState()
    {
        MonsterStatus myAttribute = gameObject.GetComponent<MonsterStatus>();
        if (ProcessDeadState(myAttribute))
            return;

        //타겟이 계속 바뀌면 안됨으로, 타겟이 유효한 경우는 유지되도록
        bool isFindTarget = false;
        if (Target != null)
        {
            Playerstatus targetAttribute = Target.GetComponent<Playerstatus>();
            if (targetAttribute.current_hp > 0)
                isFindTarget = true;
        }

        if (!isFindTarget)
            //Target = FindEnemy(searchDistanceMeter: 2.0f);


        if (Target != null)
        {
            //적을 찾았으면 대상으로 지정하고 이동            
            Playerstatus targetAttribute = Target.GetComponent<Playerstatus>();

            Vector3 dir = Target.transform.position - transform.position;
            dir.Normalize();
            float distance = Vector3.Distance(Target.transform.position, transform.position);


            if (distance > targetAttribute.playerRadius + myAttribute.monsterrad)
            {
                transform.LookAt(Target.transform.position);

                TargetPosition = Target.transform.position;
                SetState(State.MOVE);
            }
            else
            {
                if (nextAttackCooltime_ == 0.0f || nextAttackCooltime_ < Time.time)
                {
                    nextAttackCooltime_ = Time.time + myAttribute.Atk_delay;
                    SetState(State.ATTACK);
                }
            }
        }
        else
        {
            //적을 못 찾았으면 랜덤한 위치로 이동한다.            
           // TargetPosition = FindRandomTargetPosition(minRangeMeter: 4, maxRangeMeter: 8);
            SetState(State.MOVE);
        }
    }
    //----------------------------------------------------------------------------
    void Enter_DeadState()
    {
       //
        //애니메이션 실행
    }

    void Update_DeadState()
    {

    }
    //----------------------------------------------------------------------------


    /// <summary>
    /// 적이 있는지 찾는다. 찾은 경우는 찾은 적을 리턴한다.
    /// 못 찾은 경우는 null반환
    /// </summary>
    /// <returns></returns>    
  /*  GameObject FindEnemy(float searchDistanceMeter)
    {
       // float Distance = gameObject
      //  return ObjectManager.Instance.FindFirstEnemy(gameObject, searchDistanceMeter);
    }
    */
    /// <summary>
    /// waitTime이 지난후에, Move상태로 전이한다.
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator TransStateToIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SetState(State.IDLE);
    }

    void Start()
    { }
}
