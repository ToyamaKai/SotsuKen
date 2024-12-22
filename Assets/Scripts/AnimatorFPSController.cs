using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorFPSController : MonoBehaviour
{
    [SerializeField, Range(1, 60)]
    int _fps = 30;

    Animator _animator;

    /// <summary>�������l����</summary>
    float _thresholdTime;

    /// <summary>�X�L�b�v���ꂽ�X�V����</summary>
    float _skippedTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        InitializeThresholdTime();
    }

    /// <summary>
    /// �������l���Ԃ̏�����
    /// </summary>
    void InitializeThresholdTime()
    {
        _thresholdTime = 1f / _fps;
    }

    private void Update()
    {
        _skippedTime += Time.deltaTime;

        if (_thresholdTime > _skippedTime)
        {
            return;
        }

        // �A�j���[�V�����̎��Ԃ��v�Z����
        _animator.Update(_skippedTime);
        _skippedTime = 0f;
    }

    /// <summary>
    /// Inspector�̒l�ύX���̏���
    /// </summary>
    private void OnValidate()
    {
        InitializeThresholdTime();
    }
}
