using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Game.UI.Screen
{
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public abstract class BaseWindow : MonoBehaviour
    {
        [FormerlySerializedAs("_fadeDuration")] [SerializeField] protected float _fadeInDuration = 0.25f;
        [FormerlySerializedAs("_id")] [SerializeField] protected string _identifier;

        private CanvasGroup Canvas;
        private UniTaskCompletionSource _showCompletionSource;
        private UniTaskCompletionSource _hideCompletionSource;
        private Tween _showFadeTween;
        private Tween _hideFadeTween;

        public string Identifier => _identifier;

        private void Awake() => Canvas = GetComponent<CanvasGroup>();

        public virtual async UniTask Reveal(CancellationToken cancellationToken = default)
        {
            _showCompletionSource = new UniTaskCompletionSource();

            EnableCanvas();
            CreateFadeTween();
            
            RegisterFail(cancellationToken);

            await _showCompletionSource.Task;
        }

        private void EnableCanvas()
        {
            Canvas.interactable = true;
            Canvas.blocksRaycasts = true;
        }

        private void RegisterFail(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                _showFadeTween?.Kill();
                _showCompletionSource?.TrySetCanceled();
            });
        }

        private void CreateFadeTween()
        {
            _showFadeTween = Canvas.DOFade(1, _fadeInDuration)
                .OnComplete(()=>
                {
                    _showCompletionSource?.TrySetResult();
                })
                .From(0)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public virtual async UniTask Remove(CancellationToken cancellationToken = default)
        {
            _hideCompletionSource = new UniTaskCompletionSource();
            Canvas.interactable = false;

            CreateTween();

            Register(cancellationToken);

            await _hideCompletionSource.Task;
        }

        private void CreateTween()
        {
            _hideFadeTween = Canvas.DOFade(0, _fadeInDuration)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy).OnComplete(() =>
                {
                    _hideCompletionSource?.TrySetResult();
                    Destroy(gameObject);
                });
        }

        private void Register(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                _hideFadeTween?.Kill();
                _hideCompletionSource?.TrySetCanceled();
            });
        }

        public virtual void HideImmediately()
        {
            Canvas.alpha = 0;
            Canvas.interactable = false;
            Canvas.blocksRaycasts = false;
        }

        public void DestroyScreen() => Destroy(gameObject);
    }
}