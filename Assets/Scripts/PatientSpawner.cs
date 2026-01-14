using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject patientPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject g = Instantiate(patientPrefab, transform.position, Quaternion.identity);
        g.transform.parent = transform;

        Invoke("Spawn", Random.Range(2, 8));
    }
}
