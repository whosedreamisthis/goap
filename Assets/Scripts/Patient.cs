using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patient : GAgent
{
    public GameObject assignedNurse;
    public GameObject assignedCubicle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        SubGoal s1 = new SubGoal("isWaiting", true, true);
        goals.Add(s1);
        SubGoal s2 = new SubGoal("isTreated", true, true);
        goals.Add(s2);
        SubGoal s3 = new SubGoal("isHome", true, true);
        goals.Add(s3);
    }
}
