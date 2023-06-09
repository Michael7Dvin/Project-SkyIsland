using Infrastructure.Services.AppClosing;
using UI.Services.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _quitButton;
        
        private IWindowFactory _windowFactory;
        private IAppCloser _appCloser;

        public void Construct(IWindowFactory windowFactory, IAppCloser appCloser)
        {
            _windowFactory = windowFactory;
            _appCloser = appCloser;
        }

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
            _windowFactory.CreateSaveSelectionWindow();

        public void OnOptionsButtonClick()
        {
        }
        
        public void OnQuitButtonClick() => 
            _appCloser.Close();
    }
}