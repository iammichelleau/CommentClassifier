using System.Collections.Generic;

namespace CommentClassifierApi.Models
{
    public class Comment
    {
        public string Content { get; set; }

        public static readonly List<string> PositiveKeywords = new List<string> {"good", "great", "excellent", "amazing", "better", "cool",
            "love", "outstanding", "proper", "properly", "clear", "clearly", "useful",
            "fast", "quick", "reliable", "impressive", "fabulous", "intuitive"};
        public static readonly List<string> NegativeKeywords = new List<string> {"dislike", "hate", "bad", "terrible", "issue", "issues", "problem", "problems",
            "unable", "incapable", "useless", "irritating", "disappointing", "disappointed",
            "annoy", "annoying", "annoyed", "frustrating", "frustrated", "cumbersome",
            "confusing", "confused", "slow", "malfunction", "malfunctions", "improvement"};
        public static readonly List<string> NeutralKeywords = new List<string> {"work", "working", "space",
            "display", "see", "view", "use", "help", "recommend"};

        public enum Output
        {
            Excellent,
            Good,
            Neutral,
            NeedsImprovement,
            Bad,
            Unclear
        }
    }
}
