using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

namespace LootBox
{
    public class WinnersEffect : MonoBehaviourExtBind
    {
        [SerializeField]
        private ParticleSystem[] _effects;

        [Bind(Names.Events.ON_SPIN_STOPPED)]
        private void PlayEffects()
        {
            foreach (var effect in _effects)
            {
                effect.Play();
            }
        }
    }
}