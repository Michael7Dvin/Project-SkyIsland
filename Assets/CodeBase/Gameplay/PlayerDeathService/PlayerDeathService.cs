using Gameplay.Dying;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;

namespace Gameplay.PlayerDeathService
{
    public class PlayerDeathService : IPlayerDeathService
    {
        private IDeath _playerDeath;
        private readonly IGameStateMachine _gameStateMachine;

        public PlayerDeathService(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize(IDeath playerDeath)
        {
            _playerDeath = playerDeath;
            _playerDeath.Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _playerDeath.Died -= OnPlayerDied;
            _playerDeath = null;

            _gameStateMachine.EnterState<MainMenuState>();
        }
    }
}