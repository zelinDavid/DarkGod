using UnityEngine;

public class HitState : IState {
    public void Enter(EntityBase entity, params object[] args) {
        entity.RemoveSkillCB();
    }

    public void Exit(EntityBase entity, params object[] args) {

    }

    /*
    动画
    声音
    动画结束回调
    伤害计算.

     */
    public void Process(EntityBase entity, params object[] args) {
        entity.SetAction(Constant.ActionHit);
        if (entity.entityType == EntityType.Player) {
            AudioSource source = entity.GetPlayerAudioSource();
            AudioSvc.Instance.PlayCharAudio(Constant.AssassinHit, source);
        }

        TimeSvc.Instance.AddTimeTask((deltaTime) => {
            entity.SetAction(Constant.ActionDefault);
            entity.Idle();
        }, GetHitAniLength(entity) * 1000);
    }

    private float GetHitAniLength(EntityBase entity) {
        AnimationClip[] clips = entity.GetClips();
        foreach (var clip in clips) {
            if (clip.name.Contains("hit") || clip.name.Contains("HIT")) {
                return clip.length;
            }
        }
        Debug.LogError("dontGetHitAni");
        return 0;
    }
}