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

        [Bind(Names.Events.ON_REEL_STOPPED)]
        private void OnReelStopped()
        {
            Dictionary<Reel, SlotPicture> winners = Settings.Model.Get<Dictionary<Reel, SlotPicture>>(Names.ModelFields.WINNERS);
            if (winners.Count == _reels.Length)
            {
                Settings.Fsm.Invoke(Names.Events.ON_SPIN_STOPPED);
            }
        }
    }
}