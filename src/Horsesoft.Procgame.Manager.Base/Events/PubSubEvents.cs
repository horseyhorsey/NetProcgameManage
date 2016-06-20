using Prism.Events;

namespace Horsesoft.Procgame.Manager.Base.Events
{
    public class MainMenuSelectedEvent : PubSubEvent<string>
    {

    }

    public class CloseCustomDialogAnimsEvent : PubSubEvent<string> { }
}
