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


using System;
using System.Text.RegularExpressions;

namespace Localization.Jliff.Graph.Core
{
    public class Nmtoken
    {
        private string token;

        public Nmtoken()
        {
        }

        public Nmtoken(string token)
        {
            Token = token;
        }

        public string Token
        {
            get => token;

            set
            {
                bool foundMatch = false;
                try
                {
                    foundMatch = Regex.IsMatch(value, @"^[-._:A-Za-z0-9]+$", RegexOptions.IgnoreCase);
                }
                catch (ArgumentException ex)
                {
                    // Syntax error in the regular expression
                }

                if (foundMatch) token = value;
            }
        }
    }
}