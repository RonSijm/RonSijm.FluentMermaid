using System.Text;
using FluentMermaid.ClassDiagram.Extensions;
using FluentMermaid.ClassDiagram.Interfaces.ClassMembers;

namespace FluentMermaid.ClassDiagram.Nodes;

public class TypeName : ITypeName
{
    public TypeName(string name, string genericType = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name should not be null or empty", nameof(name));
        }
        
        Name = name;
        GenericType = genericType;
    }

    public TypeName(string name, TypeName genericType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name should not be null or empty", nameof(name));
        }

        Name = name;
        GenericType = $"{genericType.GenericType}~{genericType.Name}~";
    }

    public TypeName(string name, TypeName[] genericType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name should not be null or empty", nameof(name));
        }

        Name = name;

        foreach (var typeName in genericType)
        {
            if (typeName.GenericType == null)
            {
                GenericType = typeName.Name;
            }
            else
            {
                GenericType += $"{typeName.Name}~{typeName.GenericType}~";
            }
        }
    }

    public string Name { get; }
    
    public string GenericType { get; }

    public void RenderTo(StringBuilder builder)
    {
        builder.AppendValidName(Name);
        if (!string.IsNullOrWhiteSpace(GenericType))
        {
            builder
                .Append('~')
                .AppendValidName(GenericType)
                .Append('~');
        }
    }

    public override string ToString()
    {
        if (GenericType == null)
        {
            return Name;
        }
        else
        {
            return $"{GenericType}~{Name}~";
        }
    }
}