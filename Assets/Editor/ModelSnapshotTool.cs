using System.IO;
using UnityEditor;
using UnityEngine;
public class ModelSnapshotTool : EditorWindow
{
    string prefabFolder = "Assets/ToonyTinyPeople/TT_RTS/TT_RTS_Standard/prefabs";
    string outputFolder = "Assets/CharacterIcons";
    int imageSize = 512;
    [MenuItem("Tools/Model Snapshot Tool")]
    static void Open()
    {
        GetWindow<ModelSnapshotTool>("Model Snapshot");
    }
    void OnGUI()
    {
        GUILayout.Label("3D Model To PNG", EditorStyles.boldLabel);
        prefabFolder =
            EditorGUILayout.TextField(
                "Prefab Folder",
                prefabFolder);
        outputFolder =
            EditorGUILayout.TextField(
                "Output Folder",
                outputFolder);
        imageSize =
            EditorGUILayout.IntField(
                "Image Size",
                imageSize);
        GUILayout.Space(20);
        if (GUILayout.Button("Generate PNG"))
        {
            Generate();
        }
    }
    void Generate()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            AssetDatabase.Refresh();
        }
        GameObject cameraObj =
            new GameObject("Temp_PreviewCamera");
        Camera cam =
            cameraObj.AddComponent<Camera>();
        cam.clearFlags =
            CameraClearFlags.SolidColor;
        cam.backgroundColor =
            new Color(0, 0, 0, 0);
        cam.fieldOfView = 35;
        GameObject lightObj =
            new GameObject("Temp_Light");
        Light light =
            lightObj.AddComponent<Light>();
        light.type =
            LightType.Directional;
        light.intensity = 1.5f;
        light.transform.rotation =
            Quaternion.Euler(40, -30, 0);
        RenderTexture rt =
            new RenderTexture(
                imageSize,
                imageSize,
                24,
                RenderTextureFormat.ARGB32);
        cam.targetTexture = rt;
        string[] guids =
            AssetDatabase.FindAssets(
                "t:Prefab",
                new[] { prefabFolder });
        foreach (string guid in guids)
        {
            string path =
                AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab =
                AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
                continue;
            GameObject obj =
                (GameObject)
                PrefabUtility.InstantiatePrefab(prefab);
            obj.transform.position =
                Vector3.zero;
            obj.transform.rotation =
                Quaternion.identity;
            obj.transform.Rotate(0, 180, 0);
            SetupCamera(cam, obj);
            Texture2D tex =
                Capture(cam, rt);
            byte[] png =
                tex.EncodeToPNG();
            string savePath =
                outputFolder
                + "/"
                + prefab.name
                + ".png";
            File.WriteAllBytes(
                savePath,
                png);
            AssetDatabase.ImportAsset(
                savePath,
                ImportAssetOptions.ForceUpdate);
            TextureImporter importer =
                AssetImporter.GetAtPath(savePath)
                    as TextureImporter;
            if (importer != null)
            {
                importer.textureType =
                    TextureImporterType.Sprite;
                importer.spriteImportMode =
                    SpriteImportMode.Single;
                importer.alphaIsTransparency =
                    true;
                importer.SaveAndReimport();
            }
            DestroyImmediate(tex);
            DestroyImmediate(obj);
            Debug.Log(
                "Generated : "
                + prefab.name);
        }
        rt.Release();
        DestroyImmediate(rt);
        DestroyImmediate(cameraObj);
        DestroyImmediate(lightObj);
        AssetDatabase.Refresh();
        Debug.Log("All Done!");
    }
    void SetupCamera(Camera cam, GameObject obj)
    {
        Renderer[] renderers =
            obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return;
        Bounds bounds =
            renderers[0].bounds;
        foreach (Renderer r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }
        float size =
            Mathf.Max(
                bounds.size.x,
                bounds.size.y,
                bounds.size.z);
        cam.transform.position =
            bounds.center
            +
            new Vector3(
                0,
                size * 0.2f,
                -size * 2);
        cam.transform.LookAt(
            bounds.center);
    }
    Texture2D Capture(
        Camera cam,
        RenderTexture rt)
    {
        RenderTexture.active = rt;
        cam.Render();
        Texture2D tex =
            new Texture2D(
                rt.width,
                rt.height,
                TextureFormat.ARGB32,
                false);
        tex.ReadPixels(
            new Rect(
                0,
                0,
                rt.width,
                rt.height),
            0,
            0);
        tex.Apply();
        RenderTexture.active = null;
        return tex;
    }
}
