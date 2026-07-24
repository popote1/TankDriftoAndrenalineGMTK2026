using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralRoad : MonoBehaviour
{




    [SerializeField] private SplineContainer _spline;
    [SerializeField] private float _segmentsLength =1;
    [SerializeField] private float _segmentsHeigth =5;
    [SerializeField] private float _segmentsDepth =1;
    [SerializeField]private MeshFilter _meshFilter;
    [SerializeField] private bool _generateInUpdate;
    [SerializeField] private bool _topOfWallIsFlate =true;
    [SerializeField] private Mesh _meshToExport;
    

    [Space(20)] [SerializeField] private float roadRadius;
    [SerializeField]private Spline _rightSpline;
    [SerializeField] private float _powerTangentAdgustment=1;
    private Mesh _mesh;
    private List<Vector3> _vertices;
    private List<int>  _triangles;
    private List<Vector2> _uvs;
    void Update() {
        if( _generateInUpdate) Generate();
    }

    [ContextMenu("Generate")]
    private void Generate()
    {
        if (_spline.Splines.Count >= 2) {
            _spline.RemoveSplineAt(1);
        }

        if (_rightSpline == null) {
            _rightSpline = _spline.Splines[1];
        }
        _rightSpline.Clear();

        float nextKnotMod = 0;
        for (int i = 0; i < _spline.Spline.Count; i++) {
            BezierKnot not = _spline.Spline[i];
            Vector3 right = GetRight(not.Rotation);
            Vector3 up = GetUp(not.Rotation);
            Vector3 pos = _spline.transform.position + (Vector3)not.Position;   
                
            Debug.DrawLine(pos, pos + right * roadRadius, Color.red);
            Debug.DrawLine(pos, pos + up * roadRadius, Color.green);
            BezierKnot bezierKnot = new BezierKnot();
            bezierKnot.Position =  (Vector3)not.Position + right * roadRadius;
            bezierKnot.Rotation = not.Rotation;

            bezierKnot.TangentIn = not.TangentIn;
            bezierKnot.TangentOut = not.TangentOut;
            //bezierKnot.TangentIn =  ((Vector3)not.TangentOut).normalized * nextKnotMod- ((Vector3)not.TangentIn);
            
            if (i < _spline.Spline.Count - 1) {
                BezierKnot nextKnot = _spline.Spline[i+1];
                float dot = Vector3.Dot(nextKnot.TangentIn, not.TangentOut);
                float prevRightDot = Vector3.Dot(GetRight(not.Rotation), ((Vector3)(nextKnot.Position-not.Position)).normalized);
                float cross =Vector3.Dot(GetRight(not.Rotation)*roadRadius, ((Vector3)(nextKnot.Position-not.Position)));
                //float nextRightDot = Vector3.Dot(GetRight(nextKnot.Rotation), ((Vector3)(not.Position-nextKnot.Position)).normalized);
                prevRightDot = Mathf.Pow(prevRightDot, _powerTangentAdgustment);
                float newPrevMagnitude = ((Vector3)not.TangentOut).magnitude-((Vector3)not.TangentOut).magnitude * cross;
                //nextKnotMod =((Vector3)nextKnot.TangentOut).magnitude * nextRightDot+((Vector3)not.TangentOut).magnitude;
                bezierKnot.TangentOut = ((Vector3)not.TangentOut).normalized * cross;
            }
            
            
            
            _rightSpline.Add(bezierKnot); 
        }

        _spline.AddSpline(_rightSpline);



        //_mesh = new Mesh();
        //_vertices = new List<Vector3>();
        //_triangles = new List<int>();
        //_uvs = new List<Vector2>();
        //int segmentCount = Mathf.FloorToInt(_spline.CalculateLength() / _segmentsLength); 
        //for (int i = 1; i < segmentCount; i++) {
        //    float t1 = ((float)i - 1) / segmentCount;
        //    float t2 = ((float)i ) / segmentCount;
        //    GenerateRoad(t1, t2);
        //Vector3 pos =_spline.EvaluatePosition((float)i / segmentCount);
        //Vector3 tangant = _spline.EvaluateTangent((float)i / segmentCount);
        //Vector3 offset = Vector3.Cross(tangant.normalized, Vector3.up);
        //if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit)) {
        //    Debug.DrawLine(hit.point, hit.point+new Vector3(0,_segmentsHeigth), Color.crimson);
        //    Debug.DrawLine(hit.point+new Vector3(0,_segmentsHeigth)+offset, hit.point+new Vector3(0,_segmentsHeigth), Color.chartreuse);
        //}
        //}
        //_mesh.vertices = _vertices.ToArray();
        //_mesh.triangles = _triangles.ToArray();
        //_mesh.uv = _uvs.ToArray();
        //_mesh.RecalculateNormals();
        //_mesh.RecalculateTangents();
        //_meshFilter.mesh = _mesh;

    }
    
    private Vector3 GetRight(Quaternion rot)
    {
        Vector3 right = new Vector3();
        right.x = 1 - 2 * (Mathf.Pow(rot.y, 2) + Mathf.Pow(rot.z, 2));
        right.y= 2*(rot.x*rot.y+rot.w*rot.z);
        right.z = 2*(rot.x*rot.z-rot.w*rot.y);
        return right;
    }

    private Vector3 GetUp(Quaternion rot) {
        Vector3 up = new Vector3();
        up.x= 2*(rot.x*rot.y-rot.w*rot.z);
        up.y =1 - 2 * (Mathf.Pow(rot.x, 2) + Mathf.Pow(rot.z, 2));
        up.z =2*(rot.y*rot.z+rot.w*rot.x);
        return up;
    }

    private Vector3 GetForward(Quaternion rot) {
        Vector3 forward = new Vector3();
        forward.x = 2*(rot.x*rot.z+rot.w*rot.y);
        forward.y = 2*(rot.y*rot.z-rot.w*rot.x);
        forward.z = 1 - 2 * (Mathf.Pow(rot.x, 2) + Mathf.Pow(rot.y, 2));
        return forward;
    }
    private void GenerateRoad(float t1, float t2) {
        Vector3 pos = (Vector3)_spline.EvaluatePosition(t1) ;
        Vector3 forward = _spline.EvaluateTangent(t1);
        Vector3 up = _spline.EvaluateUpVector(t1);
        Vector3 right = Vector3.Cross(forward.normalized, up.normalized);
        Debug.DrawLine(pos, pos + right * roadRadius, Color.red);
        Debug.DrawLine(pos, pos + -right * roadRadius, Color.red);
    }

    private void GenerateSegments(float t1, float t2) {
        Vector3 pos1 =GetWallPoints(t1,out Vector3 pos3 );
        Vector3 pos2 =GetWallPoints(t2,out Vector3 pos4 );
        Vector3 pos5 = GetTangentWallPoints(t1, out Vector3 pos7);
        Vector3 pos6 = GetTangentWallPoints(t2, out Vector3 pos8);

        if (_topOfWallIsFlate) {
            pos5.y = pos1.y;
            pos6.y = pos2.y;
        }

        AddQuad(pos1,pos2, pos3, pos4 );
        AddQuadUVs(new Vector2(0,1), new Vector2(1,1), new Vector2(0,0), new Vector2(1,0));
        AddQuad(pos5,pos6, pos1, pos2 );
        AddQuadUVs(new Vector2(0,1), new Vector2(1,0), new Vector2(1,1), new Vector2(0,0));
        AddQuad(pos7,pos8, pos5, pos6 );
        AddQuadUVs(new Vector2(0,1), new Vector2(1,1), new Vector2(0,0), new Vector2(1,0));
    }

    private Vector3 GetWallPoints(float t, out Vector3 downPoint) {
        Vector3 pos = (Vector3)_spline.EvaluatePosition(t) ;
        if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit)) {
            downPoint = hit.point- transform.position;
            return hit.point+new Vector3(0,_segmentsHeigth,0)- transform.position;
        }
        downPoint = pos +new Vector3(0,-_segmentsHeigth,0);
        return pos;
    }

    private Vector3 GetTangentWallPoints(float t, out Vector3 downPoint) {
        Vector3 pos = (Vector3.Cross(((Vector3)_spline.EvaluateTangent(t)).normalized,Vector3.up)*_segmentsDepth)+(Vector3)_spline.EvaluatePosition(t);
        if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit)) {
            downPoint = hit.point- transform.position;
            return hit.point+new Vector3(0,_segmentsHeigth,0)- transform.position;
        }
        downPoint = pos +new Vector3(0,-_segmentsHeigth,0);
        return pos;
    }
    
    private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
        int index = _vertices.Count;
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);
        _vertices.Add(v4);
        
        _triangles.Add(index);
        _triangles.Add(index+1);
        _triangles.Add(index+2);
        
        _triangles.Add(index+1);
        _triangles.Add(index+3);
        _triangles.Add(index+2);
    }

   private void AddQuadUVs(Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4) {
        _uvs.Add(uv1);
        _uvs.Add(uv2);
        _uvs.Add(uv3);
        _uvs.Add(uv4);
    }
    [ContextMenu("SaveMesh")]
    private void SaveMesh() {
        Mesh mesh = Instantiate(_meshFilter.sharedMesh);
        mesh.name = "ProceduralMesh";
        AssetDatabase.CreateAsset(mesh, "Assets/"+mesh.name+".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [ContextMenu("ExportMesh")]
    private void ExportMesh()
    {
        string assetPath = AssetDatabase.GetAssetPath(_meshToExport);

        Debug.Log(AssetDatabase.ExtractAsset(_meshToExport, "Assets/ExportedMesh"));
        AssetDatabase.WriteImportSettingsIfDirty(assetPath);
        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        Debug.Log("Mesh Exported "+assetPath+ " at Assets/ExportedMesh.obj");
    }
    
    public static class ObjExporter
    {
        public static void ExportMesh(Mesh mesh, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Vector3 v in mesh.vertices)
                    sw.WriteLine($"v {v.x} {v.y} {v.z}");

                foreach (Vector3 n in mesh.normals)
                    sw.WriteLine($"vn {n.x} {n.y} {n.z}");

                foreach (Vector2 uv in mesh.uv)
                    sw.WriteLine($"vt {uv.x} {uv.y}");

                for (int i = 0; i < mesh.triangles.Length; i += 3)
                {
                    int a = mesh.triangles[i] + 1;
                    int b = mesh.triangles[i + 1] + 1;
                    int c = mesh.triangles[i + 2] + 1;
                    sw.WriteLine($"f {a}/{a}/{a} {b}/{b}/{b} {c}/{c}/{c}");
                }
            }
        }
    }
}

