using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using KeePassLib;
using KeePassLib.Cryptography;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;

namespace Cockroach
{
    public sealed class CockroachGen:CustomPwGenerator
    {
        private  string[][] m_strSpecialCharSet = new string[][]
        {
            new string[]{"abacus","abbess","abbot","abyss","action","afloat","ageold","aisle","aka","akimbo","albino","album","algae","alder","alike","allot","ally","also","aloof","alumna","ameba","amok","anal","angle","appear","aroma"},
            new string[]{"baccy","badge","bait","ball","banish","bank","barge","barium","barren","basque","beaker","beckon","behave","berry","beyond","biopsy","black","bliss","bobble","bonus","bounce","brain","breezy","brief","bright","brisk"},
            new string[]{"caller","candid","cantab","carbon","cartel","cash","catch","catty","cause","cease","cerise","chair","chain","change","charm","child","chuck","clergy","cleric","close","cock","cocoa","cold","combat","come","cover"},
            new string[]{"darken","death","debt","deed","decoy","defeat","defer","deify","demon","demean","dense","depict","design","deuce","dilate","dildo","dinky","dirty","disbar","dismay","distil","dizzy","divot","dodge","dog","domain"},
            new string[]{"ember","emit","emote","engage","enlist","entire","equal","erect","erode","europe","exact","exotic","expend","exude","exult","eye","eyelet","eyrie","extra","export","explore","expire","exit","exile","exhale","excuse"},
            new string[]{"fabric","fabled","face","facet","fail","faint","faggot","fairy","faith","fake","falter","fanny","fart","fasten","father","fear","feed","fend","ferry","figure","filth","finger","first","five","flag","flare"},
            new string[]{"gaily","galley","gauge","gee","geld","get","ghost","girl","glitz","global","gloss","gonna","golf","goo","good","goofy","grade","grand","grate","grasp","green","groovy","gross","grudge","guess","guitar"},
            new string[]{"habit","hamper","hamlet","handle","happen","hard","harden","harrow","hark","hatch","hazard","heady","heal","hear","heath","hedge","hack","hew","hex","high","hire","hole","home","hoop","hoot","horn"},
            new string[]{"ilk","image","imply","impose","inch","income","indeed","incur","indian","indict","induct","info","inform","inhale","ink","insert","insult","inter","into","invest","invoke","invite","issue","itch","ivory","itself"},
            new string[]{"jack","jade","jazz","jaunt","joker","judge","juice","jump","jury","jowl","joy","judder","jail","jangle","jar","jargon","jaw","jerk","jesus","jetlag","jibe","jewel","job","jolly","jotter","joypad"},
            new string[]{"karma","keep","kebab","ken","key","kick","kidnap","kind","kilt","kith","kitty","kiwi","kink","knack","knight","knit","knob","knock","knoll","know","koala","kohl","korean","kosher","krill","kraut"},
            new string[]{"label","labour","labia","lace","lackey","laden","long","lousy","lunch","lapse","large","lark","last","latch","lash","live","latin","latter","lawn","leafy","leak","lean","learn","league","leanto","leap"},
            new string[]{"man","mar","market","meadow","moan","media","mete","milky","miner","module","mound","muddle","muck","murky","mush","myopia","mutton","mute","myrrh","muzzle","muster","multi","muff","mug","muddy","much"},
            new string[]{"nadir","naive","neck","nestle","next","nibble","nobody","nipple","noisy","nomad","nurse","nymph","nutty","nudge","nudity","nth","nub","now","novel","notice","note","notch","not","norman","noose","noble"},
            new string[]{"oath","obey","oboist","occur","octet","oddly","odour","offend","often","oily","olive","onion","opaque","oomph","oral","ordain","order","ornate","ossify","ounce","oust","outfit","over","overt","owlish","oyster"},
            new string[]{"pacify","pack","paean","pager","pander","pantry","parcel","perk","party","pass","pedal","penny","perm","period","pesky","persue","phial","phobia","piano","pidgin","pillow","pistol","plain","plaza","pluck","ploy"},
            new string[]{"Quack","Quad","Quake","Quango","Quanta","Quark","Quarry","Quartz","Quash","Queen","Query","Quiet","Quill","Quilt","Quip","Quite","Quiver","Quiz","Quota","Quote","Quoth","Qwerty","Quiz","Quirk","Quint","Quick"},
            new string[]{"radio","ragged","raise","random","rank","rarity","ray","reach","reader","reap","rebut","render","rig","ripen","rosin","royal","ruck","rye","rumpus","ruddy","row","rouse","rout","rough","rotten","rose"},
            new string[]{"sacred","sadden","sample","satin","savvy","saxon","scale","scalp","scatty","scorch","scull","second","seethe","self","sepal","septic","serial","serum","shop","sewage","sicko","sieve","silage","simper","sitter","six"},
            new string[]{"tap","taunt","tattoo","tasty","tax","testy","thine","thong","thud","timbre","titch","token","toll","trace","trail","trawl","trot","tunny","tutor","two","tyre","tzar","twixt","twig","turtle","tuna"},
            new string[]{"ugly","ulcer","unbend","unborn","uncle","uncut","under","undo","unduly","uneasy","uneven","unfold","unhurt","unify","union","unique","unit","unison","unsold","untidy","untie","until","unveil","uphill","usurp","usual"},
            new string[]{"vacate","vagina","vague","vain","valid","vamp","varied","vary","vat","vault","veep","vegan","vein","velour","vendor","venus","vermin","vernal","venom","vertex","veto","vino","vinyl","virgo","vizier","voile"},
            new string[]{"wagon","wail","wank","warp","weapon","wedge","welly","whoop","wind","wither","woe","wordy","worm","wick","whup","wicket","whirl","whim","wheeze","whelk","whack","welt","weepy","weft","wave","water"},
            new string[]{"xchrom","xeno","xerox","xion","xray","xmas","xrated","xylo","xphone","xifoo","xfooth","xbig","xdge","xirl","xeriod","xford","xbag","xfind","xnono","xpig","xting","xnstsi","xbia","xsome","xmoso","xnova"},
            new string[]{"yacht","yak","yank","yap","yard","yarn","yawn","year","yell","yen","yoga","yokeee","yikes","youth","yuppie","yuck","yule","yummy","yokel","yogurt","yogi","yippee","yodel","yer","yonks","yore"},
            new string[]{"zany","zap","zeal","zealot","zapper","zen","zero","zigzag","zilch","zinc","zing","zip","zipper","zit","zither","zodiac","zombie","zonal","zone","zonked","zoo","zoom","zulu","zydeco","zkouk","zlogy"}
        };       

        private static readonly PwUuid m_uuid = new PwUuid(new byte[] {
			0x53, 0x81, 0x36, 0xCE, 0xAE, 0xFC, 0x48, 0x3F,
			0x9E, 0x90, 0xA4, 0x4F, 0x1A, 0xF0, 0x58, 0x95 });
        public override PwUuid Uuid
        {
            get { return m_uuid; }
        }

        public override string Name
        {
            get { return "Cockroach"; }
        }

        public override ProtectedString Generate(PwProfile prf, CryptoRandomStream crsRandomSource)
        {
            if (prf == null) { Debug.Assert(false); }
            else
            {
                Debug.Assert(prf.CustomAlgorithmUuid == Convert.ToBase64String(
                    m_uuid.UuidBytes, Base64FormattingOptions.None));
            }

            Random keyi = new Random((int)crsRandomSource.GetRandomUInt64());

            string strPw;
            string strSpecialLjm = "ljm";
            string strSpecialDate = DateTime.Now.ToString("yyMMdd");

            string strSpecialWord = m_strSpecialCharSet[keyi.Next(26)][keyi.Next(26)];
            strPw = strSpecialLjm + strSpecialDate +strSpecialWord;            
            
            return new ProtectedString(false, strPw);
        }
    }
}
