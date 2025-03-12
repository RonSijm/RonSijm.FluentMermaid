using FluentMermaid.Enums;
using FluentMermaid.Flowchart;
using FluentMermaid.Flowchart.Enum;
using FluentMermaid.Flowchart.Interfaces;

namespace FluentMermaid.Wrappers;

public class FluentFlowChart
{
    public static FluentFlowChart Create(Orientation orientation)
    {
        var flowChart = FlowChart.Create(orientation);
        return new FluentFlowChart(flowChart);
    }

    private readonly IFlowChart _flowChart;
    private readonly IGraph _graph;
    public INode Node { get; }

    public bool IsSubGraph { get; set; }

    public FluentFlowChart(IFlowChart flowChart, ISubGraph subGraph = null)
    {
        _flowChart = flowChart;

        if (subGraph == null)
        {
            _graph = flowChart;
        }
        else
        {
            _graph = subGraph;
            Node = subGraph;
            IsSubGraph = true;
        }
    }

    public void AddDictionary(Dictionary<string, string> keyValues, FluentNode sourceNode, string keyLinkName, Shape keyShape, Link keyLinkShape, string valueLinkName, Shape valueShape, Link valueLinkShape)
    {
        foreach (var dictBodyValues in keyValues)
        {
            var keyNode = sourceNode.TextNode(dictBodyValues.Key, keyShape, keyLinkShape, keyLinkName);
            keyNode.TextNode(dictBodyValues.Value, valueShape, valueLinkShape, valueLinkName);
        }
    }

    public FluentNode ObsidianNode(string content, Shape shape)
    {
        var node = TextNode(content, shape);
        _flowChart.Styling.SetClass(node.Node, "internal-link");

        return node;
    }

    #region Redirects
    public FluentNode TextNode(string content, Shape shape, string className = null)
    {
        var node = _graph.TextNode(content, shape);

        if (className != null)
        {
            _flowChart.Styling.SetClass(node, "step");
        }

        return new FluentNode(node, this);
    }

    public FluentFlowChart SubGraph(string title, Orientation orientation)
    {
        var subGraphNode = _graph.SubGraph(title, orientation);
        var wrapper = new FluentFlowChart(_flowChart, subGraphNode);

        return wrapper;
    }

    public void Link(INode from, INode to, Link link, string text, int length = 1)
    {
        _graph.Link(from, to, link, text, length);
    }

    public string Render()
    {
        return _flowChart.Render();
    }

    #endregion
}