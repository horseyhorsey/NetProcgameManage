using Prism.Events;

namespace Horsesoft.Procgame.Manager.Base.Events
{
    public class GetGameConfigEvent : PubSubEvent<object> { }

    public class SendGameConfigEvent : PubSubEvent<object> { }
    
}
