using UI.Windows.Base.Window;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindow : BaseWindow
    {
        private readonly PauseWindowView _view;
        private readonly PauseWindowLogic _logic;

        public PauseWindow(PauseWindowView view, PauseWindowLogic logic) : base(view)
        {
            _view = view;
            _logic = logic;
        }

        public override WindowType Type => WindowType.Pause;
        public override int MaxOpenedInstances => 1;

        protected override void SubscribeLogic()
        {
            _view.CloseButtonClicked += Close;
            _view.OptionsButtonClicked += _logic.OpenOptions;
            _view.SaveButtonClicked += _logic.SaveProgess;
            _view.MainMenuButtonClicked += _logic.ReturnToMainMenu;
        }

        protected override void UnSubscribeLogic()
        {
            _view.CloseButtonClicked -= Close;
            _view.OptionsButtonClicked -= _logic.OpenOptions;
            _view.SaveButtonClicked -= _logic.SaveProgess;
            _view.MainMenuButtonClicked -= _logic.ReturnToMainMenu;
        }
    }
}