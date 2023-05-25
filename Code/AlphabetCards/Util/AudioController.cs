using Sonigon;
using UnityEngine;

namespace Assets.AlphabetCards.Code.AlphabetCards.Util
{
    internal class AudioController
    {
        private static readonly SoundParameterIntensity SoundParameterIntensity = new SoundParameterIntensity(0f, UpdateMode.Continuous);

        public static void Play(AudioClip audioClip, Transform transform)
        {
            Play(audioClip, transform, 1.0f);
        }

        public static void Play(AudioClip audioClip, Transform transform, float volumeMultiplier)
        {
            SoundParameterIntensity.intensity = 1f;
            SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
            soundContainer.setting.volumeIntensityEnable = true;
            soundContainer.audioClip[0] = audioClip;
            SoundEvent soundEvent = ScriptableObject.CreateInstance<SoundEvent>();
            soundEvent.soundContainerArray[0] = soundContainer;
            SoundParameterIntensity.intensity = 1f * volumeMultiplier;
            SoundManager.Instance.Play(soundEvent, transform, SoundParameterIntensity);
        }
    }
}
