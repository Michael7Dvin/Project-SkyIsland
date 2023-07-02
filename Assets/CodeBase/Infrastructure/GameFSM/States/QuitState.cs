using Common.FSM;
using Infrastructure.Services.Input;
using Infrastructure.Services.ResourcesLoading;
using UnityEditor;

namespace Infrastructure.GameFSM.States
{
    public class QuitState : IState
    {
        private readonly IInputService _inputService;
        private readonly IAddressablesLoader _addressablesLoader;

        public QuitState(IInputService inputService, IAddressablesLoader addressablesLoader)
        {
            _inputService = inputService;
            _addressablesLoader = addressablesLoader;
        }

        public void Enter()
        {
            CleanUp();
            CloseApp();
        }

        public void Exit()
        {
        }

        private void CleanUp()
        {
            _inputService.Dispose();
            _addressablesLoader.ClearCache();
        }

        private void CloseApp()
        {
            
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}