using System.Text.Json.Serialization;

#nullable disable
namespace CPS.Models
{

    public class HideColorCode
    {

    }

    public class CalendarList
    {
        [JsonPropertyName("hexaDecimal")]
        public string ColorCode { get; set; }
       
        public int ParentID { get; set; }
        public string Status { get; set; }
        public int TagID { get; set; }
        public string Type { get; set; }
        public int calendarID { get; set; }
        public string calendarName { get; set; }

        public bool ShouldSerializeColorCode()
        {
            return false; // Ensure that the ColorCode property is not serialized
        }
    }
}
