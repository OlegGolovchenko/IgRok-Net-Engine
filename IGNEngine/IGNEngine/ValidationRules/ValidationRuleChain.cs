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

using IGNEngine.ValidationRules.StringRules;
using System;
using System.Collections.Generic;

namespace IGNEngine.ValidationRules
{
    public class ValidationRuleChain
    {
        private IList<ValidationRule> rules;
        private int currentRuleIndex;

        public ValidationRule CurrentRule
        {
            get
            {
                if (this.currentRuleIndex >= 0 && this.currentRuleIndex < this.rules.Count)
                {
                    return this.rules[currentRuleIndex];
                }
                return null;
            }
        }

        public ValidationRule NextRule
        {
            get
            {
                if (this.currentRuleIndex + 1 >= 0 && this.currentRuleIndex + 1 < this.rules.Count)
                {
                    return this.rules[currentRuleIndex + 1];
                }
                return null;
            }
        }

        public bool HasNextRule { get => NextRule != null; }

        public string Id { get; private set; }

        public ValidationRuleChain()
        {
            this.currentRuleIndex = 0;
            this.rules = new List<ValidationRule>();
            Id = Guid.NewGuid().ToString();
        }

        public void AddRule<T>(object parameter) where T : ValidationRule, new()
        {
            ValidationRule rule = new T();
            rule.Init(parameter);
            this.rules.Add(rule);
        }

        public void RemoveLastRule()
        {
            this.rules.RemoveAt(this.rules.Count - 1);
        }

        public bool ValidateCurrentRule(object data)
        {
            bool result = false;
            if(CurrentRule != null)
            {
                result = CurrentRule.IsValid(data);
            }
            this.currentRuleIndex++;
            return result;
        }

        public bool ValidateChain(object data)
        {
            bool result = true;
            while(this.HasNextRule && result)
            {
                result =  result && this.ValidateCurrentRule(data);
            }
            return result;
        }
    }
}
