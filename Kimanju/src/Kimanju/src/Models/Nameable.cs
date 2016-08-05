using System;
using Helpers;

namespace Kimanju {
    public class Nameable {
        private readonly String _name;

        public String Name { get { return _name; } }

        public Nameable(String name) {
            this._name = Objects.RequireNonNull(name, "Name can't be null.");
        }

        public override bool Equals (Object o) {
            if (o == null || this.GetType() != o.GetType()) {
                return false;
            }

            Nameable nameable = (Nameable) o;
            
            return Name.ToLower().Equals(nameable.Name.ToLower());
        }
        
        public override int GetHashCode() {
            return 37 * 31 + Name.GetHashCode();
        }
    }
}