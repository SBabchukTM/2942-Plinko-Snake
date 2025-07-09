using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Runtime.Core.Audio;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Runtime.Core.UI.Popup
{
    public class BasePopup : MonoBehaviour
    {
        private const float InAnimTime = 0.2f;
        
        private const string OpenPopupSound = "OpenPopup";
        private const string ClosePopupSound = "ClosePopupSound";

        private bool _isSoundEnable = true;

        [SerializeField] protected string _id;
        [SerializeField] private RectTransform _contentParent;

        protected ISoundService SoundService;
        
        public string Id => _id;

        [Inject]
        public void Construct(ISoundService soundService)
        {
            SoundService = soundService;
        }

        public virtual UniTask Show(CancellationToken cancellationToken = default)
        {
            PlayAnim();
            PlayAudio(OpenPopupSound);
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        private void PlayAnim()
        {
            if(_contentParent == null)
                return;
            
            _contentParent.localScale = Vector3.zero;
            _contentParent.DOScale(Vector3.one, InAnimTime).SetEase(Ease.InCubic).SetLink(gameObject).SetUpdate(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void HideImmediately()
        {
            gameObject.SetActive(false);
        }

        public virtual void DestroyPopup()
        {
            PlayAudio(ClosePopupSound);
            Destroy(gameObject);
        }

        protected void PlayAudio(string soundName)
        {
            if(_isSoundEnable)
                SoundService.PlaySound(soundName);
        }
    }
}