using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Colony : MonoBehaviour
{

    public Enums.Team team;

    public Transform basePos;
    public List<Transform> targetPos = new List<Transform>();

    public static List<GameObject> AllPrefabs = new List<GameObject>();

    public GameObject obj;

    #region Все префабы, используемые в классе

    public GameObject PrefabQueen;

    public GameObject PrefabWarrior;
    public GameObject PrefabElderWarrior;
    public GameObject PrefabLegendWarrior;
    public GameObject PrefabLegendCareWarrior;
    public GameObject PrefabUpperLegendWarrior;

    public GameObject PrefabWorker;
    public GameObject PrefabElderWorker;
    public GameObject PrefabEliteWorker;
    public GameObject PrefabProLikedWorker;
    public GameObject PrefabProWorker;

    public GameObject PrefabTrickLegged;
    public GameObject PrefabCricket;
    public GameObject PrefabAphids;

    #endregion

    public Dictionary<Enums.TypeResourse, int> inventory = new Dictionary<Enums.TypeResourse, int>() {

        {  Enums.TypeResourse.Twigs, 0 },
        {  Enums.TypeResourse.Leafs, 0 },
        {  Enums.TypeResourse.Rocks, 0 },
        {  Enums.TypeResourse.Drops, 0 }

    };

    public void Import(Enums.TypeResourse type, int count)   
    {

        this.inventory[type] += count;

    }

    public void InitTargetsAndBase()
    {

        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {

            if (gameObj.name.Contains("Target"))
                targetPos.Add(gameObj.transform);

        }

        basePos = transform;

    }

    public void LoadResources()
    {

        PrefabQueen = Resources.Load("Queen", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabQueen);

        InstantiateQueen(false);

        PrefabWarrior = Resources.Load("Warriors/Warrior", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabWarrior);

        PrefabElderWarrior = Resources.Load("Warriors/ElderWarrior", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabElderWarrior);

        PrefabLegendWarrior = Resources.Load("Warriors/LegendWarrior", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabLegendWarrior);

        PrefabLegendCareWarrior = Resources.Load("Warriors/LegendCareWarrior", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabLegendCareWarrior);

        PrefabUpperLegendWarrior = Resources.Load("Warriors/UpperLegendWarrior", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabUpperLegendWarrior);

        InstantiateWarrior("Warrior", 0, 0, 0, 0, true);
        InstantiateWarrior("ElderWarrior", 0, 0, 0, 0, true);
        InstantiateWarrior("LegendWarrior", 0, 0, 0, 0, true);
        InstantiateWarrior("LegendCareWarrior", 0, 0, 0, 0, true);
        InstantiateWarrior("UpperLegendWarrior", 0, 0, 0, 0, true);

        PrefabWorker = Resources.Load("Workers/Worker", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabWorker);

        PrefabElderWorker = Resources.Load("Workers/ElderWorker", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabElderWorker);

        PrefabEliteWorker = Resources.Load("Workers/EliteWorker", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabEliteWorker);

        PrefabProLikedWorker = Resources.Load("Workers/ProLikedWorker", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabProLikedWorker);

        PrefabProWorker = Resources.Load("Workers/ProWorker", typeof(GameObject)) as GameObject;
        AllPrefabs.Add(PrefabProWorker);

        InstantiateWorker("Worker", 0, 0, true);
        InstantiateWorker("ElderWorker", 0, 0, true);
        InstantiateWorker("EliteWorker", 0, 0, true);
        InstantiateWorker("ProLikedWorker", 0, 0, true);
        InstantiateWorker("ProWorker", 0, 0, true);

        PrefabTrickLegged = Resources.Load("TrickLegged", typeof(GameObject)) as GameObject;

        PrefabCricket = Resources.Load("Cricket", typeof(GameObject)) as GameObject;

        InstantiateTrickLegged(true);
        InstantiateCricket(true);

        PrefabAphids = Resources.Load("Aphids", typeof(GameObject)) as GameObject;

        InstantiateAphids(true);

    }

    public void AddResourse(Enums.TypeResourse type, int count)
    {

        this.inventory[type] += count;
        Debug.Log($"[{this.team}] Ресурс {type} добавлен в количестве {count}; Итого {this.inventory[type]}");
    }

    public void InstantiateWarrior(string modelName, int health, int damage, int protection, int enemiesnum, bool isHidden = false)
    {

        GameObject cfg;

        if (modelName.Equals("Warrior"))
            cfg = Instantiate(PrefabWarrior);
        else if (modelName.Equals("ElderWarrior"))
            cfg = Instantiate(PrefabElderWarrior);
        else if (modelName.Equals("LegendWarrior"))
            cfg = Instantiate(PrefabLegendWarrior);
        else if (modelName.Equals("LegendCareWarrior"))
            cfg = Instantiate(PrefabLegendCareWarrior);
        else if (modelName.Equals("UpperLegendWarrior"))
            cfg = Instantiate(PrefabUpperLegendWarrior);
        else
            cfg = new GameObject();

        cfg.GetComponent<BaseWarrior>().Init(health, damage, protection, enemiesnum);
        cfg.GetComponent<BaseWarrior>().SetBase(basePos);
        cfg.GetComponent<BaseWarrior>().SetTargets(targetPos);
        cfg.GetComponent<BaseWarrior>().SetTeam(team);

        Vector3 currentPos = transform.position;

        Vector3 newPos = currentPos;
        newPos.x -= 0.5f;
        newPos.y = 0.76f;
        newPos.z -= 0.5f;

        cfg.transform.position = newPos;
        cfg.transform.rotation = new Quaternion(-90, 0, 0, 0);

        if (isHidden)
            cfg.SetActive(false);

    }

    public void InstantiateWorker(string modelName, int health, int protection, bool isHidden = false)
    {

        GameObject cfg;

        if (modelName.Equals("Worker"))
            cfg = Instantiate(PrefabWorker);
        else if (modelName.Equals("ElderWorker"))
            cfg = Instantiate(PrefabElderWorker);
        else if (modelName.Equals("EliteWorker"))
            cfg = Instantiate(PrefabEliteWorker);
        else if (modelName.Equals("ProLikedWorker"))
            cfg = Instantiate(PrefabProLikedWorker);
        else if (modelName.Equals("ProWorker"))
            cfg = Instantiate(PrefabProWorker);
        else
            cfg = new GameObject();

        cfg.GetComponent<BaseWorker>().Init(health, protection);
        cfg.GetComponent<BaseWorker>().SetBase(basePos);
        cfg.GetComponent<BaseWorker>().SetTargets(targetPos);
        cfg.GetComponent<BaseWorker>().SetTeam(team);

        if (modelName.Equals("Worker"))
        {

            cfg.GetComponent<BaseWorker>().SetInventoryType(new Dictionary<Enums.TypeResourse, Tuple<int, int>>() {

                { Enums.TypeResourse.Drops, new Tuple<int,int>(0,1) }

            }, 1);

        }
        else if (modelName.Equals("ElderWorker"))
        {

            cfg.GetComponent<BaseWorker>().SetInventoryType(new Dictionary<Enums.TypeResourse, Tuple<int, int>>() {

                { Enums.TypeResourse.Leafs, new Tuple<int,int>(0,1) },
                { Enums.TypeResourse.Twigs, new Tuple<int,int>(0,1) }

            }, 1);

        }
        else if (modelName.Equals("EliteWorker"))
        {

            cfg.GetComponent<BaseWorker>().SetInventoryType(new Dictionary<Enums.TypeResourse, Tuple<int, int>>() {

                { Enums.TypeResourse.Leafs, new Tuple<int,int>(0,2) }

            }, 2);

        }
        else if (modelName.Equals("ProLikedWorker"))
        {

            cfg.GetComponent<BaseWorker>().SetInventoryType(new Dictionary<Enums.TypeResourse, Tuple<int, int>>() {

                { Enums.TypeResourse.Rocks, new Tuple<int,int>(0,1) },
                { Enums.TypeResourse.Drops, new Tuple<int,int>(0,1) }

            }, 2);

        }
        else if (modelName.Equals("ProWorker"))
        {

            cfg.GetComponent<BaseWorker>().SetInventoryType(new Dictionary<Enums.TypeResourse, Tuple<int, int>>() {

                { Enums.TypeResourse.Rocks, new Tuple<int,int>(0,1) },
                { Enums.TypeResourse.Twigs, new Tuple<int,int>(0,1) },
                { Enums.TypeResourse.Leafs, new Tuple<int,int>(0,1) }

            }, 2);

        }

        Vector3 currentPos = transform.position;

        Vector3 newPos = currentPos;
        newPos.x -= 0.5f;
        newPos.y = 0.76f;
        newPos.z -= 0.5f;

        cfg.transform.position = newPos;
        cfg.transform.rotation = new Quaternion(-90, 0, 0, 0);

        if (isHidden)
            cfg.SetActive(false);

    }

    public void InstantiateTrickLegged(bool isHidden = false)
    {

        GameObject cfg;

        cfg = Instantiate(PrefabTrickLegged);

        cfg.GetComponent<TrickLegged>().Init(16, 8, 5, 3);
        cfg.GetComponent<TrickLegged>().SetBase(basePos);
        cfg.GetComponent<TrickLegged>().SetTargets(targetPos);
        cfg.GetComponent<TrickLegged>().SetTeam(Enums.Team.Red);

        Vector3 currentPos = transform.position;

        Vector3 newPos = currentPos;
        newPos.x -= 0.5f;
        newPos.y = 0.76f;
        newPos.z -= 0.5f;

        cfg.transform.position = newPos;
        cfg.transform.rotation = new Quaternion(0, 0, 0, 0);

        if (isHidden)
            cfg.SetActive(false);

    }

    public void InstantiateAphids(bool isHidden = false)
    {

        GameObject cfg;

        cfg = Instantiate(PrefabAphids);

        cfg.GetComponent<Aphids>().Init(1, 1);
        cfg.GetComponent<Aphids>().SetBase(basePos);
        cfg.GetComponent<Aphids>().SetTargets(targetPos);

        cfg.GetComponent<BaseWorker>().SetInventoryType(new Dictionary<Enums.TypeResourse, Tuple<int, int>>() {}, 0);

        Vector3 currentPos = transform.position;

        Vector3 newPos = currentPos;
        newPos.x -= 0.5f;
        newPos.y = 0.76f;
        newPos.z -= 0.5f;

        cfg.transform.position = newPos;
        cfg.transform.rotation = new Quaternion(90, 0, 0, 0);

        if (isHidden)
            cfg.SetActive(false);

    }

    public void InstantiateCricket(bool isHidden = false)
    {

        GameObject cfg;

        cfg = Instantiate(PrefabCricket);

        cfg.GetComponent<Cricket>().Init(22, 8);
        cfg.GetComponent<Cricket>().SetBase(basePos);
        cfg.GetComponent<Cricket>().SetTargets(targetPos);
        cfg.GetComponent<Cricket>().SetTeam(Enums.Team.Black);

        cfg.GetComponent<BaseWorker>().SetInventoryType(new Dictionary<Enums.TypeResourse, Tuple<int, int>>() {

                { Enums.TypeResourse.Rocks, new Tuple<int,int>(0,1) },
                { Enums.TypeResourse.Twigs, new Tuple<int,int>(0,1) },
                { Enums.TypeResourse.Drops, new Tuple<int,int>(0,1) },
                { Enums.TypeResourse.Leafs, new Tuple<int,int>(0,1) }

        }, 3);

        Vector3 currentPos = transform.position;

        Vector3 newPos = currentPos;
        newPos.x -= 0.5f;
        newPos.y = 0.76f;
        newPos.z -= 0.5f;

        cfg.transform.position = newPos;
        cfg.transform.rotation = new Quaternion(90, 0, 0, 0);

        if (isHidden)
            cfg.SetActive(false);

    }

    public void InstantiateQueen(bool isChild)
    {

        GameObject cfg;

        cfg = Instantiate(PrefabQueen);


        cfg.GetComponent<Queen>().team = this.team;
        cfg.GetComponent<Queen>().SetColor();
        cfg.GetComponent<Queen>().isChild = isChild;

        cfg.transform.position = new Vector3(transform.position.x, 10.0f, transform.position.z);

    }

    void Start()
    {

        LoadResources();

        InitTargetsAndBase();

        if(this.team == Enums.Team.Black)
        {

            InstantiateWarrior("ElderWarrior", 2, 2, 1, 1);
            InstantiateWarrior("ElderWarrior", 2, 2, 1, 1);
            InstantiateWarrior("ElderWarrior", 2, 2, 1, 1);
            InstantiateWarrior("LegendWarrior", 10, 6, 6, 3);
            InstantiateWarrior("LegendWarrior", 10, 6, 6, 3);
            InstantiateWarrior("UpperLegendWarrior", 6, 2, 3, 2);
            InstantiateWarrior("UpperLegendWarrior", 6, 2, 3, 2);

            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ProLikedWorker", 6, 2);
            InstantiateWorker("ProLikedWorker", 6, 2);
            InstantiateWorker("ProLikedWorker", 6, 2);
            InstantiateWorker("ProLikedWorker", 6, 2);
            InstantiateWorker("ProWorker", 6, 2);
            InstantiateWorker("ProWorker", 6, 2);
            InstantiateWorker("ProWorker", 6, 2);
            InstantiateWorker("ProWorker", 6, 2);

            InstantiateCricket();
        }
        else
        {
            InstantiateWarrior("Warrior", 1, 1, 0, 1);
            InstantiateWarrior("Warrior", 1, 1, 0, 1);
            InstantiateWarrior("Warrior", 1, 1, 0, 1);
            InstantiateWarrior("Warrior", 1, 1, 0, 1);
            InstantiateWarrior("Warrior", 1, 1, 0, 1);
            InstantiateWarrior("LegendCareWarrior", 10, 6, 6, 3);
            InstantiateWarrior("LegendCareWarrior", 10, 6, 6, 3);
            InstantiateWarrior("LegendCareWarrior", 10, 6, 6, 3);
            InstantiateWarrior("LegendCareWarrior", 10, 6, 6, 3);

            InstantiateWorker("Worker", 1, 0);
            InstantiateWorker("Worker", 1, 0);
            InstantiateWorker("Worker", 1, 0);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("ElderWorker", 2, 1);
            InstantiateWorker("EliteWorker", 8, 4);
            InstantiateWorker("EliteWorker", 8, 4);
            InstantiateWorker("EliteWorker", 8, 4);

            InstantiateTrickLegged();
        }

    }

    void Update()
    {

    }

}
