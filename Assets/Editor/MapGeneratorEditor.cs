using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        MapGenerator mapGenerator = (MapGenerator) target;

        if (DrawDefaultInspector()) {
            if (mapGenerator.autoUpdate) {
                mapGenerator.Initialize();
                mapGenerator.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate")) {
            mapGenerator.Initialize();
            mapGenerator.GenerateMap();
        }
        
        if (GUILayout.Button("Generate Seed")) {
            mapGenerator.Initialize();
            mapGenerator.GenerateSeed();
            mapGenerator.GenerateMap();
        }
    }
}