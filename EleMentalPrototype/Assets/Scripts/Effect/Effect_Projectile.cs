using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Projectile : Effect
{
    public enum PROJECTILE_TYPE
    {
        STRAIGHT,
        CURVE,
        BEZIER,
        NONE_PROJECTILE,
    }

    [SerializeField] Transform meshObjectTransform_;

    Vector3 startPosition_;
    Transform targetTransform_;

    float flyTime_;
    PROJECTILE_TYPE projectileType_;
    
    Vector3[] bezierPoints_ = new Vector3[4];

    public override void Init(EFFECT_NAME effectName, Vector3 startPosition, Transform targetTransform, float flyTime, PROJECTILE_TYPE projectileType)
    {
        base.Init(effectName, startPosition, targetTransform, flyTime, projectileType);

        startPosition_ = startPosition;
        targetTransform_ = targetTransform;
        flyTime_ = flyTime;
        projectileType_ = projectileType;

        EffectPosition = startPosition_;


        float bezierValue_1 = 0f;
        float bezierValue_2 = 0f;

        if (projectileType_ == PROJECTILE_TYPE.BEZIER)
        {
            bezierValue_1 = 15f;
            bezierValue_2 = 30f;
        }
        else if(projectileType_ == PROJECTILE_TYPE.CURVE)
        {
            bezierValue_1 = 2.0f;
            bezierValue_2 = 4.0f;
        }
        else if(projectileType_ == PROJECTILE_TYPE.NONE_PROJECTILE)
        {
            startPosition_ = targetTransform_.localPosition;
            EffectPosition = startPosition_;
            flyTime_ = 0;
        }

        bezierPoints_[0] = WorldPosition;

        // 시작 지점을 기준으로 랜덤 포인트 지정.
        bezierPoints_[1] = WorldPosition +
            (bezierValue_1 * Random.Range(-1.0f, 1.0f) * transform.right) + // X (좌, 우 전체)
            (bezierValue_1 * Random.Range(0f, 1.0f) * transform.up) + // Y (아래쪽 조금, 위쪽 전체)
            (bezierValue_1 * Random.Range(-1.0f, -0.8f) * transform.forward); // Z (뒤 쪽만)

        // 도착 지점을 기준으로 랜덤 포인트 지정.
        bezierPoints_[2] = targetTransform.position +
            (bezierValue_2 * Random.Range(-1.0f, 1.0f) * targetTransform.right) + // X (좌, 우 전체)
            (bezierValue_2 * Random.Range(0, 2f) * targetTransform.up) + // Y (위, 아래 전체)
            (bezierValue_2 * Random.Range(0.8f, 1.0f) * targetTransform.forward); // Z (앞 쪽만)

        // 도착 지점.
        bezierPoints_[3] = targetTransform.position;
    }

    protected override void EffectUpdate()
    {
        if (projectileType_ == PROJECTILE_TYPE.STRAIGHT)
        {
            float dis = Vector3.Distance(startPosition_, targetTransform_.localPosition);
            EffectPosition = Vector3.MoveTowards(EffectPosition, targetTransform_.localPosition, elapsedTime_ / flyTime_);

            Vector3 diff = targetTransform_.localPosition - startPosition_;
            float angle = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;

            if(meshObjectTransform_ != null)
            {
                meshObjectTransform_.localEulerAngles = new Vector3(0, -angle, 0);
            }

        }
        else if (projectileType_ == PROJECTILE_TYPE.CURVE ||
            projectileType_ == PROJECTILE_TYPE.BEZIER)
        {
            // 베지어 곡선으로 X,Y,Z 좌표 얻기.
            transform.position = new Vector3(
                CubicBezierCurve(bezierPoints_[0].x, bezierPoints_[1].x, bezierPoints_[2].x, bezierPoints_[3].x),
                CubicBezierCurve(bezierPoints_[0].y, bezierPoints_[1].y, bezierPoints_[2].y, bezierPoints_[3].y),
                CubicBezierCurve(bezierPoints_[0].z, bezierPoints_[1].z, bezierPoints_[2].z, bezierPoints_[3].z)
            );
        }
        

        base.EffectUpdate();
    }

    float CubicBezierCurve(float a, float b, float c, float d)
    {
        // a 시작 위치
        // b 시작 위치에서 얼마나 꺾일 지 정하는 위치
        // c 도착 위치에서 얼마나 꺾일 지 정하는 위치
        // d 도착 위치

        float t = elapsedTime_ / flyTime_;
        
        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
}
