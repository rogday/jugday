using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int chunkSize;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public Material material;

    public bool autoUpdate;

    public void GenerateMap(Vector2 position) {
        var noiseMap = Noise.GenerateNoiseMap(chunkSize, seed, noiseScale,
            octaves, persistance, lacunarity, position * (chunkSize - 1));

        Terrain t = FindObjectOfType<Terrain>();

        var display = FindObjectOfType<MapDisplay>();
        display.UpdateHeights(material, minHeight, maxHeight);
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), position * (chunkSize - 1), t.transform);
    }

    private void OnValidate() {
        chunkSize = Mathf.Max(chunkSize, 1);
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
