using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject roadPrefab;
    [SerializeField] float roadLength;
    [SerializeField] int roadsOnScreen = 3;

    float safeZone = 50;
    float spawnX = 0;
    List<GameObject> allRoads;
    // Start is called before the first frame update
    void Start()
    {
        allRoads = new List<GameObject>();
        for (int i = 0; i < roadsOnScreen; i++)
        {
            SpawnRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x - safeZone > (spawnX - roadsOnScreen * roadLength))
        {
            SpawnRoad();
            RemoveFirstRoad();
        }
    }

    void SpawnRoad()
    {
        GameObject go = Instantiate(roadPrefab, Vector3.right * spawnX, transform.rotation, transform);
        spawnX += roadLength;
        allRoads.Add(go);
    }

    void RemoveFirstRoad()
    {
        Destroy(allRoads[0]);
        allRoads.RemoveAt(0);
    }
}
