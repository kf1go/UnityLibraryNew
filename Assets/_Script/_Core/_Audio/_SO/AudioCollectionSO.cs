using System.Collections.Generic;
using UnityEngine;

namespace Custom.Audio
{
    [CreateAssetMenu(fileName = "AudioCollection", menuName = "SO/Audio/AudioCollection")]
    public class AudioCollectionSO : ScriptableObject, IAudio
    {
        [SerializeField] private AudioSO[] audioList;
        public AudioSO GetRandomAudio => audioList[Random.Range(0, audioList.Length)];
        AudioSO IAudio.GetAudio()
        {
            return GetRandomAudio;
        }  
    }
}
