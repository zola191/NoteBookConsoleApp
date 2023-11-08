using Newtonsoft.Json;
using System.Globalization;

namespace NoteBookApp.Attributes
{
    public class DataTimeCustomConverter : JsonConverter<DateTime>
    {
        private const string Format = "dd:MM:yyyy";
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            //throw new NotImplementedException();
            if (reader.Value == null)
            {
                return default(DateTime);
            }

            var s = reader.Value.ToString();
            DateTime result;
            var x = DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            var y = DateTime.TryParseExact(s, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            if (DateTime.TryParseExact(s, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("dd:MM:yyyy"));
        }
    }
}
