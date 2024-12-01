module Tests

open HistorianHysteria
open Xunit

let sampleData: Puzzle = [
    "3   4";
    "4   3";
    "2   5";
    "1   3";
    "3   9";
    "3   3";
]

let firstList: LocationList = [3; 4; 2; 1; 3; 3]

let secondList: LocationList = [4; 3; 5; 3; 9; 3]

[<Theory>]
[<InlineData("3   4", 3, 4)>]
[<InlineData("4   3", 4, 3)>]
[<InlineData("2   5", 2, 5)>]
[<InlineData("1   3", 1, 3)>]
[<InlineData("3   9", 3, 9)>]
[<InlineData("3   3", 3, 3)>]
let ``Should parse the line`` (input: string) (expectedFirst: int) (expectedSecond: int) =
    
    let (first, second) = parseLine input
    
    Assert.Equal(expectedFirst, first)
    Assert.Equal(expectedSecond, second)

[<Fact>]
let ``Should parse all lines`` () =
    let expectedFirst = firstList
    let expectedSecond = secondList
    
    let (actualFirstList, actualSecondList) = parsePuzzle sampleData
    
    Assert.Equal<LocationList>(expectedFirst, actualFirstList)
    Assert.Equal<LocationList>(expectedSecond, actualSecondList)


[<Fact>]
let ``Should calculate similarity for each number`` () =
    let expected = [(3, 9); (4, 4); (2, 0); (1, 0); (3, 9); (3, 9)]

    let actual = calculateSimilarity (firstList, secondList)

    Assert.Equal<SimilarityScoreList>(expected, actual)


[<Fact>]
let ``Should sum the distances`` () =

    let actual: SimilarityScore = getSimilarityScore sampleData
    
    Assert.Equal(31, actual)
