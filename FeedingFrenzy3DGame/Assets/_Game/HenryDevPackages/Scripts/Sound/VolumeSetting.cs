using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HenryDev.Managers;

namespace BlockyBlock.UI
{
    public class VolumeSetting : MonoBehaviour
    {
        public Slider musicSlider;
        public Slider sfxSlider;
        protected virtual void Start()
        {
            musicSlider.onValueChanged.AddListener(HandleMusicValueChanged);
            sfxSlider.onValueChanged.AddListener(HandleSfxValueChanged);
        }
        protected virtual void OnDestroy()
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
        }
        protected virtual void OnEnable()
        {
            musicSlider.value = SoundManager.Instance.GetMusicVolume();
            sfxSlider.value = SoundManager.Instance.GetSFXVolume();
        }
        protected virtual void HandleMusicValueChanged(float value)
        {
            SoundManager.Instance.SetMusicVolume(value);
        }
        protected virtual void HandleSfxValueChanged(float value)
        {
            SoundManager.Instance.SetSFXVolume(value);
        }
    }
}
