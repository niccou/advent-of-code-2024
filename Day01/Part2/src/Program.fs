open System.IO
open HistorianHysteria

[<EntryPoint>]
let main argv =
    let input: Puzzle = File.ReadAllLines("input.txt") |> Array.toList

    let similarityScore = 
        input
        |> getSimilarityScore

    printfn "The similarity score is: %d" similarityScore

    0
    