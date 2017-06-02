namespace ReadOne.Application
{
    public class ReadOne
    {
        public void Do(ICommand command)
        {
            _dispatcher.SendCommand(command);
        }

        public ReadOne()
        {
            _dispatcher = new MessageDispatcher(new InMemoryEventStore());

            _dispatcher.ScanInstance(new Book());
        }

        private readonly MessageDispatcher _dispatcher;
    }
}