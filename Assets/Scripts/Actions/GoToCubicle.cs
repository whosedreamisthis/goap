using UnityEngine;

public class GoToCubicle : GAction
{
    public override bool PrePerform()
    {
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
}
