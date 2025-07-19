using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace LootBox
{
    [State(Names.FsmStates.STOP)]
    public class Stop : FSMState
    {
        [Enter]
        public void Enter()
        {
            Settings.Model.EventManager.Invoke(Names.Events.ON_SPIN_STOPPING);
        }

        [Exit]
        public void Exit()
        {
            Settings.Model.EventManager.Invoke(Names.Events.ON_SPIN_STOPPED);
        }

        [Bind(Names.Events.ON_SPIN_STOPPED)]
        private void SpinStopped()
        {
            Parent.Change(Names.FsmStates.IDLE);
        }
    }
}