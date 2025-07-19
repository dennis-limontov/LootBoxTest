using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using System.Collections.Generic;
using UnityEngine;

namespace LootBox
{
    public class Reel : MonoBehaviourExtBind
    {
        [SerializeField]
        private float _speedMax = 50f;

        [SerializeField]
        private float _speedVariance = 5f;

        [SerializeField]
        private float _speedSlowing = 25f;

        [SerializeField]
        private float _timeOfDeviation = 0.25f;

        [SerializeField]
        private float _timeOfAcceleration = 0.5f;

        [SerializeField]
        private float _timeOfDeceleration = 3f;
        
        [SerializeField]
        private RectTransform _centerPoint;

        private float _speedMaxRandomized;

        private float _speedCurrent;

        private RectTransform _reelColumn;

        private Slot[] _reelSlots;

        [OnAwake]
        private void ReelAwake()
        {
            _reelColumn = GetComponent<RectTransform>();
            _reelColumn.anchoredPosition = new Vector2(_reelColumn.anchoredPosition.x,
                _reelColumn.sizeDelta.y / 2f);
            _speedCurrent = 0f;
            _reelSlots = GetComponentsInChildren<Slot>();
        }

        [OnUpdate]
        private void SpinStep()
        {
            transform.Translate(Vector2.down * Time.deltaTime * _speedCurrent);
            if (_reelColumn.anchoredPosition.y <= 0f)
            {
                _reelColumn.anchoredPosition = new Vector2(_reelColumn.anchoredPosition.x,
                    _reelColumn.sizeDelta.y / 2f);
            }
        }

        [Bind(Names.Events.ON_SPIN_STARTED)]
        private void SpinStart()
        {
            Path = CPath.Create();
            Path.EasingCircEaseIn(_timeOfDeviation, 0f, -_speedMax / _speedSlowing, SetCurrentSpeed);
            Path.EasingCircEaseIn(_timeOfDeviation, -_speedMax / _speedSlowing, 0f, SetCurrentSpeed);
            _speedMaxRandomized = _speedMax + UnityEngine.Random.Range(-_speedVariance, _speedVariance); 
            Path.EasingCubicEaseIn(_timeOfAcceleration, 0f, _speedMaxRandomized, SetCurrentSpeed);
            Path.Action(() => Settings.Model.Set(Names.ModelFields.BLUR_PUCTURE, true));
        }

        [Bind(Names.Events.ON_SPIN_STOPPING)]
        private void SpinStopping()
        {
            Path = CPath.Create();
            Path.EasingQuadEaseInOut(_timeOfDeceleration, _speedMaxRandomized, _speedMax / _speedSlowing, SetCurrentSpeed)
                .Action(() =>
                {
                    Path = CPath.Create();
                    Settings.Model.Set(Names.ModelFields.BLUR_PUCTURE, false);
                    float minDistance = float.MaxValue;
                    int minDistanceIndex = 0;
                    for (int i = 0; i < _reelSlots.Length; i++)
                    {
                        float localDistanceY = Mathf.Abs(_reelSlots[i].transform.position.y - _centerPoint.transform.position.y);
                        if ((localDistanceY < minDistance) && (_reelSlots[i].transform.position.y >= _centerPoint.transform.position.y))
                        {
                            minDistance = localDistanceY;
                            minDistanceIndex = i;
                        }
                    }
                    //Debug.Log($"Closest for center: index = {minDistanceIndex}; distance = {minDistance}");
                    Path.Wait(minDistance / (_speedMax / _speedSlowing));
                    Path.EasingCircEaseIn(_timeOfDeviation * 2f, _speedMax / _speedSlowing, 0f, SetCurrentSpeed);
                    Path.EasingCircEaseIn(_timeOfDeviation, 0f, -_speedMax / _speedSlowing, SetCurrentSpeed);
                    Path.EasingCircEaseIn(_timeOfDeviation, -_speedMax / _speedSlowing, 0f, SetCurrentSpeed);
                    Path.Action(() =>
                    {
                        Dictionary<Reel, SlotPicture> _winners;
                        _winners = Settings.Model.Get(Names.ModelFields.WINNERS, new Dictionary<Reel, SlotPicture>());
                        _winners[this] = _reelSlots[minDistanceIndex].PictureName;
                        Settings.Model.Set(Names.ModelFields.WINNERS, _winners);
                        Settings.Model.EventManager.Invoke(Names.Events.ON_REEL_STOPPED);
                    });
                });
        }

        private void SetCurrentSpeed(float speed)
        {
            _speedCurrent = speed;
        }

        [Bind(Names.Events.TEST)]
        public void DoSmth()
        {
            Debug.Log("Lesha Shuboff iz da best");
        }
    }
}