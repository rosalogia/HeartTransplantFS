# Heart Transplant Matching in F#

The code in this repository completes an assignment in which data regarding patients in need
of a heart transplant, as well as some data about the survivability rates of different categories
of patients, is provided in a text file. The goal is to use the data to determine an order in which
patients should receive available hearts. The original assignment expects an object-oriented/imperative
approach in Java, but the code in this repository is functional and uses F#. There are significant
differences in how data is modelled and manipulated relative to the original assignment.

The structure of the project is as follows:

* Data modelling is done in [`Types.fs`](./src/Application/Types.fs)
* Functions to parse data from the input file are in [`Patients.fs`](./src/Application/Patient.fs) and [`Survivability.fs`](./src/Application/Survivability.fs)
* Functions to filter data from the patient list as well as to match hearts to patients are in [`Patients.fs`](./src/Application/Patient.fs)
* Execution code is in [`Program.fs`](./src/Application/Program.fs)

You can build and test the project if you have the .NET Core 3.1 SDK installed:

```
$ dotnet build
$ dotnet run --project src/Application < data.txt
```

The result should be 10 patients who were matched to 10 available hearts:

```
{ Id = 16
  Ethnicity = Caucasian
  Gender = Female
  Age = 5
  Cause = Viral
  StateOfHealth = Poor
  Urgency = Extreme }

...

{ Id = 12
  Ethnicity = Caucasian
  Gender = Male
  Age = 40
  Cause = ArteryDisease
  StateOfHealth = Excellent
  Urgency = Extreme }
```