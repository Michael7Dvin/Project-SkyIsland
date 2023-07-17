﻿using UI.Windows.Base.Window;

namespace UI.Windows.Implementations.DeathWindow
{
    public class DeathWindow : BaseWindow
    {
        private readonly DeathWindowView _view;
        private readonly DeathWindowLogic _logic;

        public DeathWindow(DeathWindowView view, DeathWindowLogic logic) : base(view)
        {
            _view = view;
            _logic = logic;
        }

        public override WindowType Type => WindowType.Death;
        public override int MaxOpenedInstances => 1;

        protected override void SubscribeLogic()
        {
            _view.RespawnButtonClicked += _logic.LoadLastSavedProgress;
            _view.MainMenuButtonClicked += _logic.ReturnToMainMenu;
        }

        protected override void UnSubscribeLogic()
        {
            _view.RespawnButtonClicked -= _logic.LoadLastSavedProgress;
            _view.MainMenuButtonClicked -= _logic.ReturnToMainMenu;
        }
    }
}