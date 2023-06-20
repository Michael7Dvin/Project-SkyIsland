using UI.Windows.Base.Window;

namespace UI.Windows.Implementations.MainMenu
{
    public class MainMenuWindow : BaseWindow
    {
        private readonly MainMenuWindowView _view;
        private readonly MainMenuWindowLogic _logic;

        public MainMenuWindow(MainMenuWindowView view, MainMenuWindowLogic logic) : base(view)
        {
            _view = view;
            _logic = logic;
        }

        public override WindowType Type => WindowType.MainMenu;
        public override int MaxOpenedInstances => 1;

        protected override void SubscribeLogic()
        {
            _view.PlayButtonClicked += _logic.OpenSaveSlotSelection;
            _view.OptionsButtonClicked += _logic.OpenOptions;
            _view.QuitButtonClicked += _logic.Quit;
        }

        protected override void UnSubscribeLogic()
        {
            _view.PlayButtonClicked -= _logic.OpenSaveSlotSelection;
            _view.OptionsButtonClicked -= _logic.OpenOptions;
            _view.QuitButtonClicked -= _logic.Quit;
        }
    }
}