using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class FPSCounter : MonoBehaviour
{
    public int Fps { get; private set; }
    
    [Range(1, 250)]
    public int frameRange = 60;

    public int MaxFps { get; private set; }
    public int MinFps { get; private set; }
    
    private int[] fpsBuffer;
    private int fpsBufferIndex;

    private void InitializeBuffer () {
        if (frameRange <= 0) {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }

    // Update is called once per frame
    private void Update()
    {		
        
        if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
            InitializeBuffer();
        }
        UpdateBuffer();
        CalculateFps();
       
     
    }

    private void UpdateBuffer()
    {
        //has to be unscaled because the time delta is not the actual time it took to process the last frame
        fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        
        if (fpsBufferIndex >= frameRange) {
            fpsBufferIndex = 0;
        }    
    }

    private void CalculateFps () {
        var sum = 0;
        var highest = 0;
        var lowest = int.MaxValue;
        for (var i = 0; i < frameRange; i++)
        {

            var current = fpsBuffer[i];
            sum += current;
            if (current > highest) {
                highest = current;
            }

            if (current < lowest)
            {
                lowest = current;
            }
        }
        Fps = sum / frameRange;
        MaxFps = highest;
        MinFps = lowest;
    }
}
