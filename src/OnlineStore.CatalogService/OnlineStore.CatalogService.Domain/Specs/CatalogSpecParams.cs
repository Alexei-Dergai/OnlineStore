﻿namespace OnlineStore.CatalogService.Domain.Specs
{
    public class CatalogSpecParams
    {
        private const int MaxPageSize = 70;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? CategoryId { get; set; }
        public string? ApplicationTypeId { get; set; }
        public SortOrder? Sort { get; set; }
        public string? Search { get; set; }
    }
}
