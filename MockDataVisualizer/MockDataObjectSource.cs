using System;
using System.IO;
using Microsoft.VisualStudio.DebuggerVisualizers;
using MockDataDebugVisualizer.InitCodeDumper;

namespace MockDataDebugVisualizer
{
    public class MockDataObjectSource : VisualizerObjectSource 
    {
        public override void GetData(object target, Stream outgoingData)
        {
            var weakReference = target as WeakReference;

            var dump = Dumper.DumpInitilizationCodeMethod(weakReference.Target);
            
            var writer = new StreamWriter(outgoingData);
            
            writer.Write(dump);
            
            writer.Flush();

            outgoingData.Position = 0;
        }
    }
}
