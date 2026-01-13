using System;

namespace DTO
{
    public class TestClass
    {
        public ProductClass Product { get; set; }
        public class ProductClass
        {
            public Guid Guid { get; set; }
        }
    }
}
