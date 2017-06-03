namespace Bot.CommandWrapping
{
    public interface ICommandWrapper
    {
        bool IsMatch(string[] prefix);

        string Execute(string[] parameters);

        string HelpMessage { get; }

        string Id { get; }
    }
}