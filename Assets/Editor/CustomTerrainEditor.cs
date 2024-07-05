using UnityEngine;
using UnityEditor;
using EditorGUITable;

[CustomEditor(typeof(CustomTerrain))]
[CanEditMultipleObjects]
public class CustomTerrainEditor : Editor
{
    Vector2 scrollPos;

    bool showRandom = false;
    bool showLoadTexture = false;
    bool showPerlin = false;
    bool showMultiplePerlin = false;
    bool showVoronoi = false;
    bool showMidPointDisplacement = false;
    bool showSmooth = false;
    bool showSplatMaps = false;
    bool showVegetation = false;
    bool showDetails = false;
    bool showWater = false;
    bool showErosion = false;
    bool showClouds = false;

    int spacing = 15;

    SerializedProperty resetTerrain;

    SerializedProperty randomHeightRange;

    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;

    SerializedProperty perlinXScale;
    SerializedProperty perlinYScale;
    SerializedProperty perlinOffsetX;
    SerializedProperty perlinOffsetY;
    SerializedProperty perlinOctaves;
    SerializedProperty perlinPersistance;
    SerializedProperty perlinHeightScale;

    GUITableState perlinParameterTable;
    SerializedProperty perlinParameters;

    SerializedProperty voronoiPeaks;
    SerializedProperty voronoiFallOff;
    SerializedProperty voronoiDropOff;
    SerializedProperty voronoiMinHeight;
    SerializedProperty voronoiMaxHeight;
    SerializedProperty voronoiType;

    SerializedProperty mpdHeightMin;
    SerializedProperty mpdHeightMax;
    SerializedProperty mpdHeightDampenerPower;
    SerializedProperty mpdRoughness;

    SerializedProperty smoothAmount;

    GUITableState splatMapTable;

    SerializedProperty maxTrees;
    SerializedProperty treeSpacing;
    GUITableState vegetationTable;

    SerializedProperty maxDetails;
    SerializedProperty detailSpacing;
    GUITableState detailTable;

    SerializedProperty waterPrefab;
    SerializedProperty waterHeight;

    SerializedProperty erosionType;
    SerializedProperty erosionStrength;
    SerializedProperty erosionAmount;
    SerializedProperty springsPerRiver;
    SerializedProperty solubility;
    SerializedProperty droplets;
    SerializedProperty erosionSmoothAmount;

    SerializedProperty numClouds;
    SerializedProperty particlesPerCloud;
    SerializedProperty cloudScale;
    SerializedProperty cloudMaterial;
    SerializedProperty cloudShadowMaterial;
    SerializedProperty cloudStartSize;
    SerializedProperty cloudColor;
    SerializedProperty cloudLining;
    SerializedProperty cloudMinSpeed;
    SerializedProperty cloudMaxSpeed;
    SerializedProperty cloudRange;

