namespace UI.Services.Factory
{
    public interface IWindowFactory
    {
        void CreateMainMenuWindow();
        void CreateSaveSelectionWindow();
        void CreatePauseWindow();
        void CreateDeathWindow();
    }
}