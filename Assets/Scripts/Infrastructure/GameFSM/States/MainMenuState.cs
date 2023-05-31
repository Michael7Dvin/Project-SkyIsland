﻿using Common.FSM;
using Infrastructure.Services;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.UtilityDataProviding;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUtilityDataProvider _utilityDataProvider;
        
        public MainMenuState(IGameStateMachine gameStateMachine, IUtilityDataProvider utilityDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _utilityDataProvider = utilityDataProvider;
        }

        public void Enter()
        {
            StartGame();
        }

        public void Exit()
        {
        }

        private void StartGame()
        {
            SceneLoadRequest request = new(_utilityDataProvider.ScenesInfo.IslandSceneName, OnLevelLoaded);
            _gameStateMachine.EnterState<LoadSceneState, SceneLoadRequest>(request);
        }
        
        public void OnLevelLoaded()
        {
            _gameStateMachine.EnterState<GameplayState>();
        }
    }
}