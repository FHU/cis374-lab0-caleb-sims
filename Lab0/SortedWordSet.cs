
namespace Lab0;

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

    public string? Next(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0 || words.Count == 0)
            return null;
        
        // var wordsInRange = words.GetViewBetween("a","m");
        // words.GetViewBetween()

        foreach(var candidate in words.GetViewBetween(normalizedWord, MaxString))
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

        string lo = normalizedPrefix;
        string hi = normalizedPrefix + "{";

        int count = 0;

        foreach(var candidate in words.GetViewBetween(lo, hi))
        {
            results.Add(candidate);
            count++;
            if (count >=k)
            {
                return results;
            }
        }

        return results;
    }

    public string? Prev(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0 || words.Count == 0)
            return null;
        
        // var wordsInRange = words.GetViewBetween("a","m");
        // words.GetViewBetween()

        string? best = null;

        foreach(var candidate in words.GetViewBetween("", normalizedWord))
        {
            if (candidate.CompareTo(normalizedWord) < 0)
            {
                best = candidate;
            }
        }
        return best;
    }

    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        var nlo = Normalize(lo);
        var nhi = Normalize(hi);

        var results = new List<string>();
        foreach (var word in words.GetViewBetween(nlo,nhi))
        {
            results.Add(word);
            if (results.Count >= k)
                break;
        }
        
        return results;
    }

    public bool Remove(string word)
    {
        var normalizedWord = Normalize(word);
        if (normalizedWord.Length == 0)
            return false;

        return words.Remove(normalizedWord);
    }

    private string Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return string.Empty;
        }

        return word.Trim().ToLowerInvariant();
    }

    private const string MaxString = "\uFFFF\uFFFF\uFFFF";
}