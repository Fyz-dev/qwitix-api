namespace qwitix_api.Core.Helpers
{
    public class PaginationResponse<T>
    {
        public required IEnumerable<T> Items { get; set; }
        public required bool HasNextPage { get; set; }
        public required int TotalCount { get; set; }
    }
}
