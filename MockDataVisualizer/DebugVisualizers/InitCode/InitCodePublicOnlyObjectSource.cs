using MockDataDebugVisualizer.InitCodeDumper;

namespace MockDataDebugVisualizer.DebugVisualizers.InitCode
{
    public class InitCodePublicOnlyObjectSource : AbstractMockDataObjectSource 
    {
        public override string Dump(object objectToDump)
        {
            return DumperBase.DumpCode(objectToDump, DumpMode.CodeOnly, Visibility.PublicOnly);
        }
    }
}
