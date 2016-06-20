namespace Horsesoft.Procgame.Manager.Base.Services
{
    public interface IFlyoutService
    {
        void ShowFlyout(string flyoutName);

        bool CanShowFlyout(string flyoutName);
    }
}
