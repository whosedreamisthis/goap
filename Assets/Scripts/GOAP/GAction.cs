using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class WorldStateItem
{
    public string key;
    public bool value;
}

public class GAction : MonoBehaviour
{
    [SerializeField]
    List<WorldStateItem> preconditionsList = new List<WorldStateItem>();

    [SerializeField]
    List<WorldStateItem> effectsList = new List<WorldStateItem>();

    public Dictionary<string, bool> preconditions;
    public Dictionary<string, bool> effects;

    [SerializeField]
    public GameObject target;

    [SerializeField]
    string targetTag;
    public float cost = 1.0f;
    public float duration = 0.0f;

    public bool running = false;

    protected GAgent agent;

    void Awake()
    {
        agent = GetComponent<GAgent>();
        if (target == null && targetTag != null && targetTag != "")
        {
            target = GameObject.FindGameObjectWithTag(targetTag);
        }
        preconditions = new Dictionary<string, bool>();
        foreach (WorldStateItem wsi in preconditionsList)
        {
            preconditions.Add(wsi.key, wsi.value);
        }
        effects = new Dictionary<string, bool>();
        foreach (WorldStateItem wsi in effectsList)
        {
            effects.Add(wsi.key, wsi.value);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        agent.manualMove = false;
    }

    // Update is called once per frame
    void Update() { }

    public virtual void Act()
    {
        agent.agent.SetDestination(target.transform.position);
        running = true;
    }

    public virtual bool PrePerform()
    {
        return true;
    }

    public virtual bool PostPerform()
    {
        return true;
    }

    public virtual bool Perform()
    {
        return !agent.agent.pathPending && agent.agent.remainingDistance < 2;
    }
}
