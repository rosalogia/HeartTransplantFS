namespace HeartTransplant

module Patient =
    // Define some active patterns for easy data parsing
    let (|PatientParser|_|) (line: string) =
        match line.Split "," with
        | [| patientId ; ethnicityNo ; genderNo ; age ; causeNo ; stateOfHealthNo ; urgencyNo |]  ->
            Some (
                patientId,
                ethnicityNo,
                genderNo,
                age,
                causeNo,
                urgencyNo,
                stateOfHealthNo
            )
        | _ -> None

    let (|EthnicityValidator|_|) (ethnicityNo: string) =
        match ethnicityNo with
        | "10" -> Some AfricanAmerican
        | "11" -> Some Caucasian
        | "12" -> Some Hispanic
        | _ -> None

    let (|GenderValidator|_|) (genderNo: string) =
        match genderNo with
        | "13" -> Some Female
        | "14" -> Some Male
        | _ -> None

    let (|CauseValidator|_|) (causeNo: string) =
        match causeNo with
        | "0" -> Some Viral
        | "1" -> Some Congenital
        | "2" -> Some Accident
        | "3" -> Some ArteryDisease
        | "4" -> Some MuscleDisease
        | _ -> None

    let (|StateValidator|_|) (stateOfHealthNo: string) =
        match stateOfHealthNo with
        | "5" -> Some Poor
        | "6" -> Some Good
        | "7" -> Some Excellent
        | _ -> None

    let (|UrgencyValidator|_|) (urgencyNo: string) =
        match urgencyNo with
        | "8" -> Some Extreme
        | "9" -> Some Moderate
        | _ -> None
    
    // Produce a PatientData value from a line of input
    let readPatient (line: string) =
        match line with
        | PatientParser (patientId, ethnicityNo, genderNo, age, causeNo, stateOfHealthNo, urgencyNo) ->
            match (ethnicityNo, genderNo, causeNo, stateOfHealthNo, urgencyNo) with
            | (EthnicityValidator (ethnicity),
                GenderValidator (gender),
                CauseValidator (cause),
                StateValidator (stateOfHealth),
                UrgencyValidator (urgency)) ->
                Ok
                    {Id = int patientId
                    ; Ethnicity = ethnicity
                    ; Gender = gender
                    ; Age = int age
                    ; Cause = cause
                    ; StateOfHealth = stateOfHealth
                    ; Urgency = urgency}
            | _ -> Error "Invalid gender, cause, status, or urgency input"
        | _ -> Error "Invalid patient data: invalid number of inputs"
    
    // Functions pertaining to the assignment, various filters on patient data
    let withAgeAbove age    = List.filter (fun p -> p.Age > age)
    let withState state     = List.filter (fun p -> p.StateOfHealth = state)
    let withCause cause     = List.filter (fun p -> p.Cause = cause)
    
    // Heart matching function that takes a number of available hearts and
    // all the data about patients and survivability
    let matchHearts numHearts (patients, sbcs, sbas) =
        // If there are more hearts than patients, everyone gets a heart
        if numHearts > List.length patients then
            patients
        else
            // Create a Map<Cause, double> of survivability rates according
            // to survivability by cause 5 years after the transplant
            let relevantRates =
                sbcs
                |> List.filter (fun sbc -> sbc.Years = 5)
                |> List.sortBy (fun sbc -> sbc.Cause)
                |> List.map (fun sbc -> (sbc.Cause, sbc.Rate))
                |> Map.ofList
            
            // Sort patients according to their survivability by cause 5 years after the transplant,
            // then take the first numHearts patients in the resulting list
            patients
            |> List.sortBy (fun p -> relevantRates.[p.Cause])
            |> List.take numHearts