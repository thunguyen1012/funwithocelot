using Common.Interfaces;

namespace Common
{
    public abstract class CommandBase
    {
        public IHeader Header { get; set; }
    }
}