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
            return _commands.GetCommandList();
        }

        public Library.BookPreview[] Books(string[] tags)
        {
            return _library.Books(tags);
        }

        public ReadOne()
        {
            _dispatcher = new MessageDispatcher(new InMemoryEventStore());

            _dispatcher.ScanInstance(new Book());

            _library = new Library();
            _dispatcher.ScanInstance(_library);
            _commands = new Language();
            _dispatcher.ScanInstance(_commands);
        }

        private readonly MessageDispatcher _dispatcher;
        private readonly Library _library;
        private readonly Language _commands;
    }
}