using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    None = -1,
    PlayerCam,
    ViewCam,
    TalkCam,
    Event_0_Cam,
    Event_1_Cam,
    Event_2_Cam,
    Event_3_Cam,
    Event_4_Cam,
    Boss_Cam,
}
[System.Serializable]
public struct PlayerCam
{
    public CinemachineVirtualCamera _cam;
    public float _height;
    public float _radius;
}
public class CameraManager : MonoBehaviour
{
    public static CameraManager _instance;

    public CameraType _nowType = CameraType.None;

    [SerializeField] CinemachineBrain _brain;

    [SerializeField] List<CinemachineVirtualCameraBase> _cams;

    public CinemachineVirtualCameraBase _nowCam = null;

    [SerializeField] public CinemachineFreeLook _playerCam;

    public GameObject _playerFocus;

    float wheelValue = 0f; // �������� => ��ġ�� ����� ���� ��
    [SerializeField] float wheelSpd = 2f; // ����Ǵ� ���ǵ�
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {

    }
    private void Update()
    {
        float value = Input.GetAxis("Mouse ScrollWheel"); // ���콺 �� Ȯ�� => ���� ���� �ø��� value = 0.1�� �ǰ�, ���� �Ʒ��� ������ value = -0.1�� �ȴ�.
        if (value != 0) // value�� 0�� �ƴ϶�� => ���� ����Ǿ��ٸ�,
        {
            wheelValue += value; // wheelValue�� value�� �����ش�.
            wheelValue = Mathf.Clamp(wheelValue, -5f, 3f); // wheelValue�� �ּ� -5, �ִ� 3�� ���� ���Ѵ�.

            if (wheelValue >= 3f || wheelValue <= -5f) return; // �ִ�ġ�� �����ϸ� ���� => ���� ������ ����.

            for (int i = 0; i < 3; i++) // �� Rig(Top, Middle, Bottom)�� ���鼭 ����.
            {
                // ����� �ִ� ������ ��ī�̸� ���۷�����, �پ��ϸ� ī�޶� ���������, �ٴٿ��ϸ� ī�޶� �־����Ƿ�, ���� -���ش�.
                _playerCam.m_Orbits[i].m_Height -= value * wheelSpd; // ���̸� value��ŭ -���ش�
                _playerCam.m_Orbits[i].m_Radius -= value * wheelSpd; // �ʺ� value��ŭ -���ش�.
            }
        }
    }
    void FindPlayerFocus()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("CamFocus");
        foreach(GameObject o in obj)
        {
            if(o.activeSelf)
            {
                _playerFocus = o;
                break;
            }
        }
    }
    public void StartTalkCam(GameObject fromObj, GameObject toObj)
    {
        ChangeCam(CameraType.TalkCam);

        Vector3 pos = fromObj.transform.position + (fromObj.transform.forward * -3f) + (fromObj.transform.right * 2f);
        pos.y = 3.5f;

        _nowCam.ForceCameraPosition(pos, Quaternion.identity);
        Vector3 vec = new Vector3(toObj.transform.position.x, toObj.transform.position.y + 1, toObj.transform.position.z);
        _nowCam.transform.rotation = Quaternion.LookRotation(vec - _nowCam.transform.position, Vector3.up);
        //_nowCam.LookAt = toObj.transform;
    }

    public void StartViewCam()
    {
        ChangeCam(CameraType.ViewCam);

        StartCoroutine(StartViewCamCo());
    }
    IEnumerator StartViewCamCo()
    {
        yield return new WaitForSeconds(1.5f); // ī�޶� �̵� �ð�(1.5 => Default) + ���� Appear�ִϸ��̼ǿ��� ���¢�� Ÿ�̹��� �ð�(1)

        ChangeCam(CameraType.PlayerCam);
    }

    public void StartEvent_0_Cam()
    {
        ChangeCam(CameraType.Event_0_Cam);

        StartCoroutine(StartEvent_0_CamCo());
    }
    IEnumerator StartEvent_0_CamCo()
    {
        yield return new WaitForSeconds(5f); // ī�޶� �̵� �ð�(1.5 => Default) + ���� Appear�ִϸ��̼ǿ��� ���¢�� Ÿ�̹��� �ð�(1)

        ChangeCam(CameraType.PlayerCam);
    }

    public void StartEvent_1_Cam()
    {
        ChangeCam(CameraType.Event_1_Cam);

        StartCoroutine(StartEvent_1_CamCo());
    }
    IEnumerator StartEvent_1_CamCo()
    {
        yield return new WaitForSeconds(2f); // ī�޶� �̵� �ð�(1.5 => Default) + ���� Appear�ִϸ��̼ǿ��� ���¢�� Ÿ�̹��� �ð�(1)

        //ChangeCam(CameraType.PlayerCam);
    }

    public void StartEvent_2_Cam()
    {
        ChangeCam(CameraType.Event_2_Cam);

        StartCoroutine(StartEvent_2_CamCo());
    }
    IEnumerator StartEvent_2_CamCo()
    {
        yield return new WaitForSeconds(3f); // ī�޶� �̵� �ð�(1.5 => Default) + ���� Appear�ִϸ��̼ǿ��� ���¢�� Ÿ�̹��� �ð�(1)

        //ChangeCam(CameraType.PlayerCam);
    }

    public void StartEvent_3_Cam()
    {
        ChangeCam(CameraType.Event_3_Cam);

        StartCoroutine(StartEvent_3_CamCo());
    }
    IEnumerator StartEvent_3_CamCo()
    {
        yield return new WaitForSeconds(3f); // ī�޶� �̵� �ð�(1.5 => Default) + ���� Appear�ִϸ��̼ǿ��� ���¢�� Ÿ�̹��� �ð�(1)

        //ChangeCam(CameraType.PlayerCam);
    }
    public void StartEvent_4_Cam()
    {
        ChangeCam(CameraType.Event_4_Cam);

        //StartCoroutine(StartEvent_4_CamCo());
    }
    /*
    IEnumerator StartEvent_4_CamCo()
    {
        //yield return new WaitForSeconds(3f); // ī�޶� �̵� �ð�(1.5 => Default) + ���� Appear�ִϸ��̼ǿ��� ���¢�� Ÿ�̹��� �ð�(1)

        //ChangeCam(CameraType.PlayerCam);
    }
    */

    public void StartBossCam()
    {
        ChangeCam(CameraType.Boss_Cam);

        StartCoroutine(StartBossCamCo());
    }

    IEnumerator StartBossCamCo()
    {
        yield return new WaitForSeconds(2.0f); // ī�޶� �̵� �ð�(1.5) + ���� Appear�ִϸ��̼ǿ��� ���¢�� Ÿ�̹��� �ð�(1)

        StartEffectCam(CameraType.ViewCam, 3f, 2.5f); // ī�޶� ���� ����

        yield return new WaitForSeconds(2.5f); //���� ���忬�� ���ӽð�

        ChangeCam(CameraType.PlayerCam);
    }

    public void StartEffectCam(CameraType type, float intensity, float time) // Shakeȿ��
    {
        _nowType = type;
        switch (type)
        {
            case CameraType.PlayerCam:
                {
                    for (int i = 0; i < 3; i++) // �÷��̾��� Cam�� FreeLook�̹Ƿ�, Top, Middle, Bottom Rig, ��� �����ؾ� �Ѵ�.
                    {
                        CinemachineVirtualCamera cam = _playerCam.GetRig(i); // i��° Rig(0 : Top, 1 : Middle ...)�� ���� ī�޶� �����´�
                        CinemachineBasicMultiChannelPerlin multi = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // �� ī�޶� CinemachineBasicMultiChannelPerlin (Noise �κ�) ������Ʈ�� �����´�.
                        multi.m_AmplitudeGain = intensity; // �� ������Ʈ�� AmplitudeGain(��鸲 ����)�� ���ڷ� ������ ������ �������ش�. => intensity��ŭ ��鸲
                    }
                    break;
                }
            default:
                CinemachineBasicMultiChannelPerlin v_multi = _nowCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                v_multi.m_AmplitudeGain = intensity;
                break;
        }
        
        StartCoroutine(EffectAction(time)); // _shakeTime��ŭ�� �ð����� ���ӽ����ش�.
    }
    IEnumerator EffectAction(float time) // time��ŭ ����
    {
        yield return new WaitForSeconds(time); // ���ڷ� ���� time��ŭ ���� ��,

        EndEffectAction();
    }
    public void EndEffectAction()
    {
        switch (_nowType)
        {
            case CameraType.PlayerCam:
                {
                    for (int i = 0; i < 3; i++) // �÷��̾��� Cam�� FreeLook�̹Ƿ�, Top, Middle, Bottom Rig, ��� �����ؾ� �Ѵ�.
                    {
                        CinemachineVirtualCamera cam = _playerCam.GetRig(i); // i��° Rig(0 : Top, 1 : Middle ...)�� ���� ī�޶� �����´�
                        CinemachineBasicMultiChannelPerlin multi = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // �� ī�޶� CinemachineBasicMultiChannelPerlin (Noise �κ�) ������Ʈ�� �����´�.
                        multi.m_AmplitudeGain = 0; // �� ������Ʈ�� AmplitudeGain(��鸲 ����)�� ���ڷ� ������ ������ �������ش�. => intensity��ŭ ��鸲
                    }
                    break;
                }
            default:
                CinemachineBasicMultiChannelPerlin v_multi = _nowCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                v_multi.m_AmplitudeGain = 0;
                break;
        }
    }

    public void ChangeCam(CameraType type)
    {
        //if (_nowCam == _cams[(int)type]) return; // ���� ī�޶� ��ȯ�Ϸ��� ī�޶��� ��� �ٷ� ����

        for(int i = 0; i < _cams.Count; i++)
        {
            if(i == (int)type)
            {
                if (!_cams[i].gameObject.activeSelf)
                    _cams[i].gameObject.SetActive(true);

                _cams[i].Priority = 1;
                _nowType = type;
                _nowCam = _cams[i];
            }
            else
                _cams[i].Priority = 0;
        }
        
        if(type == CameraType.PlayerCam)
        {
            FindPlayerFocus();
            _playerCam = _nowCam.GetComponent<CinemachineFreeLook>();
            _nowCam.LookAt = _playerFocus.transform;
            _nowCam.Follow = _playerFocus.transform;
        }
    }
}
