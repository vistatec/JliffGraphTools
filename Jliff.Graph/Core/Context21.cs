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


using Jliff.Graph.Serialization;

namespace Localization.Jliff.Graph.Core
{
    public class Context21
    {
        public string Ctr => "urn:oasis:names:tc:xliff:changetracking:2.0";
        public string Fs => "urn:oasis:names:tc:xliff:fs:2.0";
        public string Gls => Namespaces.GLS;
        public string Its => "https://www.w3.org/2005/11/its/";
        public string Itsm => Namespaces.ITS;
        public string Md => Namespaces.MDA;
        public string Mtc => Namespaces.MTC;
        public string Res => Namespaces.RES;
        public string Slr => "urn:oasis:names:tc:xliff:sizerestriction:2.0";
        public string Val => "urn:oasis:names:tc:xliff:validation:2.0";
        public string Xliff => "urn:oasis:names:tc:xliff:document:2.0";
    }
}