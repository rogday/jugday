using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed,
        float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        var noiseMap = new float[mapWidth, mapHeight];
        var prng = new System.Random(seed);
        var octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; ++i) {
            float offsetX = prng.Next(-10000, 10000) + offset.x;
            float offsetY = prng.Next(-10000, 10000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxHeight = float.MinValue, minHeight = float.MaxValue;

        scale = Mathf.Max(scale, 0.0001f);

        for (int y = 0; y < mapHeight; ++y) {
            for(int x = 0; x < mapWidth; ++x) {

                float amplitude = 1.0f;
                float frequency = 1.0f;
                float noiseHeight = 0.0f;

                for (int i = 0; i < octaves; ++i) {
                    float sampleX = (x - mapWidth / 2) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - mapHeight / 2) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                noiseMap[x, y] = noiseHeight;

                maxHeight = Mathf.Max(maxHeight, noiseHeight);
                minHeight = Mathf.Min(minHeight, noiseHeight);
            }
        }

        for (int y = 0; y < mapHeight; ++y)
            for (int x = 0; x < mapWidth; ++x)
                noiseMap[x, y] = Mathf.InverseLerp(minHeight, maxHeight, noiseMap[x, y]);

        return noiseMap;
    }
}
