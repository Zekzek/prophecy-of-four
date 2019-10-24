using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtiliZek;

[RequireComponent(typeof(AudioSource))]
public class MusicBehaviour : MonoBehaviour
{
    public static int SAMPLE_FREQUENCY = 44100;

    public string songID;

    private AudioSource source;
    private float nextNoteTime = 1f;
    private SongData song;

    private IEnumerator<ChordData> chords;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        song = FileManager.GetSongData(songID);
        song.Init();
        chords = song.Chords;
    }

    public void Update()
    {
        if (nextNoteTime <= 0)
        {
            PlayNextNote();
        }
        nextNoteTime -= Time.deltaTime;
    }

    private void PlayNextNote()
    {
        if (!chords.MoveNext())
        {
            chords.Reset();
            chords.MoveNext();
        }

        float duration = (60f / song.BeatsPerMinute) * chords.Current.Duration;

        foreach (NoteData note in chords.Current.Notes)
        {
            AudioClip noteClip = GenerateSinNote(note.Frequency, duration, song.NoteFade);
            source.PlayOneShot(noteClip, 1f / chords.Current.Notes.Length);
        }

        nextNoteTime += duration;
    }

    private AudioClip GenerateSinNote(float frequency, float duration, float fade)
    {
        float[] samples = new float[Mathf.RoundToInt(SAMPLE_FREQUENCY * duration)];
        for (int i = 0; i < samples.Length; i++)
        {
            float fadeMult = 1;
            if (i < samples.Length * fade)
                fadeMult = i / (samples.Length * fade);
            else if (i > samples.Length * (1f - fade))
                fadeMult = (samples.Length - i) / (samples.Length * fade);

            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * frequency / SAMPLE_FREQUENCY) * fadeMult;
        }
        AudioClip clip = AudioClip.Create("Note", samples.Length, 1, SAMPLE_FREQUENCY, false);
        clip.SetData(samples, 0);

        return clip;
    }
#if UNITY_EDITOR
    private string songJson;
    public string SongJson
    {
        get { return songJson; }
        set
        {
            songJson = value;
            source = GetComponent<AudioSource>();
            song = JsonUtility.FromJson<SongData>(songJson);
            song.Init();
            chords = song.Chords;
        }
    }
#endif
}