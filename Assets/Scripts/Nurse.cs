using UnityEngine;

public class Nurse : GAgent
{
    protected override void Start()
    {
        SubGoal s1 = new SubGoal("treatPatient", true, false);
        goals.Add(s1);
    }
}
