using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    [Header("Range")]
    [SerializeField]
    float _detectRange = 10f;
    [SerializeField]
    float _meleeAttackRange = 5f;

    [Header("Movement")]
    [SerializeField]
    float _movementSpeed = 10f;

    Vector3 _originPos = default;
    BehaviorTreeRunner _BTRunner = null;
    Transform _detectedPlayer = null;
    Animator _animator = null;

    const string _ATTACK_ANIM_STATE_NAME = "Unarmed-Attack-L1";
    const string _ATTACK_ANIM_TIRGGER_NAME = "OnAttack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _BTRunner = new BehaviorTreeRunner(SettingBT());

        _originPos = transform.position;
    }

    private void Update()
    {
        _BTRunner.Operate();
        Debug.Log(_BTRunner.rootNode);
    }

    INode SettingBT()
    {
        return new SelectorNode
            (
                new List<INode>()
                {
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckMeleeAttacking),
                            new ActionNode(CheckEnemyWithinMeleeAttackRange),
                            new ActionNode(DoMeleeAttack),
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectEnemy),
                            new ActionNode(MoveToDetectEnemy),
                        }
                    ),
                    new ActionNode(MoveToOriginPosition)
                }
            );
    }

    bool IsAnimationRunning(string stateName)
    {
        if (_animator != null)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                var normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                return normalizedTime != 0 && normalizedTime < 1f;
            }
        }

        return false;
    }

    #region Attack Node
    INode.NodeState CheckMeleeAttacking()
    {
        if (IsAnimationRunning(_ATTACK_ANIM_STATE_NAME))
        {
            return INode.NodeState.Running;
        }

        return INode.NodeState.Success;
    }

    INode.NodeState CheckEnemyWithinMeleeAttackRange()
    {
        if (_detectedPlayer != null)
        {
            if (Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_meleeAttackRange * _meleeAttackRange))
            {
                return INode.NodeState.Success;
            }
        }

        return INode.NodeState.Failure;
    }

    INode.NodeState DoMeleeAttack()
    {
        if (_detectedPlayer != null)
        {
            _animator.SetBool("isMove", false);

            _animator.SetTrigger(_ATTACK_ANIM_TIRGGER_NAME);
            return INode.NodeState.Success;
        }

        return INode.NodeState.Failure;
    }
    #endregion

    #region Detect & Move Node
    INode.NodeState CheckDetectEnemy()
    {
        var overlapColliders = Physics.OverlapSphere(transform.position, _detectRange, LayerMask.GetMask("Player"));

        if (overlapColliders != null && overlapColliders.Length > 0)
        {
            _detectedPlayer = overlapColliders[0].transform;
            _animator.SetBool("isMove", false);

            return INode.NodeState.Success;
        }

        _detectedPlayer = null;

        return INode.NodeState.Failure;
    }

    INode.NodeState MoveToDetectEnemy()
    {
        if (_detectedPlayer != null)
        {
            if (Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_meleeAttackRange * _meleeAttackRange))
            {
                return INode.NodeState.Success;
            }

            transform.position = Vector3.MoveTowards(transform.position, _detectedPlayer.position, Time.deltaTime * _movementSpeed);
            _animator.SetBool("isMove", true);

            return INode.NodeState.Running;
        }

        return INode.NodeState.Failure;
    }
    #endregion

    #region  Move Origin Pos Node
    INode.NodeState MoveToOriginPosition()
    {
        if (Vector3.SqrMagnitude(_originPos - transform.position) < float.Epsilon * float.Epsilon)
        {
            return INode.NodeState.Success;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _originPos, Time.deltaTime * _movementSpeed);
            _animator.SetBool("isMove", true);
            return INode.NodeState.Running;
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _detectRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, _meleeAttackRange);
    }
}