    private void OnEnable()
    {
        resetTerrain = serializedObject.FindProperty("resetTerrain");

        randomHeightRange = serializedObject.FindProperty("randomHeightRange");

        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");

        perlinXScale = serializedObject.FindProperty("perlinXScale");
        perlinYScale = serializedObject.FindProperty("perlinYScale");
        perlinOffsetX = serializedObject.FindProperty("perlinOffsetX");
        perlinOffsetY = serializedObject.FindProperty("perlinOffsetY");
        perlinOctaves = serializedObject.FindProperty("perlinOctaves");
        perlinPersistance = serializedObject.FindProperty("perlinPersistance");
        perlinHeightScale = serializedObject.FindProperty("perlinHeightScale");

        perlinParameterTable = new GUITableState("perlinParameterTable");
        perlinParameters = serializedObject.FindProperty("perlinParameters");

        voronoiPeaks = serializedObject.FindProperty("voronoiPeaks");
        voronoiFallOff = serializedObject.FindProperty("voronoiFallOff");
        voronoiDropOff = serializedObject.FindProperty("voronoiDropOff");
        voronoiMinHeight = serializedObject.FindProperty("voronoiMinHeight");
        voronoiMaxHeight = serializedObject.FindProperty("voronoiMaxHeight");
        voronoiType = serializedObject.FindProperty("voronoiType");

        mpdHeightMin = serializedObject.FindProperty("mpdHeightMin");
        mpdHeightMax = serializedObject.FindProperty("mpdHeightMax");
        mpdHeightDampenerPower = serializedObject.FindProperty("mpdHeightDampenerPower");
        mpdRoughness = serializedObject.FindProperty("mpdRoughness");

        smoothAmount = serializedObject.FindProperty("smoothAmount");

        maxTrees = serializedObject.FindProperty("maxTrees");
        treeSpacing = serializedObject.FindProperty("treeSpacing");

        maxDetails = serializedObject.FindProperty("maxDetails");
        detailSpacing = serializedObject.FindProperty("detailSpacing");

        waterPrefab = serializedObject.FindProperty("waterPrefab");
        waterHeight = serializedObject.FindProperty("waterHeight");

        erosionType = serializedObject.FindProperty("erosionType");
        erosionStrength = serializedObject.FindProperty("erosionStrength");
        erosionAmount = serializedObject.FindProperty("erosionAmount");
        springsPerRiver = serializedObject.FindProperty("springsPerRiver");
        solubility = serializedObject.FindProperty("solubility");
        droplets = serializedObject.FindProperty("droplets");
        erosionSmoothAmount = serializedObject.FindProperty("erosionSmoothAmount");

        numClouds = serializedObject.FindProperty("numClouds");
        particlesPerCloud = serializedObject.FindProperty("particlesPerCloud");
        cloudScale = serializedObject.FindProperty("cloudScale");
        cloudMaterial = serializedObject.FindProperty("cloudMaterial");
        cloudShadowMaterial = serializedObject.FindProperty("cloudShadowMaterial");
        cloudStartSize = serializedObject.FindProperty("cloudStartSize");
        cloudColor = serializedObject.FindProperty("cloudColor");
        cloudLining = serializedObject.FindProperty("cloudLining");
        cloudMinSpeed = serializedObject.FindProperty("cloudMinSpeed");
        cloudMaxSpeed = serializedObject.FindProperty("cloudMaxSpeed");
        cloudRange = serializedObject.FindProperty("cloudRange");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CustomTerrain terrain = target as CustomTerrain;

        Rect r = EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(r.width), GUILayout.Height(r.height));
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(resetTerrain);

