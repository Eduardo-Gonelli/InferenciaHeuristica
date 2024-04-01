public class DecisionNode : DecisionTreeNode
{
    public DecisionTreeNode trueNode;
    public DecisionTreeNode falseNode;
    public System.Func<bool> test;

    public DecisionNode(System.Func<bool> test, DecisionTreeNode trueNode, DecisionTreeNode falseNode)
    {
        this.test = test;
        this.trueNode = trueNode;
        this.falseNode = falseNode;
    }

    public override DecisionTreeNode MakeDecision()
    {
        return test() ? trueNode.MakeDecision() : falseNode.MakeDecision();
    }
}