using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LootBox
{
    public class Slot : MonoBehaviourExtBind
    {
        [field: SerializeField]
        public SlotPicture PictureName { get; private set; }

        [SerializeField]
        private Image _blurredPicture;

        [SerializeField]
        private Image _clearPicture;

        [SerializeField]
        private Image _frame;

        private Reel _reel;

        [OnAwake]
        private void SlotAwake()
        {
            _blurredPicture.gameObject.SetActive(false);
            _clearPicture.gameObject.SetActive(true);
            _frame.gameObject.SetActive(false);
            _reel = GetComponentInParent<Reel>();
        }

        [Bind(Names.Events.ON_BLUR_PICTURE)]
        private void OnBlurPicture(bool isBlurred)
        {
            _blurredPicture.gameObject.SetActive(isBlurred);
            _clearPicture.gameObject.SetActive(!isBlurred);
        }

        [Bind(Names.Events.ON_SPIN_STARTED)]
        private void OnReelSpinStarted()
        {
            _frame.gameObject.SetActive(false);
        }

        [Bind(Names.Events.ON_SPIN_STOPPED)]
        private void OnReelSpinStopped()
        {
            Dictionary<Reel, SlotPicture> winners = Settings.Model.Get<Dictionary<Reel, SlotPicture>>(Names.ModelFields.WINNERS);
            SlotPicture slotPicture;
            if (winners.TryGetValue(_reel, out slotPicture))
            {
                _frame.gameObject.SetActive(PictureName == slotPicture);
            }
        }
    }
    
    public enum SlotPicture
    {
        None = 0,
        Helmet = 1,
        Pickaxe = 2,
        Gnome = 3,
        Shoes = 4,
        Anchor = 5,
    }
}