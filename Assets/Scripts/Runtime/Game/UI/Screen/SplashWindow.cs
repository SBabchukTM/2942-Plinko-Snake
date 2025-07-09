using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class SplashWindow : BaseWindow
    {
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private float _minLoadTime;
        [SerializeField] private float _maxLoadTime;
        
        public override async UniTask Remove(CancellationToken cancellationToken = default)
        {
            await PlayLoadingAnimation(cancellationToken);
            await base.Remove(cancellationToken);
        }

        private async UniTask PlayLoadingAnimation(CancellationToken cancellationToken)
        {
            float totalLoadTime = UnityEngine.Random.Range(_minLoadTime, _maxLoadTime);
            float elapsed = 0f;

            _loadingSlider.value = 0f;

            while (elapsed < totalLoadTime)
            {
                cancellationToken.ThrowIfCancellationRequested();

                float progressSpeed = UnityEngine.Random.Range(0.5f, 1.5f);
                float deltaTime = Time.deltaTime;

                elapsed += deltaTime * progressSpeed;
                float progress = Mathf.Clamp01(elapsed / totalLoadTime);
                _loadingSlider.value = progress;

                if (UnityEngine.Random.value < 0.005f)
                {
                    float stutterDuration = UnityEngine.Random.Range(50, 100);
                    await UniTask.Delay((int)stutterDuration, cancellationToken: cancellationToken);
                }

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            _loadingSlider.value = 1f;
        }
    }
}