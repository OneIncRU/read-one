using System.Collections;

namespace ReadOne
{
    public interface IHandleCommand<T> where T : ICommand
    {
        IEnumerable Handle(T c);
    }
}