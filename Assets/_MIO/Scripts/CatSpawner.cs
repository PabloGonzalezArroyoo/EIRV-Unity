using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.AI.Navigation;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] gatos;
    [SerializeField]
    NavMeshSurface spawn;
    public void SpawnCat()
    {
        Instantiate(gatos[Random.Range(0, gatos.Length - 1)], spawn.center, Quaternion.identity);
    }
}
