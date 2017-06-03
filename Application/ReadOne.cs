namespace ReadOne.Application
{
    public class ReadOne
    {
        public void Do(ICommand command)
        {
            _dispatcher.SendCommand(command);
        }

        public string[] GetCommandList()
        {
            return new[] { "q","w" };
        }

        public ReadOne()
        {
            _dispatcher = new MessageDispatcher(new InMemoryEventStore());

            _dispatcher.ScanInstance(new Book());

            _library = new Library();
            _dispatcher.ScanInstance(_library);
        }

        private readonly MessageDispatcher _dispatcher;
        private readonly Library _library;
    }
}