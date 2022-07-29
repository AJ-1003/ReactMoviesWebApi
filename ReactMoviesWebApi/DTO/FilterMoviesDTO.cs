﻿namespace ReactMoviesWebApi.DTO
{
    public class FilterMoviesDTO
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public PaginationDTO PaginationDTO
        {
            get
            {
                return new PaginationDTO()
                {
                    Page = Page,
                    RecordsPerPage = RecordsPerPage
                };
            }
        }
        public string Title { get; set; }
        public Guid GenreId { get; set; }
        public bool InTheaters { get; set; }
        public bool UpcomingReleases { get; set; }
    }
}
