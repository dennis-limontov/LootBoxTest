using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using System.Collections.Generic;

namespace LootBox
{
    public class ReelsController : MonoBehaviourExtBind
    {
        private Reel[] _reels;

        [OnAwake]
        private void ReelsAwake()
        {
            _reels = GetComponentsInChildren<Reel>();
        }

        [Bind("OnReelStopped")]
        private void OnReelStopped()
        {
            Dictionary<Reel, SlotPicture> winners = Settings.Model.Get<Dictionary<Reel, SlotPicture>>("Winners");
            if (winners.Count == _reels.Length)
            {
                Settings.Fsm.Invoke("OnSpinStopped");
            }
        }
    }
}