/*
 * Copyright (C) 2018-2019, Vistatec or third-party contributors as indicated
 * by the @author tags or express copyright attribution statements applied by
 * the authors. All third-party contributions are distributed under license by
 * Vistatec.
 *
 * This file is part of JliffGraphTools.
 *
 * JliffGraphTools is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JliffGraphTools is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program. If not, write to:
 *
 *     Free Software Foundation, Inc.
 *     51 Franklin Street, Fifth Floor
 *     Boston, MA 02110-1301
 *     USA
 *
 * Also, see the full LGPL text here: <http://www.gnu.org/copyleft/lesser.html>
 */


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
            segment,
            ignorable
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

        public enum JlfNodeType
        {
            ec,
            em,
            file,
            group,
            ignorable,
            ph,
            sc,
            segment,
            sm,
            text,
            unit
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

        public enum MarkType
        {
            comment,
            generic,
            itsTermNo,
            term
        }

        public enum MatchType
        {
            am,
            mt,
            icm,
            idm,
            tb,
            tm,
            other
        }

        public enum Normalization
        {
            nfc,
            nfd,
            none
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