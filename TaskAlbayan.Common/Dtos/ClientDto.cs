namespace TaskAlbayan.Common
{
    public class ClientDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }


        public string Phone { get; set; }

        public string? Notes { get; set; }
        public List<VisitDto> visits { get; set; }
    }
}