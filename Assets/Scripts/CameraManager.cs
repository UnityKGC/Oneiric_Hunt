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

    [SerializeField] CinemachineFreeLook _playerCam;

    public GameObject _playerFocus;

    float wheelValue = 0f; // 지역변수 => 수치가 변경된 현재 값
    [SerializeField] float wheelSpd = 2f; // 변경되는 스피드
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {

    }
    private void Update()
    {
        float value = Input.GetAxis("Mouse ScrollWheel"); // 마우스 휠 확인 => 휠을 위로 올리면 value = 0.1이 되고, 휠을 아래로 내리면 value = -0.1가 된다.
        if (value != 0) // value가 0이 아니라면 => 값이 변경되었다면,
        {
            wheelValue += value; // wheelValue에 value를 더해준다.
            wheelValue = Mathf.Clamp(wheelValue, -5f, 3f); // wheelValue를 최소 -5, 최대 3의 수를 지닌다.

            if (wheelValue >= 3f || wheelValue <= -5f) return; // 최대치에 도달하면 리턴 => 값이 변하지 않음.

            for (int i = 0; i < 3; i++) // 각 Rig(Top, Middle, Bottom)의 돌면서 설정.
            {
                // 만들고 있는 게임이 스카이림 레퍼런스라서, 휠업하면 카메라가 가까워지고, 휠다운하면 카메라가 멀어지므로, 값을 -해준다.
                _playerCam.m_Orbits[i].m_Height -= value * wheelSpd; // 높이를 value만큼 -해준다
                _playerCam.m_Orbits[i].m_Radius -= value * wheelSpd; // 너비를 value만큼 -해준다.
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

        Vector3 pos = fromObj.transform.position + (fromObj.transform.forward * -5f) + (fromObj.transform.right * 3f);
        pos.y = 3f;

        _nowCam.ForceCameraPosition(pos, Quaternion.identity);
        _nowCam.LookAt = toObj.transform;
    }
    public void StartBossCam()
    {
        ChangeCam(CameraType.ViewCam);

        StartCoroutine(StartBossCamCo());
    }
    IEnumerator StartBossCamCo()
    {
        yield return new WaitForSeconds(2.0f); // 카메라 이동 시간(1.5) + 보스 Appear애니메이션에서 울부짖는 타이밍의 시간(1)

        StartEffectCam(CameraType.ViewCam, 3f, 2.5f); // 카메라 연출 실행

        yield return new WaitForSeconds(2.5f); //보스 등장연출 지속시간

        ChangeCam(CameraType.PlayerCam);
    }
    public void StartEffectCam(CameraType type, float intensity, float time) // Shake효과
    {
        switch(type)
        {
            case CameraType.PlayerCam:
                {
                    for (int i = 0; i < 3; i++) // 플레이어의 Cam은 FreeLook이므로, Top, Middle, Bottom Rig, 모두 변경해야 한다.
                    {
                        CinemachineVirtualCamera cam = _playerCam.GetRig(i); // i번째 Rig(0 : Top, 1 : Middle ...)의 가상 카메라를 가져온다
                        CinemachineBasicMultiChannelPerlin multi = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // 각 카메라에 CinemachineBasicMultiChannelPerlin (Noise 부분) 컴포넌트를 가져온다.
                        multi.m_AmplitudeGain = intensity; // 그 컴포넌트의 AmplitudeGain(흔들림 정도)를 인자로 가져온 값으로 설정해준다. => intensity만큼 흔들림
                    }
                    break;
                }
            default:
                CinemachineBasicMultiChannelPerlin v_multi = _nowCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                v_multi.m_AmplitudeGain = intensity;
                break;
        }
        
        StartCoroutine(EffectAction(type, time)); // _shakeTime만큼의 시간동안 지속시켜준다.
    }
    IEnumerator EffectAction(CameraType type, float time) // time만큼 지속
    {
        yield return new WaitForSeconds(time); // 인자로 받은 time만큼 지속 후,

        switch (type)
        {
            case CameraType.PlayerCam:
                {
                    for (int i = 0; i < 3; i++) // 플레이어의 Cam은 FreeLook이므로, Top, Middle, Bottom Rig, 모두 변경해야 한다.
                    {
                        CinemachineVirtualCamera cam = _playerCam.GetRig(i); // i번째 Rig(0 : Top, 1 : Middle ...)의 가상 카메라를 가져온다
                        CinemachineBasicMultiChannelPerlin multi = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // 각 카메라에 CinemachineBasicMultiChannelPerlin (Noise 부분) 컴포넌트를 가져온다.
                        multi.m_AmplitudeGain = 0; // 그 컴포넌트의 AmplitudeGain(흔들림 정도)를 인자로 가져온 값으로 설정해준다. => intensity만큼 흔들림
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
        for(int i = 0; i < _cams.Count; i++)
        {
            if(i == (int)type)
            {
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
