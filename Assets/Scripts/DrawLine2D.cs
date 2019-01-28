using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine2D : MonoBehaviour
{

    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected EdgeCollider2D m_EdgeCollider2D;
    [SerializeField]
    protected Camera m_Camera;
    protected List<Vector2> m_Points;

    // HERE
    private float startTime;

    public Slider energy;

    public float maxEnergy = 100;
    private float curEnergy;

    public float regenPerSec = 20f;
    public float costPerUnit = 40f;
    public float clickCost = 10f;
    public float stayTime = 3f;

    public bool clone = false;
    public float cloneTimer = 0f;

    private bool hasClone = false;

    private Vector3 lastPos;

    private bool firstRun = true;

    private void setEnergy(float amt)
    {
        curEnergy = Mathf.Clamp(amt, 0, maxEnergy);

        energy.value = amt / maxEnergy;
    }

    private void Start()
    {
        if (!clone)
        {
            setEnergy(maxEnergy);
        }
    }

    public virtual LineRenderer lineRenderer
    {
        get
        {
            return m_LineRenderer;
        }
    }

    public virtual bool addCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    public virtual EdgeCollider2D edgeCollider2D
    {
        get
        {
            return m_EdgeCollider2D;
        }
    }

    public virtual List<Vector2> points
    {
        get
        {
            return m_Points;
        }
    }

    protected virtual void Awake ()
    {
        if ( m_LineRenderer == null )
        {
            Debug.LogWarning ( "DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer." );
            CreateDefaultLineRenderer ();
        }
        if ( m_EdgeCollider2D == null && m_AddCollider )
        {
            Debug.LogWarning ( "DrawLine: Edge Collider 2D not assigned, Adding and Using default Edge Collider 2D." );
            CreateDefaultEdgeCollider2D ();
        }
        if ( m_Camera == null ) 
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2> ();

        startTime = Time.time;
    }

    protected virtual void Update ()
    {
        if (clone)
        {
            // HERE
            float curTime = Time.time - startTime;
            if (curTime > stayTime)
            {
                Destroy(gameObject);
            }

            return;
        }

        if (hasClone && Time.time - cloneTimer > stayTime)
        {
            GetComponent<EdgeCollider2D>().isTrigger = true;

            print("A");
            Reset();

            hasClone = false;

        }


        if (Input.GetMouseButtonUp(0))
        {
            GameObject field = Object.Instantiate(gameObject);
            field.gameObject.GetComponent<DrawLine2D>().clone = true;
            cloneTimer = Time.time;

            hasClone = true;

            firstRun = true;
        }
        if ( Input.GetMouseButtonDown ( 0 ) )
        {
            Reset ();
        }
        if (Input.GetMouseButton(0))
        {
            if (curEnergy < clickCost + 2f && firstRun)
            {
                return;
            }

            if (firstRun) {
                firstRun = false;
                setEnergy(curEnergy - clickCost); 
            }
            else
            {
                //try
                //{
                //    setEnergy(curEnergy - (costPerUnit * Vector3.Distance(m_Points[m_Points.Count-1], m_Camera.ScreenToWorldPoint(Input.mousePosition))));
                //} catch (UnityException ex)
                //{
                //    return;
                //}

            }

            lastPos = Input.mousePosition;


            if (curEnergy <= 0.001f)
            {
                return;
            }

            Vector2 mousePosition = m_Camera.ScreenToWorldPoint ( Input.mousePosition );
            if ( !m_Points.Contains ( mousePosition ) )
            {
                m_Points.Add ( mousePosition );
                m_LineRenderer.positionCount = m_Points.Count;
                m_LineRenderer.SetPosition ( m_LineRenderer.positionCount - 1, mousePosition );

                if ( m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1 )
                {
                    setEnergy(curEnergy - (costPerUnit * Vector3.Distance(m_LineRenderer.GetPosition(m_LineRenderer.positionCount - 2), mousePosition)));
                    m_EdgeCollider2D.points = m_Points.ToArray();
                    m_EdgeCollider2D.offset = transform.position * - 1;
                    m_EdgeCollider2D.edgeRadius = 0.06f;
                }
            }
        }
        else
        {
            setEnergy(curEnergy + (regenPerSec * Time.deltaTime));
        }

    }

    protected virtual void Reset ()
    {
        if ( m_LineRenderer != null )
        {
            m_LineRenderer.positionCount = 0;
        }
        if ( m_Points != null )
        {
            m_Points.Clear ();
        }
        if ( m_EdgeCollider2D != null)
        {
            m_EdgeCollider2D.Reset();
        }

    }

    protected virtual void CreateDefaultLineRenderer ()
    {
        m_LineRenderer = gameObject.AddComponent<LineRenderer> ();
        m_LineRenderer.positionCount = 0;
        m_LineRenderer.material = new Material ( Shader.Find ( "Particles/Additive" ) );
        m_LineRenderer.startColor = Color.white;
        m_LineRenderer.endColor = Color.white;
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;
        m_LineRenderer.useWorldSpace = true;
    }

    protected virtual void CreateDefaultEdgeCollider2D ()
    {
        m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D> ();
    }

}