﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class WishList
    {
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public BookModel Book { get; set; }
    }
}
