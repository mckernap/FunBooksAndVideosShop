using FunBooksAndVideos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Memberships
{
    public class BookMembership : Membership
    {
        public override string Name { get { return "Book Club Membership"; } }
    }
}
