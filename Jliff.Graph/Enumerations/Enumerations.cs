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

    public enum FormatStyle
    {
        a,
        b,
        bdo,
        big,
        blockquote,
        body,
        br,
        button,
        caption,
        center,
        cite,
        code,
        col,
        colgroup,
        dd,
        del,
        div,
        dl,
        dt,
        em,
        h1,
        h2,
        h3,
        h4,
        h5,
        h6,
        head,
        hr,
        html,
        i,
        img,
        label,
        legend,
        li,
        ol,
        p,
        pre,
        q,
        s,
        samp,
        select,
        small,
        span,
        strike,
        strong,
        sub,
        sup,
        table,
        tbody,
        td,
        tfoot,
        th,
        thead,
        title,
        tr,
        tt,
        u,
        ul
    }

    public enum Application
    {
        source,
        target
    }

    public enum YesNo
    {
        yes,
        no
    }

    public enum YesNoFirstNo
    {
        yes,
        no,
        firstNo
    }

    public enum LocQualityIssueType
    {
        addition,
        characters,
        duplication,
        formatting,
        grammar,
        inconsistency,
        inconsistentEntities,
        internationalization,
        legal,
        length,
        localeSpecificContent,
        localeViolation,
        markup,
        misspelling,
        mistranslation,
        nonConformance,
        numbers,
        omission,
        other,
        patternProblem,
        register,
        style,
        terminology,
        typographical,
        uncategorized,
        untranslated,
        whitespace
    }
}