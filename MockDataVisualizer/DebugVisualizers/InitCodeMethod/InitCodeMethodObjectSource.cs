using MockDataDebugVisualizer.InitCodeDumper;

namespace MockDataDebugVisualizer.DebugVisualizers.InitCodeMethod
{
    public class InitCodeMethodObjectSource : AbstractMockDataObjectSource 
    {
        public override string Dump(object objectToDump)
        {
            return DumperBase.DumpInitilizationCodeMethod(objectToDump);
        }
    }
}
