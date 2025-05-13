using UnityEngine;
using System.IO;

public class Screenshotter : MonoBehaviour
{
    [Header("Screenshot Settings")]
    public KeyCode screenshotKey = KeyCode.P;
    public string folderName = "Screenshots";
    public string fileNamePrefix = "screenshot_";

    private string folderPath;

    void Start()
    {
        // Prepare folder
        folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        Debug.Log($"Screenshots will be saved to: {folderPath}");
    }

    void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string filename = $"{fileNamePrefix}{timestamp}.png";
        string fullPath = Path.Combine(folderPath, filename);

        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log($"Screenshot saved: {fullPath}");
    }
}
