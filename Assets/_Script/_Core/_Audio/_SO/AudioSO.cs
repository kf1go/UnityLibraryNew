using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Custom.Audio
{

    [CreateAssetMenu(fileName = "AudioSO", menuName = "SO/Audio/AudioSO")]
    public class AudioSO : ScriptableObject, IAudio
    {
        [Header("General")]
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public AudioMixerGroup AudioMixerGroup { get; private set; }
        [field: SerializeField] public bool IsLoop { get; private set; }
        [field: SerializeField] public bool EnableMaxCount { get; private set; }
        [field: SerializeField] public int MaxCount { get; private set; } = 1;
        [field: SerializeField] public int Hash { get; private set; }

        [field: SerializeField, Range(0, 256)] public int Priority { get; private set; } = 128;
        [field: SerializeField, Range(0, 1)] public float Volume { get; private set; } = 1;
        [field: SerializeField, Range(-3, 3)] public float Pitch { get; private set; } = 1;
        [field: SerializeField, Range(-1, 1)] public float StreoPan { get; private set; } = 0;
        [field: SerializeField] private bool is3D = true;
        [field: SerializeField, Range(0, 1.1f)] public float ReverbZoneMix { get; private set; } = 1;
        [field: SerializeField, Range(0, 5)] public float DopplerLevel { get; private set; } = 0;

        [Header("3D Sound Settings")]
        [field: SerializeField, Range(0, 360)] public int Spread { get; private set; }
        [field: SerializeField, Range(0, 1000)] public int MinDistance { get; private set; } = 1;
        [field: SerializeField, Range(1.01f, 1000)] public int MaxDistance { get; private set; } = 500;
        [field: SerializeField] public AudioRolloffMode AudioRolloffMode { get; private set; } = AudioRolloffMode.Linear;
        [field: SerializeField] public AnimationCurve RollOffCurve { get; private set; } = AnimationCurve.Linear(0, 1, 1, 0);

        public float GetSpatialBlend => is3D ? 1 : 0;

        public AudioSO GetAudio()
        {
            return this;
        }
        AudioSO IAudio.GetAudio()
        {
            return GetAudio();
        }
        private void OnValidate()
        {
            Hash = GetHashCode();
        }
    }
}
