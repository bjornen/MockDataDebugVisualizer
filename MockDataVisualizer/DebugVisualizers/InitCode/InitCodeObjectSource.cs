using MockDataDebugVisualizer.InitCodeDumper;

namespace MockDataDebugVisualizer.DebugVisualizers.InitCode
{
    public class InitCodeObjectSource : AbstractMockDataObjectSource 
    {
        public override string Dump(object objectToDump)
        {
            return Dumper.DumpInitlizationCode(objectToDump);
        }
    }
}
