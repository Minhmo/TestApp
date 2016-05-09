using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp.Models;

namespace TestApp.Services
{
    public class TagsEqulityComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            if (x.Name.Equals(y.Name)) return true;
            return false;
        }

        public int GetHashCode(Tag obj)
        {
            var str = obj.Name + obj.Id;
            if (obj.ApplicationUserId != null)
            {
                str += obj.ApplicationUserId;
            }
            return str.GetHashCode();
        }
    }
}