public class ActionNode : DecisionTreeNode
{
    public System.Action action;

    public ActionNode(System.Action action)
    {
        this.action = action;
    }

    public override DecisionTreeNode MakeDecision()
    {
        action?.Invoke();
        return this;
    }
}