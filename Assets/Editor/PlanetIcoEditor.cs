using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetIco))]
public class PlanetIcoEditor : Editor {
    PlanetIco planetIco;
    Editor shapeEditor;
    Editor colourEditor;

    public override void OnInspectorGUI(){
        using (var check = new EditorGUI.ChangeCheckScope()){
            base.OnInspectorGUI();
            if(check.changed){
                planetIco.GeneratePlanet();
            }
        }

        if(GUILayout.Button("Generate PlanetIco")){
            planetIco.GeneratePlanet();
        }

        DrawSettingsEditor(planetIco.shapeSettings, planetIco.OnShapeSettingsUpdated, ref planetIco.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(planetIco.colourSettings, planetIco.OnColourSettingsUpdated, ref planetIco.colourSettingsFoldout, ref colourEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor){
        if(settings != null){
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope()){
                if(foldout){
                    CreateCachedEditor(settings, null, ref editor);
                    if(editor == null) editor = CreateEditor(settings);
                    editor.OnInspectorGUI();

                    if(check.changed){
                        if(onSettingsUpdated != null){
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable(){
        planetIco = (PlanetIco) target;
    }
}