        if (showRandom = EditorGUILayout.Foldout(showRandom, "Random"))
        {
            GUILayout.Label("Set Heights Between Random Values", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeightRange);

            EditorGUILayout.Space();

            if (GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showLoadTexture = EditorGUILayout.Foldout(showLoadTexture, "Load Texture"))
        {
            GUILayout.Label("Load Heights From Texture", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);

            EditorGUILayout.Space();

            if (GUILayout.Button("Load Texture"))
            {
                terrain.LoadTextureTerrain();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showPerlin = EditorGUILayout.Foldout(showPerlin, "Perlin Noise"))
        {
            GUILayout.Label("Set Heights Using Perlin Noise", EditorStyles.boldLabel);

            EditorGUILayout.Slider(perlinXScale, 0.001f, 0.1f, new GUIContent("X Scale"));
            EditorGUILayout.Slider(perlinYScale, 0.001f, 0.1f, new GUIContent("Y Scale"));
            EditorGUILayout.IntSlider(perlinOffsetX, 0, 10000, new GUIContent("X Offset"));
            EditorGUILayout.IntSlider(perlinOffsetY, 0, 10000, new GUIContent("Y Offset"));
            EditorGUILayout.IntSlider(perlinOctaves, 1, 10, new GUIContent("Octaves"));
            EditorGUILayout.Slider(perlinPersistance, 1f, 10f, new GUIContent("Persistance"));
            EditorGUILayout.Slider(perlinHeightScale, 0f, 1f, new GUIContent("Height Scale"));

            if (GUILayout.Button("Perlin Noise"))
            {
                terrain.PerlinTerrain();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showMultiplePerlin = EditorGUILayout.Foldout(showMultiplePerlin, "Multiple Perlin Noise"))
        {
            GUILayout.Label("Set Heights Using Multiple Perlin Noise", EditorStyles.boldLabel);
            perlinParameterTable = GUITableLayout.DrawTable(perlinParameterTable, serializedObject.FindProperty("perlinParameters"));

            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
            {
                terrain.AddNewPerlin();
            }

            if (GUILayout.Button("-"))
            {
                terrain.RemovePerlin();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Apply Multiple Perlin"))
            {
                terrain.MultiplePerlinTerrain();
            }

            if (!resetTerrain.boolValue)
            {
                if (GUILayout.Button("RidgeNoise"))
                {
                    terrain.RidgeNoise();
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(spacing);
        }

        if (showVoronoi = EditorGUILayout.Foldout(showVoronoi, "Voronoi"))
        {
            GUILayout.Label("Set Voronoi Tesellation", EditorStyles.boldLabel);

            EditorGUILayout.IntSlider(voronoiPeaks, 1, 10, new GUIContent("Peak Count"));
            EditorGUILayout.Slider(voronoiFallOff, 0.001f, 10f, new GUIContent("Fall Off"));
            EditorGUILayout.Slider(voronoiDropOff, 0.001f, 10f, new GUIContent("Drop Off"));
            EditorGUILayout.Slider(voronoiMinHeight, 0f, 1f, new GUIContent("Min Height"));
            EditorGUILayout.Slider(voronoiMaxHeight, 0f, 1f, new GUIContent("Max Height"));
            EditorGUILayout.PropertyField(voronoiType);

            if (GUILayout.Button("Voronoi"))
            {
                terrain.Voronoi();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showMidPointDisplacement = EditorGUILayout.Foldout(showMidPointDisplacement, "Mid Point Displacement"))
        {
            GUILayout.Label("Set Mid Point Displacement", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(mpdHeightMin);
            EditorGUILayout.PropertyField(mpdHeightMax);
            EditorGUILayout.PropertyField(mpdHeightDampenerPower);
            EditorGUILayout.PropertyField(mpdRoughness);

            if (GUILayout.Button("Mid Point Displacement"))
            {
                terrain.MidPointDisplacement();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showSmooth = EditorGUILayout.Foldout(showSmooth, "Smooth Terrain"))
        {
            GUILayout.Label("Set Smooth Terrain", EditorStyles.boldLabel);

            EditorGUILayout.IntSlider(smoothAmount, 1, 10, new GUIContent("Smooth Amount"));

            if (GUILayout.Button("Smooth Terrain"))
            {
                terrain.SmoothTerrain();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showSplatMaps = EditorGUILayout.Foldout(showSplatMaps, "Splat Maps"))
        {
            GUILayout.Label("Set Splat Maps", EditorStyles.boldLabel);

            splatMapTable = GUITableLayout.DrawTable(splatMapTable, serializedObject.FindProperty("splatHeights"));

            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
            {
                terrain.AddNewSplatHeight();
            }

            if (GUILayout.Button("-"))
            {
                terrain.RemoveSplatHeight();
            }

            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Apply Splat Maps"))
            {
                terrain.SplatMaps();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showVegetation = EditorGUILayout.Foldout(showVegetation, "Vegetation"))
        {
            GUILayout.Label("Set Vegetation", EditorStyles.boldLabel);

            EditorGUILayout.IntSlider(maxTrees, 0, 10000, new GUIContent("Max Trees"));
            EditorGUILayout.IntSlider(treeSpacing, 2, 20, new GUIContent("Tree Spacing"));

            vegetationTable = GUITableLayout.DrawTable(vegetationTable, serializedObject.FindProperty("vegetation"));
            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddVegetation();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemoveVegetation();
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Apply Vegetation"))
            {
                terrain.PlantVegetation();
            }
            EditorGUILayout.Space(spacing);
        }

        if (showDetails = EditorGUILayout.Foldout(showDetails, "Details"))
        {
            GUILayout.Label("Set Details", EditorStyles.boldLabel);

            EditorGUILayout.IntSlider(maxDetails, 0, 10000, new GUIContent("Max Details"));
            EditorGUILayout.IntSlider(detailSpacing, 1, 20, new GUIContent("Detail Spacing"));

            detailTable = GUITableLayout.DrawTable(detailTable, serializedObject.FindProperty("details"));
            terrain.GetComponent<Terrain>().detailObjectDistance = maxDetails.intValue;
            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddNewDetails();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemoveDetail();
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Apply Details"))
            {
                terrain.AddDetails();
            }
            EditorGUILayout.Space(spacing);
        }

        if (showWater = EditorGUILayout.Foldout(showWater, "Water"))
        {
            GUILayout.Label("Set Water", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(waterPrefab);
            EditorGUILayout.Slider(waterHeight, 0, 1, new GUIContent("Water Height"));

            if (GUILayout.Button("Add Water"))
            {
                terrain.AddWater();
            }
            EditorGUILayout.Space(spacing);
        }

        if (showErosion = EditorGUILayout.Foldout(showErosion, "Erosion"))
        {
            GUILayout.Label("Set Erosion", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(erosionType);
            EditorGUILayout.Slider(erosionStrength, 0, 1, new GUIContent("Erosion Strength"));
            EditorGUILayout.Slider(erosionAmount, 0, 1, new GUIContent("Erosion Amount"));
            EditorGUILayout.IntSlider(droplets, 0, 500, new GUIContent("Droplets"));
            EditorGUILayout.Slider(solubility, 0.001f, 1f, new GUIContent("Solubility"));
            EditorGUILayout.IntSlider(springsPerRiver, 0, 20, new GUIContent("Springs Per River"));
            EditorGUILayout.IntSlider(erosionSmoothAmount, 0, 10, new GUIContent("Smooth Amount"));

            if (GUILayout.Button("Erode"))
            {
                terrain.Erode();
            }

            EditorGUILayout.Space(spacing);
        }

        if (showClouds = EditorGUILayout.Foldout(showClouds, "Clouds"))
        {
            GUILayout.Label("Set Clouds", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(numClouds, new GUIContent("Number of Clouds"));
            EditorGUILayout.PropertyField(particlesPerCloud, new GUIContent("Particles per Clouds"));
            EditorGUILayout.PropertyField(cloudStartSize, new GUIContent("Cloud Particle Size"));
            EditorGUILayout.PropertyField(cloudScale, new GUIContent("Size"));
            EditorGUILayout.PropertyField(cloudMaterial, true);
            EditorGUILayout.PropertyField(cloudShadowMaterial, true);
            EditorGUILayout.PropertyField(cloudColor, new GUIContent("Color"));
            EditorGUILayout.PropertyField(cloudLining, new GUIContent("Lining"));
            EditorGUILayout.PropertyField(cloudMinSpeed, new GUIContent("Min Speed"));
            EditorGUILayout.PropertyField(cloudMaxSpeed, new GUIContent("Max Speed"));
            EditorGUILayout.PropertyField(cloudRange, new GUIContent("Distance Traveled"));

            if (GUILayout.Button("Generate Clouds"))
            {
                terrain.GenerateClouds();
            }

            EditorGUILayout.Space(spacing);
        }
        
        if (GUILayout.Button("Reset Terrain"))
        {
            terrain.ResetTerrain();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
