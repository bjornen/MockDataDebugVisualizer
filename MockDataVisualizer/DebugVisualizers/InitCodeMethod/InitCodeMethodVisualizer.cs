using System;
using Microsoft.VisualStudio.DebuggerVisualizers;
using MockDataDebugVisualizer.DebugVisualizers.InitCodeMethod;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(InitCodeMethodVisualizer),
typeof(InitCodeMethodObjectSource),
Target = typeof(WeakReference),
Description = "Object create method")]
namespace MockDataDebugVisualizer.DebugVisualizers.InitCodeMethod
{
    public class InitCodeMethodVisualizer : AbstractMockDataVisualizer
    {
        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(InitCodeMethodVisualizer), typeof(InitCodeMethodObjectSource));

            visualizerHost.ShowVisualizer();
        }
    }
}
