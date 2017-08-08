Module polloTingz
    Public quit As Boolean
    Public start As Boolean
    Public finalTime As Integer
    Structure timTingz
        Dim speed As Point
        Dim floatTime As Integer
        Dim picNum As Integer
        Dim isFacingRight As Boolean
        Dim isFloating As Boolean
        Dim onLadder As Boolean
        Dim floorNum As Integer
        Dim onFloor As Boolean
        Dim hasAxe As Boolean
        Dim useAxe As Boolean
        Dim doneJump As Boolean
    End Structure
    Structure FloorType
        Dim x As Single
        Dim y As Single
        Dim slope As Single
        Dim leftEdge As Integer
        Dim rightEdge As Integer
    End Structure
End Module
