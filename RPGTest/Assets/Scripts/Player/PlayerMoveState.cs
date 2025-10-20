using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveState : PlayerState
{
    private IInteractable _interactable;
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _stateAnimName) : base(_player, _stateMachine, _stateAnimName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        player.StopAllCoroutines();
        _interactable = null;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleClick();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            stateMachine.ChangeState(player.attackState);
        }
    }

    private void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                _interactable = interactable;
                MoveToPosition(_interactable.StoppingPos, _interactable.StoppingDistance);
            }
            else
            {
                MoveToPosition(hit.point, 0f);
            }
        }
    }

    private void MoveToPosition(Vector3 target, float stoppingDistance)
    {
        player.playerAgent.stoppingDistance = stoppingDistance;
        player.playerAgent.SetDestination(target);
        player.StartPlayerCoroutine(WaitArrival());
    }

    private IEnumerator WaitArrival()
    {
        var agent = player.playerAgent;
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        if (_interactable != null && stateMachine.GetCurrentState() == this)
        {
            _interactable.Interact();
            _interactable = null;
        }
    }
}
