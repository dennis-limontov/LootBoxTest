using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

namespace LootBox
{
    public class States : MonoBehaviourExt
    {
        [OnAwake]
        private void CreateFsm()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new Idle());
            Settings.Fsm.Add(new Spin());
            Settings.Fsm.Add(new Stop());
        }

        [OnStart]
        private void StartFsm()
        {
            Settings.Fsm.Start(Names.FsmStates.IDLE);
        }

        [OnUpdate]
        public void UpdateFsm()
        {
            Settings.Fsm.Update(Time.deltaTime);
        }
    }
}