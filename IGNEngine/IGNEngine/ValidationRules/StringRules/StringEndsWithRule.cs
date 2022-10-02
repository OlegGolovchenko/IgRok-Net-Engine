//#############################################################################
//  Igrok-Net Engine
//  Copyright(C) 2022  Oleg Golovchenko
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>
//#############################################################################

namespace IGNEngine.ValidationRules.StringRules
{
    public class StringEndsWithRule : ValidationRule
    {
        private string characters;
        public StringEndsWithRule()
        {
        }

        public override bool IsValid(object data)
        {
            if (data is string)
            {
                return ((string)data).EndsWith(characters);
            }
            return false;
        }

        internal override void Init(object parameter)
        {
            this.characters = (string)parameter;
        }
    }
}
