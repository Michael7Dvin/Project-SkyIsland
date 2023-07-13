namespace Infrastructure.Progress.Handling
{
    public interface IProgressHandler
    {
        void WriteValuesToProgress();
        void SetValuesFromProgress();
    }
}