using UnityEngine;

public class GetTreated : GAction
{
    public override bool PrePerform()
    {
        target = (agent as Patient).assignedCubicle;

        return target != null;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
