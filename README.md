# Advent of Code

These are solutions to the puzzles from [Advent of Code](https://adventofcode.com/).

## Running
The Program.cs has been setup to be simple. Setting the value of **day** will
solve that day's puzzle, provided that the inputs have been setup correctly
(see the [Inputs](#inputs) section for how to do that).

## Tests
The solution contains an NUnit test project which can run tests that verify
that when puzzles are solved they give the expected result. To change the
expected results, or add / remove tests, see the **GetTests** method in
**PuzzleTestRunner.cs**.

The tests are dependent on the inputs being setup correctly.

## Inputs
The inputs are not included as apparently they are not the same for each user,
and you aren't supposed to share them. To use your own inputs, setup a folder
structure like so:

- Advent
- AdventTests
- **Input**
  - ***day1.txt***
  - ***day1_sample.txt***

The text files should contain the inputs that correspond to the file names. Sometimes the
sample inputs are different between both puzzles for the day - in this case append
the _sample part of the filename with the puzzle's part number e.g. day1_sample**2**.txt
for the second part of day 1's sample.