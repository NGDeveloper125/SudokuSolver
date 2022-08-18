# SudokuSolver
API sevice to solve 9x9 basic level Sudoku puzzles

Instructions :

Open the solution on visual studio;

Note there are 2 projects -
* SudokuSolverAPI
* SudokuSolver (UI)

Executing program -

Debug the SudokuSolverAPI first;

Then debug the SudokuSolver 

Fill the ui with the sudoku puzzle you want and press solve!


To Use Api Directly -

call  https://localhost:7200/api/Solution/

add all numbers row by row from top left corner as parameter (replace empty slots with 0)

example https://localhost:7200/api/Solution/008900410023000790710050080000040370380206041047090000070010059031000860096004100
