﻿using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAcademy : Academy
{
    private myArea[] areas;
    public override void AcademyReset()
    {
        if (areas == null)
        {
            areas = GameObject.FindObjectsOfType<myArea>();
        }

        foreach (myArea area in areas)
        {
            //area.numTruffles = (int)resetParameters["num_truffles"];
            //area.numStumps = (int)resetParameters["num_stumps"];
            //area.spawnRange = resetParameters["spawn_range"];
            //area.spawnRange = resetParameters["spawn_range"];
            area.ResetArea();
        }
    }
}
