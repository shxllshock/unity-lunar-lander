using UnityEngine;
public class PerlinNoise : MonoBehaviour {
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float seed) {
        float[,] noiseMap = new float[mapHeight, mapHeight];

        if (scale <= 0) {
            scale = 0.0001f;
        }
        
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float sampleX = (x / scale) + seed;
                float sampleY = (y / scale) + seed;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}