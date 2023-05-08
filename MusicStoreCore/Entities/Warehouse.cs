﻿namespace MusicStoreCore.Entities
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
