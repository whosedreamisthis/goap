using UnityEngine;
using UnityEngine.UIElements;

public class Cubicle
{
    public bool isOccupied;
    public GameObject gObject;

    public Cubicle(GameObject go, bool io)
    {
        gObject = go;
        isOccupied = io;
    }

    public void GetPosition()
    {
        //return transform.position;
    }
}
