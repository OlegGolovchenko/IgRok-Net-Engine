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
using IGNEngine.ValidationRules;
using System;
using System.Collections.Generic;
using System.Data;

namespace IGNEngine
{
    public class ValidationEngine
    {
        private IDictionary<string, ValidationRuleChain> knownRules;

        public ValidationEngine()
        {
            this.knownRules = new Dictionary<string, ValidationRuleChain>();
        }

        public string AddRuleChainFor(string validationTypeKey)
        {
            ValidationRuleChain chain = new ValidationRuleChain();
            this.knownRules.Add(validationTypeKey, chain);
            return chain.Id;
        }

        public void AddRuleFor(string validationTypeKey, ValidationRule rule)
        {
            this.knownRules.TryGetValue(validationTypeKey, out ValidationRuleChain chain);
            if(chain != null)
            {
                chain.AddRule(rule);
            }
        }

        public void RemoveLastRuleFor(string validationTypeKey)
        {
            this.knownRules.TryGetValue(validationTypeKey, out ValidationRuleChain chain);
            if (chain != null)
            {
                chain.RemoveLastRule();
            }
        }

        public void RemoveChainFor(string validationTypeKey)
        {
            this.knownRules.Remove(validationTypeKey);
        }

        public bool ValidateChain(string validationTypeKey, object data)
        {
            this.knownRules.TryGetValue(validationTypeKey, out ValidationRuleChain chain);
            if (chain != null)
            {
                return chain.ValidateChain(data);
            }
            return false;
        }
    }
}
