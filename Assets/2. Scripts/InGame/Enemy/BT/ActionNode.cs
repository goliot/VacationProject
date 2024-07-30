using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class ActionNode : INode
{
    Func<INode.NodeState> onUpdate = null;

    public ActionNode(Func<INode.NodeState> onUpdate)
    {
        this.onUpdate = onUpdate;
    }

    public INode.NodeState Evaluate() =>
        onUpdate?.Invoke() ?? INode.NodeState.Failure;
}

