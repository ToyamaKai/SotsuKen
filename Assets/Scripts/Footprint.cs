using UnityEngine;
using System.Collections;

public class Footprint : MonoBehaviour
{
    /// <summary>
    /// 雪パーティクル
    /// </summary>
    public ParticleSystem SnowParticle;

    /// <summary>
    /// 雪用マテリアル
    /// </summary>
    Material SnowMaterial;

    /// <summary>
    /// ディスプレイスメントマップ用テクスチャー
    /// </summary>
    Texture2D DisplacementTexture;

    /// <summary>
    /// ディスプレイスメントマップ用テクスチャー更新フラグ
    /// </summary>
    bool IsUpdateDisplacementTexture;

    /// <summary>
    /// 積雪パワー
    /// </summary>
    public float AccumulatePower = 0.02f;

    /// <summary>
    /// 積雪の最大値(0〜1)
    /// </summary>
    public float AccumulateLimit = 0.5f;

    /// <summary>
    /// パーティクルイベント取得用
    /// </summary>
    ParticleCollisionEvent[] _collisionEvents;

    void Start()
    {
        // 使用するマテリアルを取得
        MeshRenderer render = gameObject.GetComponent<MeshRenderer>();
        SnowMaterial = render.material;

        // ディスプレイスメントマップ用テクスチャー
        DisplacementTexture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        for (int y = 0; y < DisplacementTexture.height; y++)
        {
            for (int x = 0; x < DisplacementTexture.width; x++)
            {
                DisplacementTexture.SetPixel(x, y, new Color(0, 0, 0));
            }
        }

        SnowMaterial.SetTexture("_DispTex", DisplacementTexture);
    }

    /// <summary>
    /// パーティクルの当たり判定イベント
    /// </summary>
    /// <param name="other"></param>
    void OnParticleCollision(GameObject other)
    {
        if (DisplacementTexture == null) return;

        int safeLength = SnowParticle.GetSafeCollisionEventSize();
        if (_collisionEvents == null || _collisionEvents.Length < safeLength)
        {
            _collisionEvents = new ParticleCollisionEvent[safeLength];
        }
        IsUpdateDisplacementTexture = false;
        int numCollisionEvents = SnowParticle.GetCollisionEvents(gameObject, _collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            AccumulateSnow(_collisionEvents[i].intersection);
            i++;
        }

        // テクスチャー更新
        if (IsUpdateDisplacementTexture)
        {
            //Debug.Log("Update");
            DisplacementTexture.Apply();
            SnowMaterial.SetTexture("_DispTex", DisplacementTexture);
        }
    }

    /// <summary>
    /// 指定位置に雪を積もらせます
    /// </summary>
    /// <param name="position"></param>
    void AccumulateSnow(Vector3 position)
    {
        // 位置からヒットした場所のテクスチャーUV値の取得方法がわからないので
        // Rayを飛ばしてRaycastHitの中のテクスチャーUV値を使用することにします。
        Ray ray = new Ray(new Vector3(position.x, position.y + Vector3.up.y * 1, position.z), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2) != true)
        {
            return;   // 地面が見つからず?
        }
        // Debug.LogFormat("tex = {0}, {1}", hit.textureCoord.x, hit.textureCoord.y);

        var tx = (int)(hit.textureCoord.x * DisplacementTexture.width);
        var ty = (int)(hit.textureCoord.y * DisplacementTexture.height);
        AccumulateSnowAdd(tx, ty, AccumulatePower);
        AccumulateSnowAdd(tx + 1, ty, AccumulatePower / 2.0f);
        AccumulateSnowAdd(tx, ty + 1, AccumulatePower / 2.0f);
        AccumulateSnowAdd(tx - 1, ty, AccumulatePower / 2.0f);
        AccumulateSnowAdd(tx, ty - 1, AccumulatePower / 2.0f);
        AccumulateSnowAdd(tx + 1, ty + 1, AccumulatePower / 4.0f);
        AccumulateSnowAdd(tx - 1, ty + 1, AccumulatePower / 4.0f);
        AccumulateSnowAdd(tx + 1, ty - 1, AccumulatePower / 4.0f);
        AccumulateSnowAdd(tx - 1, ty - 1, AccumulatePower / 4.0f);
        IsUpdateDisplacementTexture = true;
    }

    /// <summary>
    /// 指定位置に雪を積もらせます
    /// </summary>
    /// <param name="texX"></param>
    /// <param name="texY"></param>
    /// <param name="power"></param>
    void AccumulateSnowAdd(int texX, int texY, float power)
    {
        if (texX < 0 || texX >= DisplacementTexture.width) return;
        if (texY < 0 || texY >= DisplacementTexture.height) return;
        var val = DisplacementTexture.GetPixel(texX, texY);
        var dis = Mathf.Min(AccumulateLimit, val.r + power);
        DisplacementTexture.SetPixel(texX, texY, new Color(dis, dis, dis));
    }
}