namespace LivrosQueJaLi.Models
{
    public class Book
    {
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }

        private SaleInfo _saleInfo;
        public SaleInfo SaleInfo
        {
            get
            {
                if (_saleInfo == null)
                    _saleInfo = new SaleInfo()
                    {
                        RetailPrice = new RetailPrice() { Amount = 0F, CurrencyCode = "Valor Indisponível." }
                    };

                return _saleInfo;
            }
            set { _saleInfo = value; }
        }
    }

    public class VolumeInfo
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Authors { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                var vl = value?.Replace("<br>", "\n")
                    .Replace("<p>", "\t").Replace(@"<\p>", "");
                _description = vl;
            }
        }

        public int? PageCount { get; set; }

        ReadingModes _readingModes;
        public ReadingModes ReadingModes
        {
            get { return _readingModes; }
            set
            {
                if (value != null && !value.Image)
                {
                    ImageLinks = new ImageLinks();
                    _readingModes = value;
                }
            }
        }

        public ImageLinks ImageLinks { get; set; }

        private string _language;
        public string Language
        {
            get { return _language?.ToUpper(); }
            set { _language = value?.ToUpper(); }
        }
    }

    public class ReadingModes
    {
        public bool Text { get; set; }
        public bool Image { get; set; }
    }

    public class ImageLinks
    {
        string _thumbnail;
        public string Thumbnail
        {
            get
            {
                if (string.IsNullOrEmpty(_thumbnail))
                    return "noimage_128_183.png";

                return _thumbnail;
            }
            set { _thumbnail = value; }
        }

        string _small;
        public string Small
        {
            get
            {
                if (string.IsNullOrEmpty(_small))
                    return "noimage_300_418.png";

                return _small;
            }
            set { _small = value; }
        }
    }

    public class SaleInfo
    {
        public RetailPrice RetailPrice { get; set; }
    }

    public class RetailPrice
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
