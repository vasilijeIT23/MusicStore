﻿using MusicStoreCore.Enums;

namespace MusicStoreCore.Entities
{
    public class ProductType
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; } = string.Empty!;

        public ProductType() { }

        public ProductType(Category category, string name)
        {
            Category = category;
            Name = name;
        }
    }
}
