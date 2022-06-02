using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseWorker : Ant
{

    //[Тип, [Текущее количество, Максимальное количество]]
    public Dictionary<Enums.TypeResourse, Tuple<int, int>> inventory = new Dictionary<Enums.TypeResourse, Tuple<int, int>>();
    public int capacity = 0;
    public int sourceCapacity = 0;
    public int sourceProtection = 0;

    public virtual void Init(int health, int protection)
    {

        this.targetNum = 0;
        this.HP = health;
        this.sourceProtection = protection;
        this.Protection = sourceProtection;

    }

    public virtual void SetInventoryType(Dictionary<Enums.TypeResourse, Tuple<int, int>> resources, int capacity)
    {

        this.inventory = resources;
        this.capacity = capacity;
        this.sourceCapacity = capacity;

    }

    public override void SetColor()
    {

        if (this.team == Enums.Team.Red)
            GetComponentInChildren<Renderer>().material = (Resources.Load("Materials/Red_Worker", typeof(Material)) as Material);

        if (this.team == Enums.Team.Black)
            GetComponentInChildren<Renderer>().material = (Resources.Load("Materials/Black_Worker", typeof(Material)) as Material);

    }

    public override void FixedUpdate()
    {

        if (targetNum < 0)
            return;

        if (targetPos.Count == 0)
            return;

        if (transform.position.y < -1000.0f)
            Destroy(rb.gameObject);

        Vector3 moveDirecrion = new Vector3(transform.position.x - targetPos[targetNum].position.x, rb.velocity.y, transform.position.z - targetPos[targetNum].position.z);
        Vector3 lookDirection = moveDirecrion + transform.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos[targetNum].position, speed * Time.deltaTime);

        transform.LookAt(new Vector3(lookDirection.x, transform.position.y, lookDirection.z));

        if (Vector3.Distance(transform.position, targetPos[targetNum].position) < 6f)
        {

            if (targetNum == 0 && !this.returnedToBase)
            {

                targetNum = UnityEngine.Random.Range(1, targetPos.Count);

                this.returnedToBase = true;

            }
            else if (targetNum == 0 && this.returnedToBase)
            {

                for (int i = 0; i < this.inventory.Count && this.capacity > 0 && (this.inventory.Select(t => t.Value.Item1).Sum() != 0); i++)
                {

                    Enums.TypeResourse type = this.inventory.ElementAt(i).Key;

                    int count = this.inventory[type].Item1;

                    targetPos[0].gameObject.GetComponent<Colony>().AddResourse(type, count);

                    this.inventory[type] = new Tuple<int, int>(0, this.inventory[type].Item2);

                }

                this.capacity = this.sourceCapacity;

            }
            else
            {

                for (int i = 0; i < this.inventory.Count && this.capacity > 0; i++)
                {

                    Enums.TypeResourse type = this.inventory.ElementAt(i).Key;

                    int typeCapacity = this.inventory[type].Item2 - this.inventory[type].Item1;

                    int taked = targetPos[targetNum].gameObject.GetComponent<Target>().GetResourse(type, typeCapacity);
                    this.capacity -= taked;

                    this.inventory[type] = new Tuple<int, int>(this.inventory[type].Item1 + taked, this.inventory[type].Item2);

                }

                targetNum = 0;

                this.someAction = true;

            }

        }

    }

}
