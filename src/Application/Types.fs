namespace HeartTransplant

type Cause =
    | Viral
    | Congenital
    | Accident
    | ArteryDisease
    | MuscleDisease

type Status =
    | Poor
    | Good
    | Excellent

type Urgency =
    | Extreme
    | Moderate

type Ethnicity =
    | AfricanAmerican
    | Caucasian
    | Hispanic

type Gender =
    | Female
    | Male

type SurvivabilityByAge =
    { Age: int
    ; Years: int
    ; Rate: double }

type SurvivabilityByCause =
    { Cause: Cause
    ; Years: int
    ; Rate: double }

type PatientData =
    { Id: int
    ; Ethnicity: Ethnicity
    ; Gender: Gender
    ; Age: int
    ; Cause: Cause
    ; StateOfHealth: Status
    ; Urgency: Urgency }