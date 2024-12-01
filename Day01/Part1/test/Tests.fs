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

let firstList: UnsortedList = [3; 4; 2; 1; 3; 3]

let secondList: UnsortedList = [4; 3; 5; 3; 9; 3]

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
    
    Assert.Equal<UnsortedList>(expectedFirst, actualFirstList)
    Assert.Equal<UnsortedList>(expectedSecond, actualSecondList)
    
[<Fact>]
let ``Should sort ascending in list`` () =
    let expectedFirstSorted: SortedList = [1; 2; 3; 3; 3; 4]
    let expectedSecondSorted: SortedList = [3; 3; 3; 4; 5; 9]
    
    let (actualFirst, actualSecond) = sortAscending (firstList, secondList)
    
    Assert.Equal<SortedList>(expectedFirstSorted, actualFirst)
    Assert.Equal<SortedList>(expectedSecondSorted, actualSecond)

[<Fact>]
let ``Should pair the two sorted list`` () =
    let lists = (firstList, secondList) |> sortAscending
    let expected: PairList = [(1, 3); (2, 3); (3, 3); (3, 4); (3, 5); (4, 9)]

    let actual: PairList = pairLists lists

    Assert.Equal<PairList>(expected, actual)

[<Fact>]
let ``Should get the distance between for each pair in list`` () =
    let input: PairList = [(1, 3); (2, 3); (3, 3); (3, 4); (3, 5); (4, 9)]

    let expected: DistanceList = [2; 1; 0; 1; 2; 5]

    let actual: DistanceList = getDistances input

    Assert.Equal<DistanceList>(expected, actual)

[<Fact>]
let ``Should calculate the total distance`` () =
    let actual = calculateTotalDistance sampleData
    
    Assert.Equal(11, actual)
