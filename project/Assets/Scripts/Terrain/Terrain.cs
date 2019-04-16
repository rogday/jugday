using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public int mapSize = 3;
    void Start()
    {
        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        int start = -mapSize / 2;
        int end = mapSize / 2;

        for (int i = start; i <= end; i++) {
            for (int k = start; k <= end; k++) {
                mapGenerator.GenerateMap(new Vector2(i, k));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
