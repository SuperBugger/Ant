using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWarrior : Ant
{

    public int Damage = 1;
    public int EnemiesNumber = 1;

    public int damageGiven = 0;

    public virtual void Init(int health, int damage, int protection, int enemiesnum)
    {
        this.targetNum = 0;
        this.HP = health;
        this.Damage = damage;
        this.Protection = protection;
        this.EnemiesNumber = enemiesnum;

    }

    public virtual void GiveDamage(Ant target)
    {

        target.GetDamage(this.Damage);

    }

    public override void SetColor()
    {

        if (this.team == Enums.Team.Red)
            GetComponent<Renderer>().material = (Resources.Load("Materials/Red_Warrior", typeof(Material)) as Material);

        if (this.team == Enums.Team.Black)
            GetComponent<Renderer>().material = (Resources.Load("Materials/Black_Warrior", typeof(Material)) as Material);

    }

    public override void Restore()
    {

        this.damageGiven = 0;

        base.Restore();

    }

    public override void FixedUpdate()
    {

        if (transform.position.y < -1000.0f)
            Destroy(rb.gameObject);

        List<GameObject> enemies = GetNearEnemy(gameObject, 10.0f);

        if (enemies.Count != 0 && damageGiven < EnemiesNumber)
        {

            for(int i = 0; i < EnemiesNumber && i < enemies.Count; i++)
            {

                enemies[i].GetComponent<Ant>().GetDamage(Damage);

                damageGiven++;

            }

        }
        else
        {

            if (targetNum < 0)
                return;
            
            if (targetPos.Count == 0)
                return;

            transform.position = Vector3.MoveTowards(transform.position, targetPos[targetNum].position, speed * Time.deltaTime);

            transform.LookAt(new Vector3(targetPos[targetNum].position.x, 2000, targetPos[targetNum].position.z));

            if (Vector3.Distance(transform.position, targetPos[targetNum].position) < 6f)
            {

                if (targetNum == 0 && !this.returnedToBase)
                {

                    targetNum = Random.Range(1, targetPos.Count);
                    
                    this.returnedToBase = true;

                }
                else
                {

                    targetNum = 0;

                    this.someAction = true;

                }

            }

        }

    }

}