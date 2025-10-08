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

    //TOOD параметризованный тест
    [Fact]
    public void CanFormatPositiveEasternArabic()
    {
        int value = 1234567890;
        string expected = "١٢٣٤٥٦٧٨٩٠";

        string actual = TextUtil.FormatEasternArabic(value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanFormatNegativeEasternArabic()
    {
        int value = -1234567890;
        string expected = "-١٢٣٤٥٦٧٨٩٠";

        string actual = TextUtil.FormatEasternArabic(value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanFormatMaxEasternArabic()
    {
        int value = int.MaxValue;
        string expected = "٢١٤٧٤٨٣٦٤٧";

        string actual = TextUtil.FormatEasternArabic(value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanFormatMinEasternArabic()
    {
        int value = int.MinValue;
        string expected = "-٢١٤٧٤٨٣٦٤٨";

        string actual = TextUtil.FormatEasternArabic(value);
        Assert.Equal(expected, actual);
    }
}