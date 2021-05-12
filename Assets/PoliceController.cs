using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : MonoBehaviour
{
    public GameObject policeman;
    public int policemanToSpawn = 3;
    public float spawnDistance = 10;

    [HideInInspector] public int policeCalls = 0;
    public static PoliceController Instance;

    Transform player;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerCharacter.Instance.transform;
    }

    public void CallThePolice()
    {
        policeCalls++;
        GetComponent<AudioSource>().Play();
        int policemenNum = Random.Range(1, 4);
        for (int i = 0; i < policemenNum; i++)
        {
            float randOffset = Random.Range(-5f, 5f);
            Vector3 offset = Vector3.forward * randOffset + Vector3.right * randOffset;
            Instantiate(policeman, player.position - player.forward * spawnDistance + offset, transform.rotation);
        }
    }
}