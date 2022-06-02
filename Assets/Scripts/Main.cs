using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{

    public int currentDay = 0;
    public int daySpawnNewAnt = 1;
    public int aphidsSpawnDay = 1;

    public bool isRestore = false;

    public void ConfigureTargets()
    {

        List<GameObject> result = new List<GameObject>();

        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {

            Target t = gameObj.GetComponent<Target>();

            if (!t) continue;

            result.Add(gameObj);

        }

        if (result.Count < 5)
            throw new System.Exception("Не удовлетворяет условию задачи: пять куч");

        result[0].GetComponent<Target>().Init(19, 0, 16, 41);
        result[1].GetComponent<Target>().Init(10, 49, 24, 0);
        result[2].GetComponent<Target>().Init(48, 21, 39, 48);
        result[3].GetComponent<Target>().Init(36, 16, 41, 49);
        result[4].GetComponent<Target>().Init(45, 38, 0, 0);

    }

    public bool IsAllInBase(bool debug = false)
    {

        List<GameObject> listAnts = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).ToList()
            .FindAll(t => t.GetComponent<Ant>() != null);

        listAnts = listAnts.FindAll(t => t.GetComponent<Ant>().isAlive);

        int totalCount = listAnts.Count;


        List<GameObject> t1 = listAnts.FindAll(t => t.GetComponent<Ant>().returnedToBase);

        List<GameObject> t2 = listAnts.FindAll(t => t.GetComponent<Ant>().someAction);

        List<GameObject> t3 = listAnts.FindAll(t => Vector3.Distance(t.transform.position, t.GetComponent<Ant>().targetPos[0].position) < 10f);


        int inBaseCount = listAnts.FindAll(t => t.GetComponent<Ant>().returnedToBase
                                            && t.GetComponent<Ant>().someAction
                                            && Vector3.Distance(t.transform.position, t.GetComponent<Ant>().targetPos[0].position) < 10f).Count;

        return totalCount == inBaseCount;

    }

    public void RestoreAnts()
    {

        this.isRestore = true;

        List<GameObject> listAnts = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).ToList()
            .FindAll(t => t.GetComponent<Ant>() != null);

        foreach (GameObject a in listAnts)
        {

            a.GetComponent<Ant>().returnedToBase = false;
            a.GetComponent<Ant>().someAction = false;
            a.GetComponent<Ant>().Restore();

        }

        this.isRestore = false;

    }


    void Start()
    {

        ConfigureTargets();

        daySpawnNewAnt = Random.Range(this.currentDay + 1, this.currentDay + 4);

        aphidsSpawnDay = Random.Range(0, 12);

    }


    void FixedUpdate()
    {

        if (this.IsAllInBase() && !this.isRestore)
        {

            if (this.currentDay == 13)
            {

                Debug.LogError("Засуха!");
                return;

            }
            else
            {

                if (this.currentDay == aphidsSpawnDay)
                {

                    List<GameObject> listColony3 = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).ToList()
                                                                .FindAll(t => t.GetComponent<Colony>() != null);

                    var g = listColony3[Random.Range(0, listColony3.Count)];
                    if (g.GetComponent<Colony>() != null)
                        g.GetComponent<Colony>().InstantiateAphids();

                    Debug.LogWarning("Тля на базе");

                }
                if (this.currentDay == aphidsSpawnDay + 1)
                {

                    Destroy(this.gameObject);

                }

            }

            Debug.LogError($"День {this.currentDay + 1}. Хорошего дня...");

            this.currentDay++;

            RestoreAnts();

            if (this.currentDay == this.daySpawnNewAnt)
            {
                List<string> antCollectionName = new List<string>()
                {

                    "Queen",
                    "Queen",
                    "Queen",
                    "Queen",
                    "Queen",
                    "Warrior",
                    "ElderWarrior",
                    "LegendWarrior",
                    "LegendCareWarrior",
                    "UpperLegendWarrior",
                    "Worker",
                    "ElderWorker",
                    "EliteWorker",
                    "ProLikedWorker",
                    "ProWorker"

                };

                int currentSpawn = Random.Range(0, antCollectionName.Count);

                if (antCollectionName[currentSpawn] == "Queen")
                {

                    List<GameObject> listColony = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).ToList()
                                                        .FindAll(t => t.GetComponent<Colony>() != null);

                    foreach (var g in listColony)
                    {

                        if (g.GetComponent<Colony>() != null)
                            g.GetComponent<Colony>().InstantiateQueen(true);

                    }

                    Debug.Log($"Новая Королева!");

                }
                else
                {

                    List<GameObject> listColony2 = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).ToList()
                                                        .FindAll(t => t.GetComponent<Colony>() != null);

                    switch (currentSpawn)
                    {

                        case 10:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWarrior(antCollectionName[currentSpawn], 1, 1, 0, 1);

                            }

                            Debug.Log($"Новый боец [{antCollectionName[currentSpawn]}]");

                            break;

                        case 11:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWarrior(antCollectionName[currentSpawn], 2, 2, 1, 1);

                            }

                            Debug.Log($"Новый боец [{antCollectionName[currentSpawn]}]");

                            break;

                        case 12:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWarrior(antCollectionName[currentSpawn], 10, 6, 6, 3);

                            }

                            Debug.Log($"Новый боец [{antCollectionName[currentSpawn]}]");

                            break;

                        case 13:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWarrior(antCollectionName[currentSpawn], 10, 6, 6, 3);

                            }

                            Debug.Log($"Новый боец [{antCollectionName[currentSpawn]}]");

                            break;

                        case 14:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWarrior(antCollectionName[currentSpawn], 6, 2, 3, 2);

                            }

                            Debug.Log($"Новый боец [{antCollectionName[currentSpawn]}]");

                            break;

                        case 15:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWorker(antCollectionName[currentSpawn], 1, 0);

                            }

                            Debug.Log($"Новый работяга [{antCollectionName[currentSpawn]}]");

                            break;

                        case 16:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWorker(antCollectionName[currentSpawn], 2, 1);

                            }

                            Debug.Log($"Новый работяга [{antCollectionName[currentSpawn]}]");

                            break;

                        case 17:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWorker(antCollectionName[currentSpawn], 8, 4);

                            }

                            Debug.Log($"Новый работяга [{antCollectionName[currentSpawn]}]");

                            break;

                        case 18:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWorker(antCollectionName[currentSpawn], 6, 2);

                            }

                            Debug.Log($"Новый работяга [{antCollectionName[currentSpawn]}]");

                            break;

                        case 19:

                            foreach (var g in listColony2)
                            {

                                if (g.GetComponent<Colony>() != null)
                                    g.GetComponent<Colony>().InstantiateWorker(antCollectionName[currentSpawn], 6, 2);

                            }

                            Debug.Log($"Новый работяга [{antCollectionName[currentSpawn]}]");

                            break;

                        default:

                            Debug.Log("Никто не родился(");

                            break;

                    }

                }

                daySpawnNewAnt = Random.Range(this.currentDay + 1, this.currentDay + 4);

            }

        }

    }

}
