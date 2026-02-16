namespace Lab0;
// [  "beau", "beautiful", "cale", "caleb","cecily", "cephas", "cervid" 
// "ethan" , "ethel", "rhino", "ryan", "rye"]

public class SortedWordSet : IWordSet
{
    private SortedSet<string> words = new();

    public int Count => words.Count;

    public bool Add(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0)
            return false;

        return words.Add(normalizedWord);
    }

    public bool Contains(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0)
            return false;

        return words.Contains(normalizedWord);
    }

    public bool Remove(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0)
            return false;

        return words.Remove(normalizedWord);
    }

    // TODO
    public string? Prev(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0 || words.Count == 0)
            return null;

        // var wordsInRange = words.GetViewBetween("a","m");
        // words.GetViewBetween()

        string? best = null;

        foreach (var candidate in words.GetViewBetween("", normalizedWord))
        {
            if (candidate.CompareTo(normalizedWord) < 0)
            {
                best = candidate;
            }
        }
        return best;
    }

    public string? Next(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0 || words.Count == 0)
            return null;

        //var wordsInRange = words.GetViewBetween("a", "m");
        //words.GetViewBetween("charity" , "\uFFFF\uFFFF\uFFFF")

        foreach (var candidate in words.GetViewBetween(normalizedWord, MAX_STRING))
        {
            if (candidate.CompareTo(normalizedWord) > 0)
            {
                return candidate;
            }
        }
        return null;
    }

    public IEnumerable<string> Prefix(string prefix, int k)
    {
        if (k <= 0 || words.Count == 0)
            return new List<string>();

        var results = new List<string>();

        var normalizedPrefix = Normalize(prefix);
        if(normalizedPrefix.Length == 0)
            return words;

        // do the work
        int count = 0;
        string lo = normalizedPrefix;
        string hi = normalizedPrefix + "z";

        foreach (var candidate in words.GetViewBetween(lo, hi))
        {
            results.Add(candidate);
            count++;
            if (count >= k)
                break;
        }

        return results;
    }


    // TODO
    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        var nlo = Normalize(lo);
        var nhi = Normalize(hi);

        var results = new List<string>();
        foreach (var word in words.GetViewBetween(nlo, nhi))
        {
            results.Add(word);
            if (results.Count >= k)
                break;
        }

        return results;
    }


    /// <summary>
    /// Normalize a word by trimming whitespace and converting to lowercase.
    /// </summary>
    /// <param name="word">The word to normalize.</param>
    /// <returns>The normalized word.</returns>
    private string Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return string.Empty;

        // Trim and lowercase 
        return word.Trim().ToLowerInvariant();
    }

    private const string MAX_STRING = "\uFFFF\uFFFF\uFFFF";

}