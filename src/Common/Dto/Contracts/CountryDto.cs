namespace Common.Dto.Contracts
{
    public class CountryDto
    {
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public string? ISO3 { get; set; }
        public string? ISO2 { get; set; }
        public int? NumericCode { get; set; }
        public int OrderNo { get; set; }
        public string? PhoneCode { get; set; }
        public string? Currency { get; set; }
        public string? CurrencyName { get; set; }
        public string? EmojiU { get; set; }
        public string? CurrencySymbol { get; set; }
    }
}
