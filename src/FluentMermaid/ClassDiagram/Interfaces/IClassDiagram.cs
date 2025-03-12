using FluentMermaid.ClassDiagram.Enums;
using FluentMermaid.ClassDiagram.Interfaces.ClassMembers;

namespace FluentMermaid.ClassDiagram.Interfaces;

public interface IClassDiagram
{
    public List<IClass> Classes { get; }
    public List<IRelation> Relations { get; }

    IClass AddClass(ITypeName typeName, string annotation = null, string cssClass = null);

    IRelation Relation(
        IClass from,
        IClass to,
        Relationship? relationshipFrom,
        Cardinality? cardinalityFrom,
        Relationship? relationshipTo,
        Cardinality? cardinalityTo,
        RelationLink relationLink,
        string label);

    string Render();
}