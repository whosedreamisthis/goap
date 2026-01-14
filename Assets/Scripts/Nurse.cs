using UnityEngine;

public class Nurse : GAgent
{
    public GAgent assignedPatient;

    protected override void Start()
    {
        SubGoal s1 = new SubGoal("treatPatient", true, false);
        goals.Add(s1);
    }
}
