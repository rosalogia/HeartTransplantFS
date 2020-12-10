open HeartTransplant
open System

// Defining some functions to make grabbing input from stdin easier
let readNLines n = List.init n (fun _ -> Console.ReadLine())

let readNWith parser =
    Console.ReadLine()
    |> int
    |> readNLines
    |> List.map parser

let readNStdIn () = readNWith id

[<EntryPoint>]
let main _ =
    // Reading patient and survivability data from stdin using the functions
    // we defined earlier
    let patients =
        readNWith Patient.readPatient
        |> List.choose (function | Ok p -> Some p | Error e -> None)
    let sbas =
        readNWith Survivability.readSBA
        |> List.choose (function | Ok p -> Some p | Error e -> None)
    let sbcs =
        readNWith Survivability.readSBC
        |> List.choose (function | Ok p -> Some p | Error e -> None)
    
    // Pass all the read-in data to the heart matching function, which
    // also takes a number of available hearts, returning a list of
    // patients who will receive those hearts. Print the recipients.
    (patients, sbcs, sbas)
    |> Patient.matchHearts 10
    |> List.iter (printfn "%O")

    0