using UnityEngine;

public class GetPatient : GAction
{
    public override bool PrePerform()
    {
        Patient patient = GWorld.Instance.GetNextPatient();
        if (patient == null)
        {
            return false;
        }
        target = patient.gameObject;

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
        return true;
    }
}
