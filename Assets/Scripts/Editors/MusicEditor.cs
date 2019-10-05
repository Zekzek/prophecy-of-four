using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MusicBehaviour))]
public class MusicEditor : Editor
{
    private static string DEFAULT_SONG_JSON = "{\n" +
        "  \"beatsPerBar\": 4,\n" +
        "  \"quarterNotesPerBar\": 4,\n" +
        "  \"tempo\": \"LARGO\",\n" +
        "  \"noteFade\": 0.4,\n" +
        "  \"key\": {\n" +
        "    \"key\": \"C\",\n" +
        "    \"octive\": 4\n" +
        "  },\n" +
        "  \"chordType\": \"TRIAD\",\n" +
        "  \"chords\": [\n" +
        "    {\n" +
        "      \"noteOffset\": 0,\n" +
        "      \"duration\": 0.25\n" +
        "    },\n" +
        "    {\n" +
        "      \"rest\": true,\n" +
        "      \"duration\": 0.25\n" +
        "    }\n" +
        "  ]\n" +
        "}";

    private string songJson;
    private string filename;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MusicBehaviour musicBehaviour = (MusicBehaviour)target;

        if (GUILayout.Button("Start"))
        {
            Time.timeScale = 0.06f;
            musicBehaviour.SongJson = songJson;
            EditorApplication.update -= musicBehaviour.Update;
            EditorApplication.update += musicBehaviour.Update;
        }

        if (GUILayout.Button("Stop"))
        {
            EditorApplication.update -= musicBehaviour.Update;
        }

        songJson = GUILayout.TextArea(songJson, GUILayout.Height(300), GUILayout.ExpandHeight(true));

        if (GUILayout.Button("Reset"))
        {
            songJson = DEFAULT_SONG_JSON;
        }

        filename = GUILayout.TextField(filename);

        if (GUILayout.Button("Save"))
        {
            string path = "Assets" + Path.DirectorySeparatorChar +
                "Resources" + Path.DirectorySeparatorChar +
                "Music" + Path.DirectorySeparatorChar +
                filename + ".json";
            StreamWriter writer = new StreamWriter(path, true);
            writer.Write(songJson);
        }
    }
}