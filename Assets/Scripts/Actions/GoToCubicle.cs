using UnityEngine;

public class GoToCubicle : GAction
{
    bool signalSent = false;

    public override bool PrePerform()
    {
        signalSent = false;
        target = agent.inventory.FindItemByTag("Cubicle");
        return target != null;
    }

    public override bool PostPerform()
    {
        GameObject cubicle = agent.inventory.RemoveItemWithTag("Cubicle");
        if (cubicle == null)
            return false;
        GWorld.Instance.ReleaseCubicle(cubicle);

        return true;
    }

    public override bool Perform()
    {
        if (!agent.agent.pathPending && agent.agent.remainingDistance < 3)
        {
            if (!signalSent)
            {
                Nurse nurse = agent as Nurse;
                nurse.assignedPatient.beliefs.Add("nurseFinishedEscort", true);
                signalSent = true;
            }
            return true;
        }
        return false;
    }
}
