using System;

namespace ReadOne.Application
{
    public class ReadOne
    {
        public ReadOne()
        {
            _store = new InMemoryEventStore();
            _dispatcher = new MessageDispatcher(_store);

            _dispatcher.ScanInstance(new Book());

            _library = new Library();
            _dispatcher.ScanInstance(_library);
            _commands = new Language();
            _dispatcher.ScanInstance(_commands);
        }

        public void Init()
        {
            Do(new Add
            {
                Id = Guid.NewGuid(),
                Name = "Implementing Domain-Driven Design"
            });

            Do(new Add
            {
                Id = Guid.NewGuid(),
                Name = "Impact Mapping: Making a Big Impact with Software Products and Projects"
            });
        }

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

        private readonly IEventStore _store;
        private readonly MessageDispatcher _dispatcher;
        private readonly Library _library;
        private readonly Language _commands;
    }
}