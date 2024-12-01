module HistorianHysteria

type PuzzleRow = string

type Puzzle = PuzzleRow list

type LocationId = int

type LocationList = LocationId list

type SimilarityScore = int

type SimilarityPair = LocationId * SimilarityScore

type SimilarityScoreList = SimilarityPair list

let parseLine (input: string): LocationId * LocationId =
    let parts = input.Split(' ') |> Array.filter (fun x -> x <> "")
    (int parts.[0], int parts.[1])

let calculateLocationSimilarity (second: LocationList) (id: LocationId): SimilarityPair =
    second
        |> List.map (fun x -> if x = id then 1 else 0)
        |> List.sum
        |> fun x -> (id, id * x)

let calculateSimilarity (lists: LocationList * LocationList): SimilarityScoreList =
    let (first, second) = lists
    first
        |> List.map (fun x -> calculateLocationSimilarity second x)

let parsePuzzle (puzzle: Puzzle): LocationList * LocationList =
    let rec parseAll (firstList: LocationList) (secondList: LocationList) (lines: Puzzle): LocationList * LocationList =
            match lines with
            | [] -> (firstList, secondList)
            | head :: tail ->
                let (first, second) = parseLine head
                parseAll (firstList @ [first]) (secondList @ [second]) tail
    parseAll [] [] puzzle

let getFinalSimilarityScore (scores: SimilarityScoreList): SimilarityScore =
    scores
        |> List.map snd
        |> List.sum 

let getSimilarityScore (puzzle: Puzzle): SimilarityScore =
    puzzle
        |> parsePuzzle 
        |> calculateSimilarity
        |> getFinalSimilarityScore
