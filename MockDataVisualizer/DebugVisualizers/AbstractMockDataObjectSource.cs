using System;
using System.IO;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace MockDataDebugVisualizer.DebugVisualizers
{
    public abstract class AbstractMockDataObjectSource : VisualizerObjectSource 
    {
        public override void GetData(object target, Stream outgoingData)
        {
            var weakReference = target as WeakReference;

            var dump = Dump(weakReference.Target);
            
            var writer = new StreamWriter(outgoingData);
            
            writer.Write(dump);
            
            writer.Flush();

            outgoingData.Position = 0;
        }

        public abstract string Dump(object objectToDump);
    }
}
