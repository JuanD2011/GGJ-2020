using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips", menuName = "AudioClips")]
public class AudioClips : ScriptableObject
{
    [Header("Music")]
    public AudioClip music = null;

    [Header("UI")]
    public AudioClip defaultButton = null;

    [Header("Game")]
    public AudioClip winner = null;
    public AudioClip gameOver = null;

    public AudioClip cat = null;
    public AudioClip oldWoman = null;

    public AudioClip gunReload = null;
    public AudioClip gunShot = null;
    public AudioClip gunBullet = null;
}