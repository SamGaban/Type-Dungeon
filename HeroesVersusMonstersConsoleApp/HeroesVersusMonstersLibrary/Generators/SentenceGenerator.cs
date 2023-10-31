using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Generators
{
    public static class SentenceGenerator
    {

        public static List<string> starters = new List<string>
        {
            "A", "The", "When", "While", "Before", "After", "Since", "Because", "Although", "Despite", "During", "Given", "However", "Indeed", "Moreover", "Nonetheless", "Rather", "So", "Therefore", "Thus", "Unless", "Until", "Yet", "Especially", "Ironically", "Obviously", "Particularly", "Similarly", "Likewise", "Alternatively", "Consequently", "Notably", "Surprisingly", "Unfortunately", "Additionally", "Arguably", "Briefly", "Generally", "Historically", "Interestingly", "Namely", "Presumably"
        };

        public static List<string> words35 = new List<string>
        {
            "aide", "bark", "cane", "dart", "elk", "find", "gory", "hype", "idle", "jazz", "knee", "lame", "mild", "nose", "opus", "pace", "quip", "rang", "sage", "tile", "urge", "vibe", "wane", "xeno", "yawn", "zone", "able", "bide", "clap", "dive", "easy", "flee", "grip", "hoax", "icon", "jerk", "knit", "lair", "mote", "nail", "oboe", "pane", "quip", "rant", "seal", "tune", "urns", "void", "wolf", "yolk", "zoom", "acid", "bore", "claw", "dock", "earn", "flip", "glue", "hurt", "iris", "jolt", "kale", "lace", "mule", "navy", "ooze", "palm", "quit", "rave", "skim", "trap", "urge", "veil", "warp", "yoga", "zeal", "aged", "bolt", "chef", "dull", "east", "fume", "gaze", "hive", "itch", "jump", "kite", "lone", "mute", "neat", "oval", "purr", "quid", "reef", "soar", "twig", "user", "vial", "wick", "yarn", "zest", "amen", "buzz", "chip", "deck", "emit", "foal", "glow", "hurl", "jinx", "kept", "link", "moan", "newt", "oxen", "peak", "quad", "rind", "spur", "tide", "veer", "wilt", "yank", "zero", "army", "bump", "cite", "drum", "elms", "fist", "gale", "horn", "isle", "jazz", "king", "luck", "moss", "numb", "ogle", "pale", "quiz", "ramp", "spin", "toil", "vast", "wolf", "yang", "zinc", "asks", "buns", "clot", "daze", "eggs", "flaw", "goat", "haze", "icon", "jute", "kiwi", "loft", "moot", "nape", "oats", "puck", "quay", "rift", "silk", "tuck", "void", "wisp", "yoga", "zaps", "atom", "bale", "cola", "dots", "etch", "flux", "gnaw", "hulk", "inch", "jest", "knot", "lush", "mild", "nook", "oops", "plug", "quid", "rake", "slam", "twin", "veto", "womb", "yule", "zany", "oval", "puma", "quad", "rust", "swim", "twig", "urns", "vamp", "waxy", "yelp", "zips", "aqua", "bawl", "cyst", "dupe", "echo", "fizz", "grid", "hewn", "imps", "jibe", "kelp", "luxe", "muse", "neap", "oily", "puke", "quip", "roam", "snug", "toll", "urge", "vein", "wisp", "yawn", "zinc", "avid", "brim", "cork", "dyed", "etch", "foul", "grit", "heap", "isle", "jolt", "keys", "lisp", "mugs", "nibs", "opts", "pout", "quay", "ripe", "smug", "tuna", "vibe", "wane", "yang", "zone", "akin", "blip", "cone", "drab", "eels", "firm", "gawk", "hazy", "iris", "jive", "kohl", "limp", "moor", "null", "onus", "palm", "quiz", "reek", "sift", "toil", "vial", "wary", "yolk", "zest", "alee", "blob", "cove", "dusk", "expo", "flap", "glib", "hewn", "idle", "judo", "kept", "lode", "mend", "naps", "ogle", "puck", "quid", "rile", "slur", "tabs", "urns", "volt", "wisp", "yarn", "zany"
        };

        public static List<string> words58 = new List<string>
        {
            "abrupt", "beacon", "candid", "dragon", "eleven", "flinch", "gravel", "herald", "impact", "jungle", "kettle", "lament", "morsel", "nimble", "obtain", "plunge", "quorum", "resist", "stifle", "tangle", "uphold", "voyage", "wobble", "xenial", "yellow", "zealot", "agenda", "bunion", "chance", "dabble", "enrage", "fumble", "guzzle", "humble", "ignite", "jumble", "keenly", "linger", "muzzle", "nuzzle", "output", "prance", "quench", "relate", "scenic", "thrift", "unlock", "verbal", "wrench", "yonder", "zigzag", "accuse", "baffle", "commit", "defend", "effort", "gambit", "hijack", "invoke", "jigsaw", "knives", "lament", "mimosa", "nugget", "oblige", "parade", "quaint", "remote", "snatch", "travel", "uptake", "vexing", "wisdom", "yearly", "zodiac", "absorb", "brisket", "crucial", "dictate", "expire", "foster", "gritty", "herald", "invert", "jargon", "kindred", "lizard", "minion", "nibble", "orient", "placid", "quiver", "revolt", "squirm", "tinker", "unwind", "vortex", "wobble", "xylem", "yogurt", "zephyr", "banana", "clinic", "delete", "elapse", "fizzle", "gaggle", "harbor", "injure", "jingle", "knobby", "lather", "matrix", "noodle", "oscill", "pickle", "quasar", "result", "shrink", "trophy", "unique", "vector", "wombat", "xyloph", "yokel", "zenith", "attain", "barter", "convey", "digest", "expose", "famine", "gander", "hinder", "intend", "jumper", "kidnap", "locate", "meddle", "narrow", "outrun", "pistol", "quench", "resume", "sturdy", "trench", "unfold", "violet", "waddle", "yarns", "zombie", "balloon", "cluster", "diamond", "escapes", "frolics", "giblets", "hankies", "implant", "jackets", "kernels", "lingers", "modules", "napkins", "outlook", "puzzles", "quibble", "roscoes", "splints", "tickles", "unfurls", "vanilla", "whimpers", "xyloids", "yodeler", "zeppelins", "blazing", "crinkles", "doodles", "fajitas", "gracing", "hurdles", "indulge", "juvenile", "kettles", "limerick", "morning", "narrate", "octopus", "plaster", "quizzes", "rinsing", "springs", "turnips", "vagrant", "wizards", "xeroxed", "yodeling", "zeppelin", "bandits", "crumple", "dribble", "flaming", "grumble", "honking", "intrude", "junkies", "knuckle", "lobster", "muscles", "nudges", "optical", "pastrami", "quartet", "ranting", "sputnik", "twinkle", "virtual", "wrinkle", "xylophone", "yowling", "zombies", "bumpkin", "canteen", "drowsy", "fritter", "gyrated", "huffing", "inquire", "jogging", "kinship", "lethargy", "mankind", "nesting", "obscure", "parfait", "quokkas", "rockets", "scruffy", "trivial", "upswing", "vintage", "wobbles", "xenophobic", "yawning", "zealots", "burning", "captain", "dwindle", "frosted", "giggles", "hovered", "inertia", "jazzing", "kissing", "letters", "mystify", "neurons", "oatmeal", "piranha", "qualify", "rejoice", "stymied", "tumbles", "urinate", "vendors", "wiggles", "xeroxes", "yielded", "zeppelins"
        };

        public static List<string> words812 = new List<string>
        {
            "absolutely", "backpacking", "complicated", "determination", "exaggerate", "fluctuation", "gargantuan", "humiliation", "intolerable", "juxtaposition", "kleptomaniac", "legislation", "magnificent", "necessitate", "optimization", "procrastinate", "qualification", "restoration", "spontaneous", "thermodynamic", "unbelievable", "vegetarianism", "waterproofing", "xenophobically", "yesteryear", "zoological", "asymmetrical", "bioengineering", "controversial", "discombobulate", "encapsulation", "fingerprinting", "geographically", "hypothetically", "interconnected", "jurisdictional", "knowledgeable", "labyrinthine", "microprocessor", "nonconformists", "overcompensate", "photosynthesis", "quintessential", "revolutionary", "sustainability", "terminological", "unquestionable", "vivisectionist", "whippersnapper", "xylophonically", "yachtsmanship", "zeppelinography", "anthropology", "blunderbuss", "conversation", "disinterested", "enlightenment", "fortuneteller", "gobbledygook", "hallucination", "imperceptible", "juxtaposition", "kleptomaniac", "lackadaisical", "misconception", "nondescript", "overexposure", "psychotherapy", "quartermaster", "radioactivity", "serendipitous", "troubleshooter", "underestimated", "visualization", "waterboarding", "xenophobic", "youthfulness", "zoologist", "astronomical", "breakthrough", "circumference", "dysfunctional", "extraordinary", "flabbergasted", "gratuitously", "homeostasis", "indistinguishable", "justification", "kaleidoscopic", "longitudinal", "miscalculation", "necessities", "overindulgence", "pharmaceutical", "qualitative", "reprehensible", "subterranean", "thermodynamics", "unpredictable", "vernacular", "wonderstruck", "xylophonist", "yellowish", "zealotry"
        };

        public static string Generate(int numberofwords, Entity entity)
        {
            Random rnd = new Random();
            string? finalString = "";
            finalString += starters[rnd.Next(0, starters.Count())];
            finalString += " ";
            switch (entity.Race)
            {
                case "Wolf":
                    for (int i = 0; i < numberofwords; i++)
                    {
                        finalString += words35[rnd.Next(0, words35.Count())];
                        finalString += " ";
                    }
                    break;
                case "Orc":
                    for (int i = 0; i < numberofwords; i++)
                    {
                        finalString += words58[rnd.Next(0, words58.Count())];
                        finalString += " ";
                    }
                    break;
                case "Dragonling":
                    for (int i = 0; i < numberofwords; i++)
                    {
                        finalString += words812[rnd.Next(0, words812.Count())];
                        finalString += " ";
                    }
                    break;
            }
            finalString += words35[rnd.Next(0, words35.Count())];
            finalString += (rnd.Next(1, 3) == 1) ? "." : "!"; 

            return finalString;
        }
    }
}
