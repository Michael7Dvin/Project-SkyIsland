using Common.FSM;
using Infrastructure.Services.Input;
using Infrastructure.Services.ResourcesLoading;
using UnityEditor;

namespace Infrastructure.GameFSM.States
{
    public class QuitState : IState
    {
        private readonly IInputService _inputService;
        private readonly IResourcesLoader _resourcesLoader;

        public QuitState(IInputService inputService, IResourcesLoader resourcesLoader)
        {
            _inputService = inputService;
            _resourcesLoader = resourcesLoader;
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
            _resourcesLoader.ClearCache();
        }

        private void CloseApp()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}