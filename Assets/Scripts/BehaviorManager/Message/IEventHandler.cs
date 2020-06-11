public interface IEventHandler
{
    void Handle(MessageType type, IEvent arg);
}
