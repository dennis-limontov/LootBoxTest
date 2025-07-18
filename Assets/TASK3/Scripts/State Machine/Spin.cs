using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace LootBox
{
    [State("Spin")]
    public class Spin : FSMState
    {
        public string STOP_BUTTON_NAME = "Stop";

        [Enter]
        public void Enter()
        {
            Settings.Model.EventManager.Invoke("OnSpinStarted");
        }

        [Exit]
        public void Exit()
        {
            Model.Set("IsReadyToStop", false);
        }

        [One(3f)]
        public void MakeAvailableToStop()
        {
            Model.Set("IsReadyToStop", true);
        }

        [Bind("OnBtn")]
        public void OnStopClick(string buttonName)
        {
            if (buttonName == STOP_BUTTON_NAME) Parent.Change("Stop");
        }
    }
}