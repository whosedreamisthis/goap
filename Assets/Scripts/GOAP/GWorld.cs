using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GWorld
{
    static GWorld instance;
    private static Dictionary<string, bool> state;
    public static Queue<Patient> patients = new Queue<Patient>();
    public static Queue<GameObject> cubicles = new Queue<GameObject>();

    public static GWorld Instance
    {
        get
        {
            instance ??= new GWorld();
            return instance;
        }
    }

    static GWorld()
    {
        state = new Dictionary<string, bool>();
        GameObject[] cubicleGOs = GameObject.FindGameObjectsWithTag("Cubicle");
        for (int i = 0; i < cubicleGOs.Length; i++)
        {
            cubicles.Enqueue(cubicleGOs[i]);
        }
    }

    public void AddWaitingPatient(Patient p)
    {
        patients.Enqueue(p);
    }

    public Patient GetNextPatient()
    {
        if (patients.Count <= 0)
        {
            return null;
        }
        return patients.Dequeue();
    }

    public void ReleaseCubicle(GameObject cub)
    {
        cubicles.Enqueue(cub);
        if (!state["freeCubicle"])
        {
            state["freeCubicle"] = true;
        }
    }

    public GameObject ReserveCubicle()
    {
        if (cubicles.Count <= 1)
        {
            state["freeCubicle"] = false;
        }
        return cubicles.Dequeue();
    }

    private GWorld() { }

    public Dictionary<string, bool> GetState()
    {
        return state;
    }
}
