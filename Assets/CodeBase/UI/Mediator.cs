using Infrastructure.GameFSM.States;
using UnityEditor;

namespace UI
{
    public class Mediator : IMediator
    {
        private readonly MainMenuState _mainMenuState;

        public Mediator(MainMenuState mainMenuState)
        {
            _mainMenuState = mainMenuState;
        }

        public void StartGame() => 
            _mainMenuState.StartGame();

        public void QuitGame()
        {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}