module HistorianHysteria

type PuzzleRow = string

type Puzzle = PuzzleRow list

type LocationId = int

type UnsortedList = LocationId list

type SortedList = LocationId list

type Distance = int

type DistanceList = Distance list

type Pair = LocationId * LocationId

type PairList = Pair list

let parseLine (input: string): LocationId * LocationId =
    let parts = input.Split(' ') |> Array.filter (fun x -> x <> "")
    (int parts.[0], int parts.[1])

let parsePuzzle (puzzle: Puzzle): UnsortedList * UnsortedList =
    let rec parseAll (firstList: UnsortedList) (secondList: UnsortedList) (lines: Puzzle): UnsortedList * UnsortedList =
            match lines with
            | [] -> (firstList, secondList)
            | head :: tail ->
                let (first, second) = parseLine head
                parseAll (firstList @ [first]) (secondList @ [second]) tail
    parseAll [] [] puzzle

let sortAscending (lists: UnsortedList * UnsortedList): (SortedList * SortedList) =
    let (first, second) = lists
    (List.sort first, List.sort second)
    
let pairLists (lists: SortedList * SortedList): PairList =
    let (first, second) = lists
    List.zip first second

let getDistance (pair: Pair): Distance = 
    let (first, second) = pair
    abs (second - first)

let getDistances (pairs: PairList): DistanceList =
    List.map getDistance pairs

let getTotalDistance (distances: DistanceList): Distance =
    List.sum distances

let calculateTotalDistance (puzzle: Puzzle): Distance =
    puzzle
        |> parsePuzzle 
        |> sortAscending
        |> pairLists
        |> getDistances
        |> getTotalDistance
