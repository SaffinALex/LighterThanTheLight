using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator {
    ShapeSettings settings;
    INoiseFilter[] noiseFilters;
    public MinMax elevationMinMax;

    public void UpdateSettings(ShapeSettings settings){
        this.settings = settings;
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];

        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere){
        float firstLayerValue = 0;
        float elevation = 0;
        Vector3 rand3 = new Vector3(Random.Range(0.0f, settings.randomGeneration), Random.Range(0.0f, settings.randomGeneration), Random.Range(0.0f, settings.randomGeneration));

        if (noiseFilters.Length > 0){
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere, rand3);
            if(settings.noiseLayers[0].enabled){
                elevation = firstLayerValue;
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if(settings.noiseLayers[i].enabled){
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere, rand3) * mask;
            }
        }
        elevation = settings.planetRadius * (1+elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}
