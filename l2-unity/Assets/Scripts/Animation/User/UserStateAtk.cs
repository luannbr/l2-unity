using UnityEngine;

public class UserStateAtk : UserStateBase {
    private float _lastNormalizedTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        LoadComponents(animator);
        if (!_enabled) {
            return;
        }

        AnimatorClipInfo[] clipInfos = animator.GetNextAnimatorClipInfo(0);
        if (clipInfos == null || clipInfos.Length == 0) {
            clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        }

        _networkAnimationController.UpdateAnimatorAtkSpdMultiplier(clipInfos[0].clip.length);

        SetBool("wait", true, false);
        SetBool("atkwait", true, false);
        SetBool("atk01", true, false);

        PlaySoundAtRatio(CharacterSoundEvent.Atk_1H, _audioHandler.AtkRatio);
        PlaySoundAtRatio(ItemSoundEvent.sword_small, _audioHandler.SwishRatio);

        _lastNormalizedTime = 0;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!_enabled) {
            return;
        }

        SetBool("atk01", true, false);
        if ((stateInfo.normalizedTime - _lastNormalizedTime) >= 1f) {
            _lastNormalizedTime = stateInfo.normalizedTime;
            PlaySoundAtRatio(CharacterSoundEvent.Atk_1H, _audioHandler.AtkRatio);
            PlaySoundAtRatio(ItemSoundEvent.sword_small, _audioHandler.SwishRatio);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}