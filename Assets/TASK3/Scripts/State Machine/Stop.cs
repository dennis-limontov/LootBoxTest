using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace LootBox
{
    [State("Stop")]
    public class Stop : FSMState
    {
        [Enter]
        public void Enter()
        {
            Settings.Model.EventManager.Invoke("OnSpinStopping");
        }

        [Exit]
        public void Exit()
        {
            Settings.Model.EventManager.Invoke("OnSpinStopped");
        }

        [Bind("OnSpinStopped")]
        private void SpinStopped()
        {
            Parent.Change("Idle");
        }
    }
}