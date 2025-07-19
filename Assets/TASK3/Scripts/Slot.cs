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
        private Image _frame;

        private Reel _reel;

        [OnAwake]
        private void SlotAwake()
        {
            _frame.gameObject.SetActive(false);
            _reel = GetComponentInParent<Reel>();
        }

        [Bind("OnSpinStarted")]
        private void OnReelSpinStarted()
        {
            _frame.gameObject.SetActive(false);
        }

        [Bind("OnSpinStopped")]
        private void OnReelSpinStopped()
        {
            Dictionary<Reel, SlotPicture> winners = Settings.Model.Get<Dictionary<Reel, SlotPicture>>("Winners");
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