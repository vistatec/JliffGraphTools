namespace Localization.Jliff.Graph
{
    public enum ElementType
    {
        segment,
        ignorable
    }

    public enum State
    {
        initial,
        translated,
        reviewed,
        final
    }

    public enum ElementKind
    {
        ec,
        em,
        ph,
        sc,
        sm
    }

    public enum Application
    {
        source,
        target
    }
}