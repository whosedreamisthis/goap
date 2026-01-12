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
    NavMeshAgent agent;
    List<GAction> actions = new List<GAction>();
    protected List<SubGoal> goals = new List<SubGoal>();
    GAction currentAction;

    GPlanner planner;
    Queue<GAction> actionQueue;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GAction[] inActions = gameObject.GetComponents<GAction>();
        foreach (GAction a in inActions)
        {
            actions.Add(a);
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
                currentAction.running = false;
                currentAction = null;
                if (actionQueue != null && actionQueue.Count == 0)
                {
                    actionQueue = null;
                }
            }
        }
        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();
            foreach (SubGoal sg in goals)
            {
                actionQueue = planner.Plan(actions, sg.goals, null);
                if (actionQueue != null)
                {
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
            currentAction.Act();
        }
    }
}
