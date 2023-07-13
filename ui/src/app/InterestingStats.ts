const InterestingFacts: string[] = [
    'Für Leser, die manchmal abschweifen, benötigt es 8 bis 9 Monate die Bibel vollständig zu lesen. (Bei 30min pro Tag und 5 Tage pro Woche)',
    'Während der Entwicklung, hieß die Webseite noch \'Gemeinschaftskochbuch\'',
];

const hfa = 'HFA';

class BibleVers {
    Text: string;
    Source: string;
    Translation: string;

    constructor(text: string, source: string, translation: string) {
        this.Text = text;
        this.Source = source;
        this.Translation = translation;
    }
}


const BibleVerses: BibleVers[] = [
    new BibleVers("Und Gott sprach: 'Siehe, ich habe euch alles samentragende Kraut gegeben, das auf der ganzen Erde wächst, und jeden Baum, an dem samentragende Baumfrüchte sind: das soll euch zur Nahrung dienen.'", "1. Mose 1,29", "HFA"),
    new BibleVers("Besser ein Gericht aus Gemüse, wo Liebe ist, als ein gemästeter Ochse, aber Hass dabei.", "Sprüche 15,17", "HFA"),
    new BibleVers("Schmecket und sehet, wie freundlich der HERR ist. Wohl dem, der auf ihn trauet!", "Psalm 34,9", "HFA"),
    new BibleVers("Wenn du dich zu Tische setzest mit einem Fürsten, so überlege sorgfältig, was vor dir steht, und stelle ein Messer an deine Kehle, wenn du voller Gier bist.", "Sprüche 23,1-2", "HFA"),
    new BibleVers("Nimm Weizen und Gerste, Bohnen und Linsen, Hirse und Dinkel und gib sie in ein Gefäß und mache Brot daraus. Du sollst es nach der Zahl der Tage essen, die du auf deine Seite liegst: dreihundertneunzig Tage.", "Hesekiel 4,9", "HFA"),
    new BibleVers("Ihr sollt euch nicht um Speise bemühen, die vergänglich ist, sondern um Speise, die da bleibt zum ewigen Leben, die der Sohn des Menschen euch geben wird; denn diesen hat Gott, der Vater, beglaubigt.", "Johannes 6,27", "HFA"),
    new BibleVers("Ein gütiger Mensch tut seiner eigenen Seele Gutes; aber ein Grausamer schadet seinem eigenen Fleisch.", "Sprüche 11,17", "HFA"),
    new BibleVers("Und vornehmt keine Vergesslichkeit der Gastfreundschaft; denn durch sie haben einige ohne ihr Wissen Engel beherbergt.", "Hebräer 13,2", "HFA"),
    new BibleVers("Unser tägliches Brot gib uns heute!", "Matthäus 6,11", "HFA"),
    new BibleVers("Und alles, was ihr tut mit Worten oder mit Werken, das tut alles im Namen des Herrn Jesus und dankt Gott, dem Vater, durch ihn.", "Kolosser 3,17", "HFA"),

    new BibleVers("Und Gott sprach: 'Siehe, ich habe euch alles samentragende Kraut gegeben, das auf der ganzen Erde wächst, und jeden Baum, an dem samentragende Baumfrüchte sind: das soll euch zur Nahrung dienen.'", "1. Mose 1,29", "Elberfelder"),
    new BibleVers("Besser ein Gericht aus Gemüse, wo Liebe ist, als ein gemästeter Ochse, aber Hass dabei.", "Sprüche 15,17", "Elberfelder"),
    new BibleVers("Schmecket und sehet, dass der HERR gütig ist; wohl dem Manne, der sich auf ihn verlässt!", "Psalm 34,9", "Elberfelder"),
    new BibleVers("Wenn du dich zu Tische setzest mit einem Herrscher, so bedenke wohl, wen du vor dir hast, und bringe dich dann in Zaum, wenn du voller Gier bist.", "Sprüche 23,1-2", "Elberfelder"),
    new BibleVers("Nimm Weizen und Gerste, Bohnen und Linsen, Hirse und Spelt, und gib sie in ein Gefäß und mache Brot daraus für dich; die Tage, an denen du auf deiner Seite liegst: dreihundertneunzig Tage, sollst du es essen.", "Hesekiel 4,9", "Elberfelder"),
    new BibleVers("Schaffet nicht die Speise, die vergänglich ist, sondern die Speise, die da bleibt zum ewigen Leben, welche der Sohn des Menschen euch geben wird; denn diesen hat Gott der Vater beglaubigt.", "Johannes 6,27", "Elberfelder"),
    new BibleVers("Die Seele des Segens wird reichlich getränkt, und wer erquickt, wird auch selbst erquickt.", "Sprüche 11,25", "Elberfelder"),
    new BibleVers("Vergesset nicht der Gastfreundschaft; denn durch diese haben einige ohne ihr Wissen Engel beherbergt.", "Hebräer 13,2", "Elberfelder"),
    new BibleVers("Unser tägliches Brot gib uns heute.", "Matthäus 6,11", "Elberfelder"),
    new BibleVers("Und alles, was ihr tut in Worten oder in Werken, tut alles im Namen des Herrn Jesus und danket Gott dem Vater durch ihn!", "Kolosser 3,17", "Elberfelder"),

    new BibleVers("And God said, 'Behold, I have given you every plant yielding seed that is on the face of all the earth, and every tree with seed in its fruit. You shall have them for food.'", "Genesis 1:29", "ESV"),
    new BibleVers("Better is a dinner of herbs where love is than a fattened ox and hatred with it.", "Proverbs 15:17", "ESV"),
    new BibleVers("Taste and see that the LORD is good; blessed is the one who takes refuge in him.", "Psalm 34:8", "ESV"),
    new BibleVers("When you sit to dine with a ruler, note well what is before you, and put a knife to your throat if you are given to gluttony.", "Proverbs 23:1-2", "ESV"),
    new BibleVers("Take wheat and barley, beans and lentils, millet and spelt; put them in a storage jar and use them to make bread for yourself.", "Ezekiel 4:9", "ESV"),
    new BibleVers("Do not work for the food that perishes, but for the food that endures to eternal life, which the Son of Man will give to you.", "John 6:27", "ESV"),
    new BibleVers("A generous person will prosper; whoever refreshes others will be refreshed.", "Proverbs 11:25", "ESV"),
    new BibleVers("Do not neglect to show hospitality to strangers, for thereby some have entertained angels unawares.", "Hebrews 13:2", "ESV"),
    new BibleVers("Give us today our daily bread.", "Matthew 6:11", "ESV"),
    new BibleVers("And whatever you do, whether in word or deed, do it all in the name of the Lord Jesus, giving thanks to God the Father through him.", "Colossians 3:17", "ESV"),

    new BibleVers("So God said, 'See, I have given you every herb that yields seed which is on the face of all the earth, and every tree whose fruit yields seed; to you it shall be for food.'", "Genesis 1:29", "NKJV"),
    new BibleVers("Better is a dinner of herbs where love is, than a fatted calf with hatred.", "Proverbs 15:17", "NKJV"),
    new BibleVers("Oh, taste and see that the LORD is good; blessed is the man who trusts in Him!", "Psalm 34:8", "NKJV"),
    new BibleVers("When you sit down to eat with a ruler, consider carefully what is before you; and put a knife to your throat if you are a man given to appetite.", "Proverbs 23:1-2", "NKJV"),
    new BibleVers("Also take for yourself wheat, barley, beans, lentils, millet, and spelt; put them into one vessel, and make bread of them for yourself.", "Ezekiel 4:9", "NKJV"),
    new BibleVers("Do not labor for the food which perishes, but for the food which endures to everlasting life, which the Son of Man will give you.", "John 6:27", "NKJV"),
    new BibleVers("The generous soul will be made rich, and he who waters will also be watered himself.", "Proverbs 11:25", "NKJV"),
    new BibleVers("Do not forget to entertain strangers, for by so doing some have unwittingly entertained angels.", "Hebrews 13:2", "NKJV"),
    new BibleVers("Give us this day our daily bread.", "Matthew 6:11", "NKJV"),
    new BibleVers("And whatever you do in word or deed, do all in the name of the Lord Jesus, giving thanks to God the Father through Him.", "Colossians 3:17", "NKJV"),
];
