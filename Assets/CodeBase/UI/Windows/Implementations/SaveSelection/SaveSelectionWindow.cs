using Cysharp.Threading.Tasks;
using Infrastructure.Progress;
using Infrastructure.Services.SaveLoadService;
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

        protected override async void OnOpened()
        {
            await UpdateSaveSlots();
            _view.CloseButtonClicked += Close;
            _view.SaveSlotButtonClicked += _logic.StartGame;
            _view.DeleteSaveButtonClicked += OnSaveDeleteButtonClicked;
        }

        protected override void OnClosed()
        {
            _view.CloseButtonClicked -= Close;
            _view.SaveSlotButtonClicked -= _logic.StartGame;
            _view.DeleteSaveButtonClicked -= OnSaveDeleteButtonClicked;
        }

        private async void OnSaveDeleteButtonClicked(SaveSlotID saveSlotID)
        {
            _logic.DeleteSaveFile(saveSlotID);
            await UpdateSaveSlots();
        }
        
        private async UniTask UpdateSaveSlots()
        {
            await ReDrawSaveSlot(SaveSlotID.First, _view.SaveSlot1);
            await ReDrawSaveSlot(SaveSlotID.Second, _view.SaveSlot2);
            await ReDrawSaveSlot(SaveSlotID.Third, _view.SaveSlot3);
        }

        private async UniTask ReDrawSaveSlot(SaveSlotID saveSlotID, SaveSlot saveSlot)
        {
            (bool isSuccessful, AllProgress result) firstSlotProgress = await _logic.GetProgress(saveSlotID);
            if (firstSlotProgress.isSuccessful == true)
                saveSlot.DrawWithProgress(firstSlotProgress.result);
            else
                saveSlot.DrawEmpty();
        }
    }
}