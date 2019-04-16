using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int chunkSize, int seed,
        float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {

        var noiseMap = new float[chunkSize, chunkSize];
        var prng = new System.Random(seed);
        var octaveOffsets = new Vector2[octaves];

        float maxpossibleHeight = (1.0f - Mathf.Pow(persistance, octaves)) / (1.0f - persistance);

        for (int i = 0; i < octaves; ++i) {
            float offsetX = prng.Next(-10_000, 10_000) + offset.x;
            float offsetY = prng.Next(-10_000, 10_000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        for (int y = 0; y < chunkSize; ++y) 
            for(int x = 0; x < chunkSize; ++x) {

                float amplitude = 1.0f;
                float frequency = 1.0f;
                float noiseHeight = 0.0f;

                for (int i = 0; i < octaves; ++i) {
                    float sampleX = (x + octaveOffsets[i].x - chunkSize / 2) / scale * frequency;
                    float sampleY = (y + octaveOffsets[i].y - chunkSize / 2) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                noiseMap[x, y] = (noiseHeight + 1) / (2.0f * maxpossibleHeight / 1.2f);

            }

        return noiseMap;
    }
}
