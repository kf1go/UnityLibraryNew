using UnityEngine;

namespace Custom.Audio
{
    [CreateAssetMenu(fileName = "AudioSOSet", menuName = "SO/AudioSOSet")]
    public class AudioSOSet : ScriptableObject, IAudio
    {
        [SerializeField] private AudioCollectionSO[] audioCollections;
        AudioSO IAudio.GetAudio()
        {
            int randomIndex = Random.Range(0, audioCollections.Length);
            return audioCollections[randomIndex].GetRandomAudio;
        }
    }
}
