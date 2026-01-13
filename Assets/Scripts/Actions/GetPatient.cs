using UnityEngine;

public class GetPatient : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetNextPatient();
        if (target == null)
        {
            return false;
        }

        GameObject cubicle = GWorld.Instance.ReserveCubicle();
        if (cubicle == null)
        {
            return false;
        }

        agent.inventory.AddItem(cubicle);
        return true;
    }

    public override bool PostPerform()
    {
        Patient patient = target.GetComponent<Patient>();
        patient.beliefs.Add("isFetched", true);
        patient.assignedCubicle = agent.inventory.FindItemByTag("Cubicle");

        return true;
    }
}
