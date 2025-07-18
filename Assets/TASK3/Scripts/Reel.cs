using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using UnityEngine;

namespace LootBox
{
    public class Reel : MonoBehaviourExtBind
    {
        [SerializeField]
        private float _speedMax = 500f;

        private float _speedCurrent;

        private RectTransform _wheelColumn;

        [OnAwake]
        private void WheelAwake()
        {
            _wheelColumn = GetComponent<RectTransform>();
            _wheelColumn.anchoredPosition = new Vector2(_wheelColumn.anchoredPosition.x,
                _wheelColumn.sizeDelta.y / 2f);
            _speedCurrent = 0f;
        }

        [OnUpdate]
        private void SpinStep()
        {
            transform.Translate(Vector2.down * Time.deltaTime * _speedCurrent);
            if (_wheelColumn.anchoredPosition.y <= 0f)
            {
                _wheelColumn.anchoredPosition = new Vector2(_wheelColumn.anchoredPosition.x,
                    _wheelColumn.sizeDelta.y / 2f);
            }
        }

        [Bind("OnSpinStarted")]
        private void SpinStart()
        {
            Path = CPath.Create();
            Path.EasingCubicEaseIn(0.5f, 0f, _speedMax, (speed) => {
                Debug.Log($"OnSpinStarted: speed = {speed}");
                _speedCurrent = speed;
            });
        }

        [Bind("OnSpinStopping")]
        private void SpinStopping()
        {
            Path = CPath.Create();
            Path.EasingQuadEaseInOut(3f, _speedMax, 0f, (speed) => {
                Debug.Log($"OnSpinStopped: speed = {speed}");
                _speedCurrent = speed;
            }).Action(() => Settings.Fsm.Invoke("OnSpinStopped"));
        }

        [Bind("Test")]
        public void DoSmth()
        {
            Debug.Log("Lesha Shuboff iz da best");
        }
    }
}