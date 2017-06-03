namespace Bot.CommandWrapping
{
    public interface ICommandManager
    {
        ICommandManager AddCommand(ICommandWrapper commandWrapper);

        string[] ProcessInput(string input);
    }
}