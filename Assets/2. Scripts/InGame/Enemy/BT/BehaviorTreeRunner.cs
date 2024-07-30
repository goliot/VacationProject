public class BehaviorTreeRunner
{
    public INode rootNode;

    public BehaviorTreeRunner(INode rootNode)
    {
        this.rootNode = rootNode;
    }

    public void Operate()
    {
        rootNode.Evaluate();
    }
}