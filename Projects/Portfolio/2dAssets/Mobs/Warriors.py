

'''Creates instance of the game class and initialises dimenstions and position of the game '''
def main():
    size = GetSizeInput()
    boardPosition = GetBoardInput(size)
    warriorsGame = WarriorsGame(size, boardPosition)
    
'''Collects Input from the user and validates it, then returns the int'''
def GetSizeInput():
    while True:  
        try:
            size = int(input("Enter nxn board size: "))
            if size < 2:  
                raise ValueError("Please enter a valid number that is greater than 1.")
            return size  
        except ValueError as err:  
            print(err)

'''Collects Input from the user and validates it for the appropriate symbols of "W" and "." then returns the string'''
def GetBoardInput(size):
    expectedLength = size * size
    while True:  
        try:
            inputPosition = input("Please enter position: ")
            if len(inputPosition)!=expectedLength:  
                raise ValueError(f"Please enter a valid {size}x{size} grid")
            
            for i in range(0,size):
                if inputPosition[i]!="." and inputPosition[i]!="W":
                    raise ValueError("Please enter a valid symbols \".\" or \"W\" grid")
            
            return inputPosition  
        except ValueError as err:  
            print(err) 
            
'''Class contaning methods used to run the game, on initialisation requieres int dimentions and string for position '''
class WarriorsGame:
    def __init__(self, size, boardPosition):
        self.board = Board(size)
        self.board.SetUpPieces(boardPosition)
        self.board.Draw()
        MoveList = self.board.MoveAndAttack()
        self.board.AttackDiagonals(MoveList)
        self.board.DrawAttacks()
        self.board.DetectAttacks()
        
    

'''Board class is used by the WarriorsGame class, contains functions used to draw, set up game position and check moves/attacks '''
'''init args: size int '''
class Board:
    def __init__(self, size: int):
        self.squares = []
        self.Size = size
        
    '''Creates list of legal moves for each piece and returns it '''
    def MoveAndAttack(self):
        moveLocations = []
        
        for row in self.squares:
            for column in row:
                
                if column.Piece:
                    if (column.X+2 < self.Size):
                        moveLocations.append((column.X+2, column.Y))
                    if (column.X-2 >= 0):
                        moveLocations.append((column.X-2, column.Y))
                    if (column.Y+2 < self.Size):
                        moveLocations.append((column.X, column.Y+2))
                    if (column.Y-2 >= 0):
                        moveLocations.append((column.X, column.Y-2))
        return moveLocations
    
    '''creates list of attacked squares and returns it based on the inputed list of moves '''
    def AttackDiagonals(self, moveLocations):      
        for move in moveLocations:
            for row in self.squares:
                for column in row:
                    if(column.X==move[0]+1 and column.Y==move[1]+1):
                        column.Attacked = True
                    if(column.X==move[0]+1 and column.Y==move[1]-1):
                        column.Attacked = True
                    if(column.X==move[0]-1 and column.Y==move[1]+1):
                        column.Attacked = True
                    if(column.X==move[0]-1 and column.Y==move[1]-1):
                        column.Attacked = True
                        
    '''sets up position on the board given the position string '''
    def SetUpPieces(self, BoardString:str):
        print("Setting up pieces...")
        index = 0
        for i in range(0,self.Size):
            currentRow = []
            for j in range (0,self.Size):
                piece = False
                if(BoardString[index] =="W"):
                    piece = True
                square = Square(i,j, piece)
                currentRow.append(square)
                index+=1
            self.squares.append(currentRow)
        
    '''Draws board based on the 2x2 list contaning square objects '''
    def Draw(self):
        index = 0
        for i in range(0,self.Size):
            for j in range (0,self.Size):
                if(self.squares[i][j].Piece==False):
                    print(".", end=" ")
                else:
                    print("W", end=" ")     
            print()
    
    '''Displayes attacked squares with an X based on the Attacked conditional of square object instances '''
    def DrawAttacks(self):
        index = 0
        for i in range(0,self.Size):
            for j in range (0,self.Size):
                if(self.squares[i][j].Attacked==False):
                    print(".", end=" ")
                else:
                    print("X", end=" ")     
            print()    
    
    '''Checks and displayes msg if the square coontains a piece and is under attack '''
    def DetectAttacks(self):
        index = 0
        for i in range(0,self.Size):
            for j in range (0,self.Size):
                if(self.squares[i][j].Attacked==True) and (self.squares[i][j].Piece==True) :
                    print("Warriors attack each other")
   
    
    '''Used to store information on the location of pieces, moves and attacks '''
class Square:
    def __init__(self, positionX: int,positionY:  int, piece: bool):
        self.X = positionX
        self.Y = positionY
        self.Piece = piece
        self.Attacked = False
    def __str__(self):
        return f"{self.X} {self.Y} {self.Piece}"    
        
if __name__ == "__main__":
    main() 