using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, bool> state;
    public GAction action;

    public Node(Node p, float c, Dictionary<string, bool> s, GAction a)
    {
        parent = p;
        cost = c;
        state = s;
        action = a;
    }
}

public class GPlanner
{
    public Queue<GAction> Plan(
        List<GAction> actions,
        Dictionary<string, bool> goals,
        Dictionary<string, int> state
    )
    {
        List<Node> leaves = new List<Node>();
        Node root = new Node(null, 0, GWorld.Instance.GetState(), null);
        if (BuildGraph(root, leaves, actions, goals))
        {
            Node cheapest = null;
            foreach (Node n in leaves)
            {
                if (cheapest == null || n.cost < cheapest.cost)
                {
                    cheapest = n;
                }
            }

            List<GAction> result = new List<GAction>();
            Node prev = cheapest;
            while (prev != null)
            {
                if (prev.action != null)
                    result.Insert(0, prev.action);
                prev = prev.parent;
            }

            return new Queue<GAction>(result);
        }
        return null;
    }

    private bool BuildGraph(
        Node parent,
        List<Node> leaves,
        List<GAction> actions,
        Dictionary<string, bool> goals
    )
    {
        bool foundPath = false;

        foreach (GAction a in actions)
        {
            if (CheckConditions(a.preconditions, parent.state))
            {
                Dictionary<string, bool> newState = ApplyEffects(parent.state, a.effects);
                Node nextNode = new Node(parent, parent.cost + a.cost, newState, a);

                if (CheckConditions(goals, newState))
                {
                    leaves.Add(nextNode);
                    foundPath = true;
                }
                else
                {
                    List<GAction> remainingActions = new List<GAction>(actions);
                    remainingActions.Remove(a);
                    if (BuildGraph(nextNode, leaves, remainingActions, goals))
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    private bool CheckConditions(
        Dictionary<string, bool> conditions,
        Dictionary<string, bool> state
    )
    {
        foreach (KeyValuePair<string, bool> kv in conditions)
        {
            if (!state.ContainsKey(kv.Key) || state[kv.Key] != kv.Value)
            {
                return false;
            }
        }
        return true;
    }

    private Dictionary<string, bool> ApplyEffects(
        Dictionary<string, bool> currentState,
        Dictionary<string, bool> effects
    )
    {
        Dictionary<string, bool> newState = new Dictionary<string, bool>(currentState);
        foreach (KeyValuePair<string, bool> kv in effects)
        {
            newState.Add(kv.Key, kv.Value);
        }
        return newState;
    }
}
