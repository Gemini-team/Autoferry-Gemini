using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RustInteropTest: MonoBehaviour
{

    private RenderTexture _renderTexture;

    public RenderTexture RenderTexture
    {
        get => _renderTexture;
        set => _renderTexture = value;
    }

    private Camera camera;
    private int frameCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Camera>() != null)
        {
            camera = gameObject.GetComponent<Camera>();
            _renderTexture = camera.targetTexture;
        }

        if ( _renderTexture == null)
        {
            _renderTexture = new RenderTexture(800, 640, 24, RenderTextureFormat.Default, 0);
            camera.targetTexture = _renderTexture;
        } 

        //byte[] arr = { 104, 104, 104, 104, 104, 104 };
        //uint length = (uint)arr.Length;

        int randomNum = RustInterop.GetRandomInt();
        Debug.Log("Random number: " + randomNum);
        Debug.Log("Multiply by two: " + RustInterop.MultiplyByTwo(randomNum));

        //Debug.Log("bufferstuff: " + RustInterop.WriteBytesToBMPFile(arr, length));

    }

    public void OnEnable()
    {
        RenderPipelineManager.endFrameRendering += EndFrameRendering;
    }

    public void OnDisable()
    {
        RenderPipelineManager.endFrameRendering -= EndFrameRendering;
    }



    void EndFrameRendering(ScriptableRenderContext context, Camera[] cameras)
    {
        if (!camera.name.Equals("Fly_optical_camera"))
            AsyncGPUReadback.Request(camera.activeTexture, 0, TextureFormat.RGB24, ReadbackCompleted);

    }

    void ReadbackCompleted(AsyncGPUReadbackRequest request)
    {

        /*
        ByteString data = ByteString.CopyFrom(request.GetData<byte>().ToArray());
        serviceImpl.Data = data;
        serviceImpl.DataLength = data.Length;
        */

        if (frameCounter < 1)
        {
            // TODO: Find a way that does not require the ToArray() call
            // since this is probably doing a copy.
            byte[] arr = request.GetData<byte>().ToArray();
            RustInterop.WriteBytesToBMPFile(arr, (uint)arr.Length);
            frameCounter++;
        }

    }
}
