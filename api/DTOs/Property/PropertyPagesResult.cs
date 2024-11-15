namespace api.DTOs.Property
{
    public class PropertyPagesResult
    {
        public PropertyPages Pages { get; set; } = new PropertyPages();
        public List<DisplayPropertyDTO> Properties { get; set; } = new List<DisplayPropertyDTO>();
    }
}