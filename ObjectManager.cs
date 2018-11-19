using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{/*
    public enum ObjectType
    {
        Enemy,
        Player,
    }

    private static ObjectManager instance_ = null;
    public static ObjectManager Instance
    {
        get
        {
            return instance_;
        }

        private set { }
    }



    void Awake()
    {
        instance_ = this;

        CreateObjectByTest();


    }

Dictionary<ObjectType, List<GameObject>> objectLists = new Dictionary<ObjectType, List<GameObject>>();

    /// <summary>
    /// 타입별로 그룹화해서, 오브젝트를 저장한다.
    /// </summary>
    /// <param name="objType"></param>
    /// <param name="obj"></param>
    public void AddObject(ObjectType objType, GameObject obj)
    {
        if (!objectLists.ContainsKey(objType))
        {
            objectLists[objType] = new List<GameObject>();
        }

        if (!objectLists[objType].Contains(obj))
        {
            objectLists[objType].Add(obj);
        }
    }

    /// <summary>
    /// 특정 타입에서, 특정 오브젝트를 삭제한다.
    /// </summary>
    /// <param name="objType"></param>
    /// <param name="obj"></param>
    public void RemoveObject(ObjectType objType, GameObject obj)
    {
        if (objectLists[objType].Contains(obj))
        {
            objectLists[objType].Remove(obj);
        }
    }


    public GameObject CreateObject(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }
        return go;
    }


 public void CreateObjectByTest()
    {
        GameObject playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        GameObject monster1Prefab = Resources.Load("Prefabs/Monster_1") as GameObject;

        GameObject player = CreateObject(gameObject, playerPrefab);
        GameObject slime1 = CreateObject(gameObject, monster1Prefab);
        GameObject slime2 = CreateObject(gameObject, monster1Prefab);




        AddObject(ObjectType.Player, player);
        AddObject(ObjectType.Enemy, slime1);
        AddObject(ObjectType.Enemy, slime2);
    }
    public ObjectType GetHostileType(ObjectType myObjectType)
    {
        if (myObjectType == ObjectType.Enemy)
            return ObjectType.Player;

        return ObjectType.Enemy;
    }



    public GameObject FindFirstEnemy(GameObject actor, float distance)
    {
        //나의 속성(플레이어인지 적인지)을 얻어오기 위해 캐릭터 어튜리뷰트를 얻어온다.
        //또한 속성은 Player와 Monster_1 프리팹에 설정되 있어야 한다.
        MonsterStatus actorAttribute = actor.GetComponent<MonsterStatus>();
        if (actorAttribute == null)
            return null;

        //액터 입장에서 적대적인 객체타입을 얻어온다.
        ObjectType hostileType = GetHostileType);


        //해당 타입의 오브젝트가 추가되기 전에, 찾게 되는 경우는 못 찾은것으로 처리한다.
        if (!objectLists.ContainsKey(hostileType))
            return null;

        //적대적인 타입을 가진 오브젝트 리스트에서 정해준 거리안에 있는 1번째 대상을 반환한다.         
        //적대적인 대상을 모두 반환해서, 광역 스킬을 사용한다던지 할 수도 있지만, 일단 첫번째 대상만 반환하도록 하자
        foreach (GameObject obj in objectLists[hostileType])
        {
            if (Vector3.Distance(actor.transform.position, obj.transform.position) <= distance)
            {
                Debug.Log("Find Enemy : " + obj.name);
                return obj;
            }
        }

        return null;
    }


    void Start()
    {

    }

    void Update()
    {

    }*/
}

