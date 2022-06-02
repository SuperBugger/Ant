using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public Dictionary<Enums.TypeResourse, int> inventory = new Dictionary<Enums.TypeResourse, int>() {

        {  Enums.TypeResourse.Twigs, 0 },
        {  Enums.TypeResourse.Leafs, 0 },
        {  Enums.TypeResourse.Rocks, 0 },
        {  Enums.TypeResourse.Drops, 0 }

    };

    public void Init(int twigs, int leafs, int rocks, int drops)
    {

        this.inventory[Enums.TypeResourse.Twigs] = twigs;
        this.inventory[Enums.TypeResourse.Leafs] = leafs;
        this.inventory[Enums.TypeResourse.Rocks] = rocks;
        this.inventory[Enums.TypeResourse.Drops] = drops;

    }

    public int GetResourse(Enums.TypeResourse type, int count)
    {

        int result = count;

        if (this.inventory[type] < count)
            result = count - this.inventory[type];

        this.inventory[type] -= count;

        if (this.inventory[type] < 0)
            this.inventory[type] = 0;

        if (result < 0) 
            return 0;

        return result;

    }

}
