using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    private Texture _colorTexture;
    private Color _fadeColor = Color.black;

    [HideInInspector] public float FadeBalance;
    public FadeState State;
    public float FadeSpeed; 
    public float FromInDelay;
    public float FromOutDelay;

    void Awake()
    {
        var nullTexture = new Texture2D(1, 1);
        nullTexture.SetPixel(0, 0, Color.black);
        nullTexture.Apply();
        _colorTexture = nullTexture;
        FadeBalance = (1 + FromInDelay);
    }

    void Update()
    {
        _fadeColor.a = FadeBalance;

        if (FadeBalance > (1 + FromInDelay))
        {
            FadeBalance = (1 + FromInDelay);
            State = FadeState.InEnd;
        }

        if (FadeBalance < -(0 + FromOutDelay))
        {
            FadeBalance = -(0 + FromOutDelay);
            State = FadeState.OutEnd;
        }

        switch (State)
        {
            case FadeState.In:
                FadeBalance += Time.deltaTime * FadeSpeed;
                break;

            case FadeState.Out:
                FadeBalance -= Time.deltaTime * FadeSpeed;
                break;

            case FadeState.Stop:
                FadeBalance -= 0;
                break;

            case FadeState.InEnd:
                FadeBalance = (1 + FromInDelay);
                break;

            case FadeState.OutEnd:
                FadeBalance = -(0 + FromOutDelay);
                break;
        }
    }

    void OnGUI()
    {
        GUI.depth = -2;
        GUI.color = _fadeColor;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _colorTexture, ScaleMode.StretchToFill, true);
    }

    public enum FadeState
    {
        In,
        Out,
        Stop,
        InEnd,
        OutEnd
    }

}