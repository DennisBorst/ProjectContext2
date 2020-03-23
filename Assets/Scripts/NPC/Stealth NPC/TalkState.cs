using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkState : State
{
    private float damping = 5f;
    private Vector3 lookPos;
    private Quaternion oldRotation;
    public TalkState(StateEnum id)
    {
        this.id = id;
    }
    public override void OnEnter(BlackBoard blackBoard)
    {
        base.OnEnter(blackBoard);
        oldRotation = blackBoard.npcStealth.npcObject.transform.rotation;
        blackBoard.npcStealth.dialogueUI.text = "";
        blackBoard.npcStealth.dialogueCanvas.SetActive(true);
        blackBoard.npcStealth.dialogueUI.text = blackBoard.npcStealth.textPlayer;
    }
    public override void OnExit()
    {
        blackBoard.npcStealth.talkingPlayer.animationPlaying = false;
        blackBoard.npcStealth.talkingPlayer = null;
        blackBoard.npcStealth.dialogueCanvas.SetActive(false);
        blackBoard.npcStealth.npcObject.transform.rotation = oldRotation;

    }
    public override void OnUpdate()
    {
        Vector3 lookPos = blackBoard.npcStealth.talkingPlayer.transform.position - blackBoard.npcStealth.npcObject.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        float eulerY = lookRot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
        blackBoard.npcStealth.npcObject.transform.rotation = Quaternion.Slerp(blackBoard.npcStealth.npcObject.transform.rotation, rotation, Time.deltaTime * damping);

        if (blackBoard.npcStealth.talkingPlayer.deinteract)
        {
            blackBoard.npcStealth.talkingPlayer.animationPlaying = false;
            fsm.SwitchState(StateEnum.Idle);
        }
        else
        {
            blackBoard.npcStealth.talkingPlayer.animationPlaying = true;
            blackBoard.npcStealth.talkingPlayer.SetAnimation("isWalking", false);
        }
    }
}
