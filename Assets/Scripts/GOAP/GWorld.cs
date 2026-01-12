using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GWorld
{
    static GWorld instance;
    private static Dictionary<string, bool> state;
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
    }

    private GWorld() { }

    public Dictionary<string, bool> GetState()
    {
        return state;
    }
}
