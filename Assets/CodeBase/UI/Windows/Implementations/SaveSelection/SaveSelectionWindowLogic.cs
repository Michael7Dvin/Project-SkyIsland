using Infrastructure.GameFSM.States;
using Infrastructure.Services.LevelLoading;
using Infrastructure.Services.LevelLoading.Data;
using UI.Services.Mediating;

namespace UI.Windows.Implementations.SaveSelection
{
    public class SaveSelectionWindowLogic
    {
        private readonly LevelData _replaceWithProgressServiceNewGameData = new(LevelType.Island, "Island");
        private readonly IMediator _mediator;

        public SaveSelectionWindowLogic(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void StartNewGame() => 
            _mediator.EnterGameState<LoadLevelState, LevelData>(_replaceWithProgressServiceNewGameData);
    }
}