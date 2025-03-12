using System.Text;
using FluentMermaid.ClassDiagram.Enums;
using FluentMermaid.ClassDiagram.Interfaces;
using FluentMermaid.ClassDiagram.Interfaces.ClassMembers;
using FluentMermaid.ClassDiagram.Nodes;
using FluentMermaid.Enums;
using FluentMermaid.Extensions;

namespace FluentMermaid.ClassDiagram;

internal class ClassDiagramRoot : IClassDiagram
{
    public List<IClass> Classes { get; } = new();
    public List<IRelation> Relations { get; } = new();
    
    public ClassDiagramRoot(Orientation orientation)
    {
        Orientation = orientation;
    }

    public Orientation Orientation { get; }

    public IClass AddClass(ITypeName typeName, string annotation, string cssClass)
    {
        _ = typeName ?? throw new ArgumentNullException(nameof(typeName));
        
        var @class = new ClassNode(typeName, annotation, cssClass);
        Classes.Add(@class);
        return @class;
    }

    public IRelation Relation(
        IClass from,
        IClass to,
        Relationship? relationshipFrom,
        Cardinality? cardinalityFrom,
        Relationship? relationshipTo,
        Cardinality? cardinalityTo,
        RelationLink relationLink,
        string label)
    {
        var relation = new RelationNode(
            from,
            to,
            relationshipFrom,
            cardinalityFrom,
            relationLink,
            cardinalityTo,
            relationshipTo,
            label);
        Relations.Add(relation);
        return relation;
    }

    public string Render()
    {
        StringBuilder builder = new();
        builder.AppendLine("classDiagram");
        builder.Append("direction ").AppendLine(Orientation.Render());
        
        Relations.ForEach(r => r.RenderTo(builder));
        Classes.ForEach(c => c.RenderTo(builder));

        return builder.ToString();
    }
}