using System;
using Microsoft.VisualStudio.DebuggerVisualizers;
using MockDataDebugVisualizer.DebugVisualizers.InitCode;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(InitCodeVisualizer),
typeof(InitCodeObjectSource),
Target = typeof(WeakReference),
Description = "Object init code")]
namespace MockDataDebugVisualizer.DebugVisualizers.InitCode
{
    public class InitCodeVisualizer : AbstractMockDataVisualizer
    {
        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(InitCodeVisualizer), typeof(InitCodeObjectSource));

            visualizerHost.ShowVisualizer();
        }
    }
}
