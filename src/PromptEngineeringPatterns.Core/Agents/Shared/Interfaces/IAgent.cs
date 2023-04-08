namespace PromptEngineeringPatterns.Core.Agents.Shared.Interfaces;

public interface IAgent<T> where T : notnull
{
    public Task<T> Ask(string message);
}
