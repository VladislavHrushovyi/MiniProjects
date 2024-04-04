namespace AlgorithmTasks._26_50;

public class SubstringWithConcatenationOfAllWords
{
    public void Execute()
    {
        var sol = new Solution();
        var result = sol.FindSubstring("hmtsvuabdjitpfehczyfliomwqkelhcstxmrsmuxxcrohxwjzuqkegwawigjskdliqeaeyzcasskqovpkmzosyqriwaaheoruutytscvlvhbldqyprojrfacdbekfsbbpwzkpefazpdmsvkjmzsgevkhwoudjxeyufijjplkeoxwlhhzgafmvpwtgqyppcpljxwvohftxcrimzisbphyhdtnvremvtyequzeefzdjtmvuwqvlxvucohzfnlagleyggfmnepwxbsrzionhjhjzrkeevelhljptobgvscuuwovgcinoqynjiungaapnkggajmxbrsxpitwmblatkqcumqcmsraczifixemowjfecbrgptbzlevxwykormttuadrephdznhdqalfwnghtefperfvdaqzaurhjykkntoeuzlsoszhwgyazgxegqejkvsvvcglvuprvclmwlvutuutcuzkkdtwalrypxjntaercqfzdatywqtymtjyweuxsnvhnguggbzitejyfdwdvqchjlfhrbgjodrbjnkpqlyjqhjxmgahyeqjpvqhyxspclqmzcbrobdgjmdxufhskxdbvukttdivobhpflhtmmzxyevtkenwbrxynksincfdfntlacemogkokhjwlxumqsldefltjqwrfrefvkulqeadljzwiukyoxbseonqhjhoblwyvqkoukrhvsfempccpgwtujjbtsswdowbegpasghoymsndupsaomxxxasqsqvgpwkvnkovtcufwjlqadtorqorqihlberqdclqobmifipbrasrenixfyovgxgjcazhenxsolcuvsvmupgaeqyjoohwopgspfztuyojksffmcuhuchyuhiiypgmgmewudrobfznygvklrtfvbfpkpzigfwnuajqdaehigwiotuzkogfrmwbdvmkdixxjinoktxsbwcqpxralyhcmefpbrbecydnisdtmbnpxqfopxoaxycjehafbcmdarqlkvgywtnvltmasacpaeaeoamrcawfrosdjybgtpdfkpheskvuqvbgxpxcrvjijbotzpbsggzswjwqttmlqnsqcrstnbeyeurflszszzmxilpdebqxrinvcfrrixpmrjtcbswcrjbbuqgauxxlhmijrzcbwupndiqebmjsxkwtdcuxztkjgsuzuxbqrsgubwacklwkwudbxzayvkjdeecybfruyxqbvqfhebrawxdvydvtnfwtjbumingikwjhooousiuqfzndcizrpxlayhohuupgsbnjrddjlazszmyexxvmuipvpdclatruwedoijxvlzomnmqklmzfuoamrextapowvrkfckbplrcydsjqgivbyynrcmlcbzbzsnexzhmkyojdjutpcrscpfttugyxfbwaodxokjalajqjfmyhfrlwyfpunpstqovhtsvgdxrdhjmmxn",
            new[] { "xbqrsgubwacklwkwudbxzayvkjde","ltjqwrfrefvkulqeadljzwiukyox","ccpgwtujjbtsswdowbegpasghoym","tywqtymtjyweuxsnvhnguggbzite","vmkdixxjinoktxsbwcqpxralyhcm","gzswjwqttmlqnsqcrstnbeyeurfl","ycjehafbcmdarqlkvgywtnvltmas","fztuyojksffmcuhuchyuhiiypgmg","acpaeaeoamrcawfrosdjybgtpdfk","fdfntlacemogkokhjwlxumqsldef","mewudrobfznygvklrtfvbfpkpzig","fwtjbumingikwjhooousiuqfzndc","upndiqebmjsxkwtdcuxztkjgsuzu","pheskvuqvbgxpxcrvjijbotzpbsg","bmifipbrasrenixfyovgxgjcazhe","cufwjlqadtorqorqihlberqdclqo","bseonqhjhoblwyvqkoukrhvsfemp","sndupsaomxxxasqsqvgpwkvnkovt","yjqhjxmgahyeqjpvqhyxspclqmzc","fwnuajqdaehigwiotuzkogfrmwbd","nxsolcuvsvmupgaeqyjoohwopgsp","brobdgjmdxufhskxdbvukttdivob","szszzmxilpdebqxrinvcfrrixpmr","ecybfruyxqbvqfhebrawxdvydvtn","jyfdwdvqchjlfhrbgjodrbjnkpql","jtcbswcrjbbuqgauxxlhmijrzcbw","hpflhtmmzxyevtkenwbrxynksinc","efpbrbecydnisdtmbnpxqfopxoax" });
        Console.WriteLine($"{string.Join(',', result)}");
    }
}

partial class Solution {
    public IList<int> FindSubstring(string s, string[] words)
    {
        IList<int> result = new List<int>();
        if (string.IsNullOrEmpty(s) || words.Length == 0)
        {
            return result;
        }

        int wordlength = words[0].Length;
        int substrLength = wordlength * words.Length;
        if (substrLength > s.Length)
        {
            return result;
        }

        for (int i = 0; i < s.Length - substrLength + 1; i+= 5)
        {
            var currSubStr = s.Substring(i, substrLength);
            foreach (var word in words)
            {
                currSubStr = ReplaceFirst(currSubStr, word, "");
                if (string.IsNullOrEmpty(currSubStr))
                {
                    result.Add(i);
                }
            }
        }
        return result;
    }

    private string ReplaceFirst(string originalText, string search, string replace)
    {
        bool isDeleteCurr = false;
        var parts = originalText.Chunk(search.Length);
        var withoutSearch = parts.Where(x =>
        {
            if (string.Join("", x) == search && !isDeleteCurr)
            {
                isDeleteCurr = true;
                return false;
            }

            return true;
        });

        return string.Join("", withoutSearch.Select(x => string.Join("", x)));
    }
}