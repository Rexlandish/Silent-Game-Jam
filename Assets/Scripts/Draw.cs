using UnityEngine;

public class Draw : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;
    public AudioSource DrawSound;
    public float drawDistance = 5f;

    private LineRenderer currentLineRenderer;
    private Vector3 lastPos;

    void Update()
    {
        Drawing();
    }

    void Drawing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
            DrawSound.enabled = true;
        }
        else if (Input.GetMouseButton(0) && currentLineRenderer != null)
        {
            PointToMousePos();
            DrawSound.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLineRenderer = null;
            DrawSound.enabled = false;
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.useWorldSpace = true;

        Vector3 mousePos = GetMouseWorldPosition();

        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

        lastPos = mousePos;
    }

    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(
            currentLineRenderer.positionCount - 1,
            pointPos
        );
    }

    void PointToMousePos()
    {
        Vector3 mousePos = GetMouseWorldPosition();

        if (Vector3.Distance(lastPos, mousePos) > 0.01f)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = drawDistance;
        return m_camera.ScreenToWorldPoint(mousePos);
    }
}