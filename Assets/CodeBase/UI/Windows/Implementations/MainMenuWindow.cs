using Infrastructure.GameFSM.States;
using UI.Services.Mediating;
using UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Implementations
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _quitButton;

        private IMediator _mediator;

        public void Construct(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override WindowType Type => WindowType.MainMenu;

        protected override void SubscribeOnButtons()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }

        protected override void UnsubscribeFromButtons()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }

        public void OnPlayButtonClick() =>
            _mediator.OpenUIWindow(WindowType.SaveSelection);

        public void OnOptionsButtonClick()
        {
        }

        public void OnQuitButtonClick() =>
            _mediator.EnterGameState<QuitState>();
    }
}