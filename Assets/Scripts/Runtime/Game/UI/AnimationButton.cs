using Runtime.Core.Audio;
using Runtime.Game.Services.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace Runtime.Game.UI
{
    [RequireComponent(typeof(Animation), typeof(Button))]
    public class AnimationButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Animation _pressAnimation;

        private ISoundService _soundService;
        public Button Button => _button;

#if UNITY_EDITOR
        private void Reset()
        {
            _button = GetComponent<Button>();
            _pressAnimation = GetComponent<Animation>();

            UnityEventTools.AddPersistentListener(_button.onClick, PlayPressAnimation); 
            _pressAnimation.playAutomatically = false;

            _pressAnimation.clip = Resources.Load<AnimationClip>("ButtonClickAnim");
            _pressAnimation.AddClip(Resources.Load<AnimationClip>("ButtonClickAnim"), "ButtonClickAnim");
        }
#endif

        [Inject]
        public void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }

        public void PlayPressAnimation()
        {
            _pressAnimation.Play();
            _soundService.PlaySound(ConstAudioNames.ButtonSound);
        }
    }
}