using Custom.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Audio
{
    public class AudioEmitter : MonoBehaviour, IPoolable
    {
        private static readonly Dictionary<int, int> audioPlayCount = new Dictionary<int, int>(16);
        private const string k_playWithoutInit = "playing without init";
        private const string k_instanceIsInPool = "audio emitter is already in pool";
        public event Action OnAudioPlayEnd;

        [Header("Preplace")]
        [SerializeField] private AudioSO defaultAudioSO;
        [SerializeField] private bool overrideVolume;
        private bool IsPrePlaced => defaultAudioSO != null;
        public bool IsPlaying => audioSource.isPlaying;

        [Header("General")]
        private AudioSource audioSource;
        private AudioSO currentAudioSO;

        private bool IsInitialized => currentAudioSO != null;
        private bool isInPool;               //flag for checking if this is inside the pool
        private bool decrasePlayCountOnStop; //flag for OnDisable/OnDestroy

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if (IsPrePlaced)
            {
                Initialize(defaultAudioSO);
            }
        }
        void IPoolable.OnCreate()
        {
            isInPool = true;
        }
        void IPoolable.OnPop()
        {
            isInPool = false;
            decrasePlayCountOnStop = false;
        }
        void IPoolable.OnPush()
        {
            isInPool = true;
            decrasePlayCountOnStop = false;

            OnAudioPlayEnd = null;
            currentAudioSO = null;
        }
        internal static bool IsAudioPlayable(AudioSO audioSO, bool autoIncrement = false)
        {
            if (!audioSO.EnableMaxCount)
            {
                return true;
            }

            int hash = audioSO.Hash;
            audioPlayCount.TryGetValue(hash, out int count);

            bool result = count < audioSO.MaxCount;
            if (result && autoIncrement)
            {
                audioPlayCount[hash] = ++count;
            }
            return result;
        }
        private static void DecreaseDictionaryInstance(AudioSO audioSO)
        {
            if (!audioSO.EnableMaxCount)
            {
                return;
            }

            int hash = audioSO.Hash;
            int result = --audioPlayCount[hash];
            Debug.Assert(result >= 0);
        }
        public void Play(bool destroyOnEnd = false)
        {
            if (isInPool)
            {
                throw new InvalidOperationException(k_instanceIsInPool);
            }
            if (!IsInitialized)
            {
                throw new InvalidOperationException(k_playWithoutInit);
            }

            bool flagPlayable = IsAudioPlayable(currentAudioSO, true);
            if (!flagPlayable)
            {
                Debug.LogWarning($"Audio Instance Reached Max capacity : {currentAudioSO.MaxCount} {currentAudioSO.name}", currentAudioSO);
                if (!IsPrePlaced)
                {
                    KillAudio();
                }
                return;
            }

            decrasePlayCountOnStop = true;

            audioSource.Play();
            StartCoroutine(WaitUntilAudioEnd());

            IEnumerator WaitUntilAudioEnd()
            {
                while (IsPlaying)
                {
                    yield return null;
                }

                OnAudioStop();

                if (destroyOnEnd)
                {
                    KillAudio();
                }
            }
        }
        public void PlayWithInit(IAudio audioSO, bool destroyOnEnd = false)
        {
            AudioSO audio = audioSO.GetAudio();

            if (IsPlaying) //can't initialize without stopping audio
            {
                StopAudio();
            }
            Initialize(audio);
            Play(destroyOnEnd);
        }
        public void PlayOneShot()
        {
            if (isInPool)
            {
                throw new InvalidOperationException(k_instanceIsInPool);
            }
            if (!IsInitialized)
            {
                throw new InvalidOperationException(k_playWithoutInit);
            }

            audioSource.PlayOneShot(currentAudioSO.Clip, currentAudioSO.Volume);
        }
        public void PlayOneShotWithInit(IAudio audioSO)
        {
            AudioSO audio = audioSO.GetAudio();
            if (IsPlaying) //can't initialize without stopping audio
            {
                StopAudio();
            }
            Initialize(audio);
            PlayOneShot();
        }
        /// <summary>
        /// note : stops coroutine
        /// </summary>
        public void StopAudio()
        {
            if (!IsPlaying)
            {
                return;
            }

            StopAllCoroutines();

            OnAudioStop();

            audioSource.Stop();
        }
        /// <summary>
        /// return audio to pool
        /// </summary>
        /// <exception cref="InvalidOperationException">instance is already in pool</exception>
        public void KillAudio()
        {
            if (isInPool)
            {
                throw new InvalidOperationException(k_instanceIsInPool);
            }

            StopAudio();
            MonoGenericPool<AudioEmitter>.Push(this);   //deactivate gameObject, auto cancel Coroutine.
        }
        public void Initialize(IAudio audioSO)
        {
            AudioSO audio = audioSO.GetAudio();

            if (IsPlaying)
            {
                Debug.LogWarning($"{name}_initializing while playing audio");
                return;
            }

            currentAudioSO = audio;

            //global
            audioSource.clip = audio.Clip;
            audioSource.outputAudioMixerGroup = audio.AudioMixerGroup;

            //preplaced settings
            if (!overrideVolume)
            {
                audioSource.volume = audio.Volume;
            }
            if (IsPrePlaced)
            {
                return;
            }

            audioSource.priority = audio.Priority;
            audioSource.pitch = audio.Pitch;
            audioSource.panStereo = audio.StreoPan;
            audioSource.spatialBlend = audio.GetSpatialBlend;
            audioSource.reverbZoneMix = audio.ReverbZoneMix;

            //3DSOUND SETTINGS
            audioSource.dopplerLevel = audio.DopplerLevel;
            audioSource.spread = audio.Spread;
            audioSource.rolloffMode = audio.AudioRolloffMode;
            audioSource.minDistance = audio.MinDistance;
            audioSource.maxDistance = audio.MaxDistance;
            if (audio.AudioRolloffMode == AudioRolloffMode.Custom)
            {
                audioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, audio.RollOffCurve);
            }
        }
        private void OnAudioStop()
        {
            if (OnAudioPlayEnd != null)
            {
                OnAudioPlayEnd.Invoke();
            }
            if (decrasePlayCountOnStop)
            {
                DecreaseDictionaryInstance(currentAudioSO);
                decrasePlayCountOnStop = false;
            }
        }
        private void OnDestroy()
        {
            if (!isInPool)
            {
                OnAudioStop();
            }
        }
    }
}
