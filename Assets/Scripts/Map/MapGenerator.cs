using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {

    [Header("Objects")]
    public GameObject mapObject;
    
    [Header("Perlin Noise Options")]
    public int mapScale;
    public float noiseScale;
    //public float random;
    public float seed;

    [Header("Map Options")]
    public float pointHeight;
    public float heightExaggeration;
    public float minHeight;
    public float maxHeight;
    public int noiseY; // This is so that we choose a random y value from the perlin noise map.
    
    [Header("Other Options")]
    public bool autoUpdate;
    
    
    private float startingSeed = 0;
    private float lastCameraPositionX = 0;
    public void Update() {
        // Infinite generation for when the camera is scrolled on the X axis.
        float cameraPositionX = Camera.main.transform.position.x;
        float diff = cameraPositionX - lastCameraPositionX;
        if (diff > 0.05f || diff < -0.05f) {
            lastCameraPositionX = cameraPositionX;
            seed = startingSeed + (cameraPositionX / (float) Math.PI);
            GenerateMap();
        }
    }

    public void GenerateMap() {
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(mapScale, mapScale, noiseScale, seed); // The main noise map

        MapDisplay display = GetComponent<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
        
        // Generating points of the line renderer
        
        // These variables are to compensate and make sure that the points are inside the cameras view.
        float cameraMinX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x;
        float cameraMaxX = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x + 0.3f;
        float pointGap = (cameraMaxX - cameraMinX) / mapScale;

        Vector3[] points = new Vector3[mapScale];
        float gap = 0; // This variable is to keep track of the gap in between the points
        for (int i = 0; i < mapScale; i++) { 
            // Generating the X and Y values for the point from the noise map generated
            float pointX = (gap + cameraMinX);
            float pointY = ((noiseMap[noiseY, i] * heightExaggeration) - pointHeight);
            
            pointY = Mathf.Clamp(pointY, -minHeight, maxHeight); // Clamping the height.
            
            // Assigning the generated coordinates to a list so that we can give it to the line renderer
            points[i] = (new Vector3(pointX, pointY));
            
            gap += pointGap; // Incrementing the gap for the next point.
        }
        
        // Giving the points generated to the line renderer.
        LineRenderer lineRenderer = mapObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = mapScale;
        lineRenderer.SetPositions(points);
        
        // Generating collider from line renderer
        
        // Getting the edge collider component from the map object
        EdgeCollider2D edgeCollider2D = mapObject.GetComponent<EdgeCollider2D>();
        if (edgeCollider2D == null) // If it does not exist then add it to the map object.
            edgeCollider2D = mapObject.AddComponent<EdgeCollider2D>();
        
        // We have to convert the points list we have to a Vector2 list instead of a Vector3 array because thats the only thing that the edge collider's SetPoints method will accept.
        List<Vector2> v2points = new List<Vector2>();
        foreach (Vector3 point in points) // Converting to Vector2
            v2points.Add(new Vector2(point.x, point.y));
        edgeCollider2D.SetPoints(v2points); // Giving the edge collider the converted points.
    }

    public void GenerateSeed() {
        seed = Random.Range(0, 100000);
        noiseY = Random.Range(0, mapScale);
        GenerateMap();
    }

    private void Start() {
        startingSeed = seed;
        GenerateMap();
    }
}