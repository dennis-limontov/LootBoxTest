using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using System.Collections.Generic;

namespace LootBox
{
    [State(Names.FsmStates.IDLE)]
    public class Idle : FSMState
    {
        public string START_BUTTON_NAME = "Start";

        [Enter]
        public void Enter()
        {
            Model.Set(Names.ModelFields.IS_READY_TO_START, true);
            Model.Set(Names.ModelFields.IS_READY_TO_STOP, false);
            Settings.Model.EventManager.Invoke(Names.Events.TEST); // hello Lesha
        }

        [Exit]
        public void Exit()
        {
            Model.Set(Names.ModelFields.IS_READY_TO_START, false);
            Model.Set(Names.ModelFields.WINNERS, new Dictionary<Reel, SlotPicture>());
        }

        [Bind(Names.Events.ON_BUTTON_CLICKED)]
        public void OnStartClick(string buttonName)
        {
            if (buttonName == START_BUTTON_NAME) Parent.Change(Names.FsmStates.SPIN);
        }
    }
}