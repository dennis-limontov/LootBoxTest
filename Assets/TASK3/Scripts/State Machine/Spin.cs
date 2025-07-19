using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace LootBox
{
    [State(Names.FsmStates.SPIN)]
    public class Spin : FSMState
    {
        public const float STOP_BUTTON_INTERACTABLE_DELAY = 3f;
        public const string STOP_BUTTON_NAME = "Stop";

        [Enter]
        public void Enter()
        {
            Settings.Model.EventManager.Invoke(Names.Events.ON_SPIN_STARTED);
        }

        [Exit]
        public void Exit()
        {
            Model.Set(Names.ModelFields.IS_READY_TO_STOP, false);
        }

        [One(STOP_BUTTON_INTERACTABLE_DELAY)]
        public void MakeAvailableToStop()
        {
            Model.Set(Names.ModelFields.IS_READY_TO_STOP, true);
        }

        [Bind(Names.Events.ON_BUTTON_CLICKED)]
        public void OnStopClick(string buttonName)
        {
            if (buttonName == STOP_BUTTON_NAME) Parent.Change(Names.FsmStates.STOP);
        }
    }
}