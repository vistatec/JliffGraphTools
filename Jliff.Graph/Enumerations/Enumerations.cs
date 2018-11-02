namespace Localization.Jliff.Graph
{
    public class Enumerations
    {
        public enum Application
        {
            source,
            target
        }

        public enum ElementKind
        {
            ec,
            em,
            ph,
            sc,
            sm
        }

        public enum ElementType
        {
            ignorable,
            segment
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

        public enum State
        {
            initial,
            translated,
            reviewed,
            final
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
    }
}