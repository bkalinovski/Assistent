namespace VAssistent.Assistent.API.Models
{
    public class SpeechToTextResponse
    {
        public string RecognitionStatus { get; set; }
        public string DisplayText { get; set; }
        public string Offset { get; set; }
        public string Duration { get; set; }
    }
}
