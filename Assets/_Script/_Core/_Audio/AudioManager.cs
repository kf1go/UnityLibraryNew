using System;
using UnityEngine;
using Custom.Pool;


namespace Custom.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        protected override MonoSingletonFlags SingletonFlag => MonoSingletonFlags.DontDestroyOnLoad; 

        [SerializeField] private PoolMonoBehaviourSO poolPrefabMonoBehaviourSO;
        //private static AudioEmitter audioEmitter2D;
        protected override void Awake()
        {
            base.Awake();
            MonoGenericPool<AudioEmitter>.Initialize(poolPrefabMonoBehaviourSO);
            //audioEmitter2D = MonoGenericPool<AudioEmitter>.Pop();
            //audioEmitter2D.name = "audioEmitter2D";
        }
        public static AudioEmitter GetEmitter()
        {
            AudioEmitter audioEmitter = MonoGenericPool<AudioEmitter>.Pop();
            return audioEmitter;
        }
    }
}
