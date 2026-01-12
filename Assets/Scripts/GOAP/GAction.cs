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
    GameObject target;

    [SerializeField]
    string targetTag;
    public float cost = 1.0f;
    float duration = 0.0f;

    public bool running = false;

    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
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
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void Act()
    {
        agent.SetDestination(target.transform.position);
        running = true;
    }
}
