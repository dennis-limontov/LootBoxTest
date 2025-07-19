using AxGrid.FSM;
using AxGrid.Model;
using System.Collections.Generic;

namespace LootBox
{
    [State("Idle")]
    public class Idle : FSMState
    {
        public string START_BUTTON_NAME = "Start";

        [Enter]
        public void Enter()
        {
            Model.Set("IsReadyToStart", true);
            Model.Set("IsReadyToStop", false);
            //Settings.Model.EventManager.Invoke("Test");
        }

        [Exit]
        public void Exit()
        {
            Model.Set("IsReadyToStart", false);
            Model.Set("Winners", new Dictionary<Reel, SlotPicture>());
        }

        [Bind("OnBtn")]
        public void OnStartClick(string buttonName)
        {
            if (buttonName == START_BUTTON_NAME) Parent.Change("Spin");
        }
    }
}