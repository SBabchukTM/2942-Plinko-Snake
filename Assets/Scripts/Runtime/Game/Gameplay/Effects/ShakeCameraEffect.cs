using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Effects
{
    public class ShakeCameraEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _psPrefab;
        
        private ParticleSystem _psEffect;

        private ExplosionEffect _expEffect;

        [Inject]
        private void Construct(ExplosionEffect explosionEffect)
        {
            _expEffect = explosionEffect;
            _expEffect.OnExplode += ProcessExploder;
        }

        private void Awake()
        {
            _psEffect = Instantiate(_psPrefab).GetComponent<ParticleSystem>();
        }

        private void OnDestroy()
        {
            _expEffect.OnExplode -= ProcessExploder;
        }

        private void ProcessExploder(Vector3 pos)
        {
            DoPs(pos);
        }

        private void DoPs(Vector3 pos)
        {
            _psEffect.transform.position = pos;
            _psEffect.Play();
            transform.DOShakePosition(0.5f, 10);
        }
    }
}