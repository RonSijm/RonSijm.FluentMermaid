using System.Text;
using FluentMermaid.SequenceDiagram.Interfaces;

namespace FluentMermaid.SequenceDiagram.Actions;

internal record OptStart(string Title) : IAction
{
    public void RenderTo(StringBuilder builder)
    {
        builder
            .Append("opt ")
            .AppendLine(Title);
    }

    public string Title { get; } = Title;

    public void Deconstruct(out string title)
    {
        title = Title;
    }
}