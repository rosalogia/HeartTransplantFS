namespace HeartTransplant

module Survivability =
    // The active patterns and reading functions defined here are
    // similar to those in the Patient module
    let (|SurvivabilityParser|_|) (line: string) =
        match line.Split "," with
        | [| value ; years ; rate |]  ->
            Some (
                value,
                years,
                rate
            )
        | _ -> None
    
    let readSBA line =
        match line with
        | SurvivabilityParser (age, years, rate) ->
            Ok
                { Age = int age
                ; Years = int years
                ; Rate = double rate }
        | _ -> Error "Invalid survivability data"

    let readSBC line =
        match line with
        | SurvivabilityParser (cause, years, rate) ->
            match cause with
            | Patient.CauseValidator (c) ->
                Ok
                    { Cause = c
                    ; Years = int years
                    ; Rate = double rate }
            | _ -> Error "Invalid input for cause"
        | _ -> Error "Invalid survivability data"