using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public void GenerateMap() {
        var noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale,
            octaves, persistance, lacunarity, offset);

        var display = FindObjectOfType<MapDisplay>();
        display.UpdateHeights(minHeight, maxHeight);
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve));
    }

    private void OnValidate() {
        mapWidth = Mathf.Max(mapWidth, 1);
        mapHeight = Mathf.Max(mapHeight, 1);
        noiseScale = Mathf.Max(noiseScale, float.Epsilon);

        octaves = Mathf.Max(octaves, 1);
        lacunarity = Mathf.Max(lacunarity, 1);
    }

    public float minHeight {
        get {
            return meshHeightMultiplier * meshHeightCurve.Evaluate(0);
        }
    }

    public float maxHeight {
        get {
            return meshHeightMultiplier * meshHeightCurve.Evaluate(1);
        }
    }
}
