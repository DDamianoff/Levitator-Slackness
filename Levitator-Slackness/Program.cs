using Levitator_Slackness;
using Microsoft.VisualBasic.FileIO;

const string defaultInFile = "./input.csv";
const string outFile = "./out.md";
List<Block> blocks = new();

if (!File.Exists(defaultInFile))
{
    Console.WriteLine("input.csv file must be provided");
    return;
}

#region Sanitizer

var file = File.ReadLines(defaultInFile).ToList();

if (file[0].Contains("BLItemNo"))
    file.Remove(file[0]);

if (file[^3].Contains(",,,,,,,,,"))
    3.Times(() => file.Remove(file[^1]));

File.WriteAllLines(defaultInFile,file);

#endregion

#region Serializer

using var parser = new TextFieldParser(defaultInFile);


parser.TextFieldType = FieldType.Delimited;
parser.SetDelimiters(",");

while (!parser.EndOfData)
{
    var fields = parser.ReadFields();
    blocks.Add(new Block(fields![3], fields[6], int.Parse(fields[8])));
}

#endregion

#region Processing

var finalResultString = (from block in blocks
    let formattedQuantity = block.Quantity.ToString().PadLeft(3, '0')
    select $"""
        - ### `[{ formattedQuantity}]` { block.PartName} : 
            - { block.ColorName} 
            - `{ block.Quantity}` <br/>
    
        """
    into result
    select string.Concat(result, Environment.NewLine)).ToList();

#endregion

#region Write

var inDiskFile = File.CreateText(outFile);

inDiskFile.WriteLine("# Pieces and stuff");
inDiskFile.WriteLine($"> **Total**: {blocks.Select(b => b.Quantity).Sum()}");
finalResultString.ForEach(_ => inDiskFile.Write(_));
inDiskFile.Close();

#endregion
