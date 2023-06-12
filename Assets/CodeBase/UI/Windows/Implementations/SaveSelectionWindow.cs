using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading;
using UI.Services.Mediating;
using UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Implementations
{
    public class SaveSelectionWindow : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private Button _saveSlot1Button;
        [SerializeField] private Button _saveSlot2Button;
        [SerializeField] private Button _saveSlot3Button;

        private readonly LevelData _replaceWithProgressServiceNewGameLevelData = new(LevelType.Island, "Island");
        
        private IMediator _mediator;

        public void Construct(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override WindowType Type => WindowType.SaveSelection;

        protected override void SubscribeOnButtons()
        {
            _closeButton.onClick.AddListener(Close);   
            
            _saveSlot1Button.onClick.AddListener(StartNewGame);
            _saveSlot2Button.onClick.AddListener(StartNewGame);
            _saveSlot3Button.onClick.AddListener(StartNewGame);
        }

        protected override void UnsubscribeFromButtons()
        {
            _closeButton.onClick.RemoveListener(Close);
            
            _saveSlot1Button.onClick.RemoveListener(StartNewGame);
            _saveSlot2Button.onClick.RemoveListener(StartNewGame);
            _saveSlot3Button.onClick.RemoveListener(StartNewGame);
        }

        private void StartNewGame() => 
            _mediator.EnterGameState<LoadLevelState, LevelData>(_replaceWithProgressServiceNewGameLevelData);
    }
}