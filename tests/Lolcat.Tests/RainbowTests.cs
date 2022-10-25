namespace Lolcat.Tests;

public class RainbowTests
{
    [Fact]
    public void Convert_UsingAnsiEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Ansi, Seed: 42);
        var lolcat = new Rainbow(style);
        const string text = "Somewhere over the rainbow...";
        const string expected = "[38;2;3;170;210;1mS[0m[38;2;2;174;206;1mo[0m[38;2;1;178;203;1mm[0m[38;2;1;182;199;1me[0m[38;2;1;186;196;1mw[0m[38;2;1;190;192;1mh[0m[38;2;1;193;189;1me[0m[38;2;1;197;185;1mr[0m[38;2;1;200;181;1me[0m[38;2;1;204;177;1m [0m[38;2;2;207;173;1mo[0m[38;2;3;210;169;1mv[0m[38;2;4;214;165;1me[0m[38;2;5;217;161;1mr[0m[38;2;6;220;157;1m [0m[38;2;7;222;153;1mt[0m[38;2;8;225;149;1mh[0m[38;2;10;228;145;1me[0m[38;2;12;230;140;1m [0m[38;2;13;233;136;1mr[0m[38;2;15;235;132;1ma[0m[38;2;17;237;128;1mi[0m[38;2;20;239;124;1mn[0m[38;2;22;241;119;1mb[0m[38;2;24;243;115;1mo[0m[38;2;27;245;111;1mw[0m[38;2;29;246;107;1m.[0m[38;2;32;248;103;1m.[0m[38;2;35;249;98;1m.[0m";

        var converted = lolcat.Convert(text);

        converted.Should().Be(expected);
    }

    [Fact]
    public void Convert_UsingSpectreEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: 42);
        var lolcat = new Rainbow(style);
        const string text = "Somewhere over the rainbow...";
        const string expected = "[rgb(3,170,210)]S[/][rgb(2,174,206)]o[/][rgb(1,178,203)]m[/][rgb(1,182,199)]e[/][rgb(1,186,196)]w[/][rgb(1,190,192)]h[/][rgb(1,193,189)]e[/][rgb(1,197,185)]r[/][rgb(1,200,181)]e[/][rgb(1,204,177)] [/][rgb(2,207,173)]o[/][rgb(3,210,169)]v[/][rgb(4,214,165)]e[/][rgb(5,217,161)]r[/][rgb(6,220,157)] [/][rgb(7,222,153)]t[/][rgb(8,225,149)]h[/][rgb(10,228,145)]e[/][rgb(12,230,140)] [/][rgb(13,233,136)]r[/][rgb(15,235,132)]a[/][rgb(17,237,128)]i[/][rgb(20,239,124)]n[/][rgb(22,241,119)]b[/][rgb(24,243,115)]o[/][rgb(27,245,111)]w[/][rgb(29,246,107)].[/][rgb(32,248,103)].[/][rgb(35,249,98)].[/]";

        var converted = lolcat.Convert(text);

        converted.Should().Be(expected);
    }
}
