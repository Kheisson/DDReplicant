using UnityEngine;

/// <summary>
/// Stage creator with a song name, score and tracks
/// </summary>
[CreateAssetMenu(fileName = "New Song", menuName = "Add New Song")]
public class StageSO : ScriptableObject
{
    public ushort bestScore;
    public string songName;
    public AudioClip previewClip, fullTrack;
}
