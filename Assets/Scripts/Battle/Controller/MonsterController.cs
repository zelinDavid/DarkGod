using UnityEngine;

public class MonsterController : Controller {
    private void Update() {
        if (isMove) {
            SetDir();
            SetMove();
        }
    }

    private void SetDir() {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1));
        transform.localEulerAngles = new Vector3(0, angle, 0);
    }

    private void SetMove() {
        ctrl.Move((transform.forward - transform.up) * Constant.MonsterMoveSpeed * Time.deltaTime);
    }

}