using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{
  
    public Enums.Team team = Enums.Team.None;
    public Rigidbody rb;
    public Vector3 newpos;

    public GameObject PrefabColony;
    public bool inPosition = false;
    public bool isChild = false;

    public void SpawnNewColony()
    {

        PrefabColony.gameObject.transform.position = this.transform.position;

        Instantiate(PrefabColony);

    }

    public void SetColor()
    {

        if (this.team == Enums.Team.Red)
            GetComponent<Renderer>().material = (Resources.Load("Materials/Red_Warrior", typeof(Material)) as Material);

        if (this.team == Enums.Team.Black)
            GetComponent<Renderer>().material = (Resources.Load("Materials/Black_Warrior", typeof(Material)) as Material);

    }

    public void SetPos(Vector3 v)
    {
        this.newpos = v;
    }

    public void Start()
    {

        rb = GetComponent<Rigidbody>();

        if (this.team == Enums.Team.Red)
            PrefabColony = Resources.Load("Base/BaseRed") as GameObject;

        if (this.team == Enums.Team.Black)
            PrefabColony = Resources.Load("Base/BaseBlack") as GameObject;

        if (!isChild)
        {

            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

        }
        else
        {

            int nX = (int)GameObject.FindGameObjectWithTag("Ground").transform.localScale.x;
            int nZ = (int)GameObject.FindGameObjectWithTag("Ground").transform.localScale.z;

            nX = Random.Range(-nX / 2, nX / 2);
            nZ = Random.Range(-nZ / 2, nZ / 2);

            gameObject.GetComponent<Queen>().SetPos(new Vector3(nX, 0.8f, nZ));

        }

    }

    public void FixedUpdate()
    {

        if(!isChild) { return; }

        if (!inPosition)
        {

            transform.position = Vector3.MoveTowards(transform.position, newpos, 10.0f * Time.deltaTime);

            transform.LookAt(new Vector3(newpos.x, 2000, newpos.z));

            if (Vector3.Distance(transform.position, newpos) < 6f)
            {

                inPosition = true;

                SpawnNewColony();

                Destroy(rb.gameObject);

            }

        }

    }

}
