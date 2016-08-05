using System;
using Helpers;

namespace Kimanju
{
    public class User
    {
        private readonly String _name;
        private readonly String _mail;

        public String Name { get { return _name; } }
        public String Mail { get { return _mail; } }

        public User(String name, String mail) {
            this._name = Objects.RequireNonNull(name, "User name can't be null.");
            this._mail = Objects.RequireNonNull(mail, "User mail can't be null.");
        }

        public override String ToString() {
            return String.Format("{0} ({1})", Name, Mail);
        }

        public override bool Equals (Object o) {
            if (o == null || this.GetType() != o.GetType()) {
                return false;
            }

            User user = (User) o;
            
            return Name.ToLower().Equals(user.Name.ToLower())
                && Mail.ToLower().Equals(user.Mail.ToLower());
        }
        
        public override int GetHashCode() {
            int result = 31;

            result = 37 * result + (Name.GetHashCode());
            result = 37 * result + (Mail.GetHashCode());

            return result;
        }
    }
}