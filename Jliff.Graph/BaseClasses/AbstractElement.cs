using System.ComponentModel;

namespace Localization.Jliff.Graph
{
    public abstract class AbstractElement
    {
        public AbstractElement()
        {
            canCopy = true;
            canDelete = true;
        }

        [DefaultValue(true)]
        public bool canCopy { get; set; }

        [DefaultValue(true)]
        public bool canDelete { get; set; }
    }
}