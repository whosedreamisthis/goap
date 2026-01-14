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

        Nurse nurse = agent as Nurse;
        nurse.assignedPatient = target.GetComponent<Patient>();

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
        patient.assignedNurse = gameObject;

        return true;
    }
}
