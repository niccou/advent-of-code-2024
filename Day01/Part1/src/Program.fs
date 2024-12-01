open System.IO
open HistorianHysteria

[<EntryPoint>]
let main argv =
    let input: Puzzle = File.ReadAllLines("input.txt") |> Array.toList

    let totalDistance = 
        input
        |> calculateTotalDistance

    printfn "The total distance is: %d" totalDistance

    0
    