using Xunit;

namespace ExampleLib.UnitTests;

public class TextUtilTest
{
    [Fact]
    public void CanExtractRussianWords()
    {
        const string text = """
                            Играют волны — ветер свищет,
                            И мачта гнётся и скрыпит…
                            Увы! он счастия не ищет
                            И не от счастия бежит!
                            """;
        List<string> expected =
        [
            "Играют",
            "волны",
            "ветер",
            "свищет",
            "И",
            "мачта",
            "гнётся",
            "и",
            "скрыпит",
            "Увы",
            "он",
            "счастия",
            "не",
            "ищет",
            "И",
            "не",
            "от",
            "счастия",
            "бежит",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanExtractWordsWithHyphens()
    {
        const string text = "Что-нибудь да как-нибудь, и +/- что- то ещё";
        List<string> expected =
        [
            "Что-нибудь",
            "да",
            "как-нибудь",
            "и",
            "что",
            "то",
            "ещё",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanExtractWordsWithApostrophes()
    {
        const string text = "Children's toys and three cats' toys";
        List<string> expected =
        [
            "Children's",
            "toys",
            "and",
            "three",
            "cats'",
            "toys",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanExtractWordsWithGraveAccent()
    {
        const string text = "Children`s toys and three cats` toys, all of''them are green";
        List<string> expected =
        [
            "Children`s",
            "toys",
            "and",
            "three",
            "cats`",
            "toys",
            "all",
            "of'",
            "them",
            "are",
            "green",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(GetFormattedEasternArabics))]
    public void FormatEasternArabic_ShouldReturnCorrectResult(int value, string expected)
    {
        string actual = TextUtil.FormatEasternArabic(value);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<int, string> GetFormattedEasternArabics()
    {
        return new TheoryData<int, string>
        {
            {
                1234567890, "١٢٣٤٥٦٧٨٩٠"
            },
            {
                -1234567890, "-١٢٣٤٥٦٧٨٩٠"
            },
            {
                int.MaxValue, "٢١٤٧٤٨٣٦٤٧"
            },
            {
                int.MinValue, "-٢١٤٧٤٨٣٦٤٨"
            },
        };
    }
}