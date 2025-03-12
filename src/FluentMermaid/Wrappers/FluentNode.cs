using System.Text;
using FluentMermaid.Flowchart.Enum;

namespace FluentMermaid.Wrappers;

public class FluentNode(INode innerNode, FluentFlowChart parent)
{
    public INode Node => innerNode;
    public FluentFlowChart Graph => parent;

    public void RenderTo(StringBuilder target)
    {
        innerNode.RenderTo(target);
    }

    public string Id => innerNode.Id;

    public void LinkTo(FluentNode childNode, Link statusArrow, string text)
    {
        parent.Link(innerNode, childNode.Node, statusArrow, text);
    }

    public FluentNode LinkFrom(FluentNode childNode, Link statusArrow, string text)
    {
        parent.Link(childNode.Node, innerNode, statusArrow, text);

        return this;
    }

    public FluentNode LinkFrom(Link statusArrow, string text, params FluentNode[] childNodes)
    {
        if (childNodes == null || childNodes.Length == 0)
        {
            throw new ArgumentException($"parameter '{nameof(childNodes)}' expects at least 1 item.", nameof(childNodes));
        }

        foreach (var childNode in childNodes)
        {
            parent.Link(childNode.Node, innerNode, statusArrow, text);
        }

        return this;
    }

    public FluentNode TextNode(string content, Shape shape, Link link, string sourceUrlLinkName)
    {
        var node = parent.TextNode(content, shape);
        parent.Link(innerNode, node.Node, link, sourceUrlLinkName);

        return node;
    }

    /// <summary>
    /// Creates a node and adds a link class, to use in obsidian.
    /// </summary>
    /// <returns></returns>
    public FluentNode ObsidianNode(string content, Shape shape, Link link, string sourceUrlLinkName)
    {
        var node = parent.ObsidianNode(content, shape);
        parent.Link(innerNode, node.Node, link, sourceUrlLinkName);

        return node;
    }
}