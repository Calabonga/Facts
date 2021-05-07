using System.IO;
using System.Text;

namespace Calabonga.Facts.Web.Controllers.Facts
{
    public class StringWriterWithEncoding : StringWriter
    {
        public StringWriterWithEncoding(Encoding encoding) =>
            Encoding = encoding;

        public override Encoding Encoding { get; }
    }
}