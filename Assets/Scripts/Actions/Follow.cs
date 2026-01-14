using UnityEngine;

public class Follow : GAction
{
    GameObject nurseToFollow;

    protected override void Start()
    {
        agent.manualMove = true;
    }

    public override void Act()
    {
        running = true;
    }

    public override bool PrePerform()
    {
        nurseToFollow = GetComponent<Patient>().assignedNurse;
        return nurseToFollow != null;
    }

    public override bool PostPerform()
    {
        return true;
    }

    public override bool Perform()
    {
        if (agent.beliefs.ContainsKey("nurseFinishedEscort"))
        {
            return true;
        }

        // 2. Update destination to the Nurse's current position
        if (nurseToFollow != null)
        {
            // Optimization: Only set destination if the nurse has moved significantly
            agent.agent.SetDestination(nurseToFollow.transform.position);
        }

        return false; // Action is still in progress
    }
}
