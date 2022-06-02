using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Ant : MonoBehaviour
{

    public int HP = 1;
    public int Protection = 1;
    public int SourceProtection = 1;
    public bool isAlive = true;
    public float speed = 14.0f;

    public Transform basePos;
    public List<Transform> targetPos = new List<Transform>();
    public int targetNum = 0;

    public bool returnedToBase = false;
    public bool someAction = false;

    public Enums.Team team = Enums.Team.None;
    public Rigidbody rb;

    public virtual void GetDamage(int damage)
    {

        List<GameObject> listAnts = (FindObjectsOfType(typeof(GameObject)) as GameObject[]).ToList()
            .FindAll(t => t.GetComponent<Main>() != null);

        if(listAnts.Count != 0)
        {

            listAnts[0].GetComponent<AudioSource>().Play();

        }


        if (isAlive)
            this.HP -= damage - this.Protection;

        if(this.HP <= 0)
        {
            isAlive = false;
            Death(gameObject);
        }

    }

    public void Death(GameObject obj)
    {

        Debug.LogWarning($"Çהמנמגüÿ ןמדטברטל [{obj.name}]! ");
        if (!isAlive)
            Destroy(obj);


    }

    public void SetTeam(Enums.Team team)
    {

        this.team = team;

        SetColor();

    }

    public virtual void SetColor()
    {



    }

    public List<GameObject> GetNearEnemy(GameObject Ant, float distance)
    {

        Collider[] enemies = Physics.OverlapSphere(Ant.transform.position, distance);

        List<GameObject> result = new List<GameObject>();

        foreach (Collider col in enemies)
        {
            result.Add(col.gameObject);
        }

        result = result.FindAll(t => t.GetComponent<Ant>() != null);
        result = result.FindAll(t => t.GetComponent<Ant>().team != Ant.GetComponent<Ant>().team);

        return result;

    }

    public void SetBase(Transform target)
    {

        this.basePos = target;

    }

    public void SetTargets(List<Transform> targets)
    {

        this.targetPos = targets;

        if (this.basePos != null && this.targetPos[0] != this.basePos)
            this.targetPos.Insert(0, this.basePos);

    }
    
    public virtual void Restore()
    {

        this.Protection = this.SourceProtection;

    }

    //
    public virtual void Start()
    {
        
        rb = GetComponent<Rigidbody>();

    }

    //
    public virtual void FixedUpdate()
    {



    }

}
