using UI.Windows.Base.Window;

namespace UI.Windows.Implementations.SaveSelection
{
    public class SaveSelectionWindow : BaseWindow
    {
        private readonly SaveSelectionWindowView _view;
        private readonly SaveSelectionWindowLogic _logic;

        public SaveSelectionWindow(SaveSelectionWindowView view, SaveSelectionWindowLogic logic) : base(view)
        {
            _view = view;
            _logic = logic;
        }

        public override WindowType Type => WindowType.SaveSelection;
        public override int MaxOpenedInstances => 1;

        protected override void SubscribeLogic()
        {
            _view.CloseButtonClicked += Close;
            _view.SaveSlotButtonClicked += _logic.StartGame;
        }

        protected override void UnSubscribeLogic()
        {
            _view.CloseButtonClicked -= Close;
            _view.SaveSlotButtonClicked -= _logic.StartGame;
        }
    }
}