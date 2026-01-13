using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubGoal
{
    public Dictionary<string, bool> goals;
    public bool remove;

    public SubGoal(string k, bool v, bool r)
    {
        goals = new Dictionary<string, bool> { { k, v } };
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    List<GAction> actions = new List<GAction>();
    protected List<SubGoal> goals = new List<SubGoal>();
    public Dictionary<string, bool> beliefs = new Dictionary<string, bool>();
    GAction currentAction;
    SubGoal currentGoal;

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GInventory inventory = new GInventory();

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GAction[] inActions = gameObject.GetComponents<GAction>();
        foreach (GAction a in inActions)
        {
            actions.Add(a);
        }
    }

    void CompleteCurrentAction()
    {
        currentAction.PostPerform();
        currentAction.running = false;
        currentAction = null;
        if (actionQueue != null && actionQueue.Count == 0)
        {
            actionQueue = null;

            goals.Remove(currentGoal);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (currentAction != null)
        {
            if (!agent.pathPending && agent.remainingDistance < 2)
            {
                Invoke("CompleteCurrentAction", currentAction.duration);
            }
        }
        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();
            foreach (SubGoal sg in goals)
            {
                actionQueue = planner.Plan(actions, sg.goals, beliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg;
                    break;
                }
            }
        }
        if (
            actionQueue != null
            && actionQueue.Count > 0
            && (currentAction == null || !currentAction.running)
        )
        {
            currentAction = actionQueue.Dequeue();
            bool success = currentAction.PrePerform();
            if (!success)
            {
                planner = null;
                actionQueue = null;
            }
            else
                currentAction.Act();
        }
    }
}
