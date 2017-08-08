Public Class Form1
    Dim tStuff As timTingz
    Dim throwTimer As Integer
    Dim helpTimer As Integer
    Dim livesLeft As Integer
    Dim blastLeft As Integer
    Dim direction As Integer
    Dim counter As Integer
    Dim total As Integer
    Dim isDancing As Boolean
    Dim notPress As Boolean
    Dim ladder(NUMLADDER) As PictureBox
    Dim fStuff(NUMFLOOR) As FloorType
    Dim ball(NUMBALL) As PictureBox
    Dim bStuff(NUMBALL) As timTingz
    Dim lives(NUMLIVES) As PictureBox
    Dim coins(NUMCOIN) As PictureBox
    Dim speedBlast(NUMBLAST) As PictureBox
    Const NUMCOIN As Integer = 7
    Const NUMBLAST As Integer = 4
    Const NUMLIVES As Integer = 2
    Const NUMLADDER As Integer = 6
    Const NUMFLOOR As Integer = 6
    Const NUMBALL As Integer = 3
    Const POWERPRICE As Integer = 20
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim index As Integer

        Call moveTim()
        Call animateTim()
        Call throwBall()
        Call animateRemy()
        Call flashHelp()
        Call clockTime()
        Call winTim()
        Call coinCollect()

        For index = 0 To NUMBLAST
            Call speedPower(index)
        Next index

        For index = 0 To NUMBALL
            If ball(index).Visible = True Then
                Call moveBall(index)
                Call animateBall(index)
                If isTouching(tim, ball(index)) = True Then
                    Call killTim()
                End If
            End If
        Next index

        If quit = True Then
            Me.Close()
        End If
    End Sub
    Private Sub coinCollect()
        Dim index As Integer
        Dim randX As Integer
        Dim randY As Integer
        Dim rnd As New Random()

        randX = rnd.Next(90, 540)
        randY = rnd.Next(215, 444)

        For index = 0 To NUMCOIN
            If isTouching(tim, coins(index)) = True Then
                coins(index).Visible = False
                cash.Text = cash.Text + 10
                If coins(index).Visible = False Then
                    coins(index).Visible = True
                    coins(index).Left = randX
                    coins(index).Top = randY
                End If
            End If
        Next

    End Sub
    Private Sub clockTime()
        If isTouching(tim, pingu) Then
            clock.Text = clock.Text
            finalTime = clock.Text
        Else
            If counter = 18 Then
                clock.Text = clock.Text + 1
                counter = 0
            Else
                counter = counter + 1
            End If
        End If

    End Sub
    Private Sub speedPower(ByVal pindex As Integer)
        If isTouching(tim, speedBlast(pindex)) = True Then
            If counter = 10 Then
                speedBlast(pindex).Visible = False
                tStuff.speed.X = 0
                counter = 0
            Else
                counter = counter + 1
                If tStuff.isFacingRight = True Then
                    If notPress = True Then
                        tStuff.speed.X = 0
                    Else
                        tStuff.speed.X = 30
                    End If

                Else
                    If notPress = True Then
                        tStuff.speed.X = 0
                    Else
                        tStuff.speed.X = -30
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub moveTim()
        tStuff.floorNum = getFloorNumber(tim.Top)

        If tStuff.isFloating = True Then
            Call jumping(tStuff)
        Else
            tStuff.onLadder = checkLadder()
            If tStuff.onLadder = False Then
                Call moveAlongFloor(tStuff, tim)
            Else
                tStuff.speed.X = 0
            End If
        End If

        If (tim.Left <= 75 And tStuff.speed.X < 0) Or (tim.Left >= 545 And tStuff.speed.X > 0) Then
            tStuff.speed.X = 0
            Call ladderSet()
        End If

        tim.Left = tim.Left + tStuff.speed.X
        tim.Top = tim.Top + tStuff.speed.Y
    End Sub
    Private Sub moveBall(ByVal bindex As Integer)
        bStuff(bindex).floorNum = getFloorNumber(ball(bindex).Top)

        If bStuff(bindex).isFloating = True Then
            Call jumping(bStuff(bindex))
        Else
            bStuff(bindex).onLadder = ballCheckLadder(bindex)
            If bStuff(bindex).onLadder = False Then
                If fStuff(bStuff(bindex).floorNum).slope < 0 Then
                    bStuff(bindex).speed.X = -10
                Else
                    bStuff(bindex).speed.X = 10
                End If
                Call moveAlongFloor(bStuff(bindex), ball(bindex))
            Else
                bStuff(bindex).speed.X = 0
                bStuff(bindex).speed.Y = 10
                bStuff(bindex).isFloating = True
            End If
        End If

        If (ball(bindex).Left < 77 And bStuff(bindex).speed.X < 0) Or (ball(bindex).Left > 540 And bStuff(bindex).speed.X > 0) Then
            bStuff(bindex).speed.X = 0
        End If

        ball(bindex).Left = ball(bindex).Left + bStuff(bindex).speed.X
        ball(bindex).Top = ball(bindex).Top + bStuff(bindex).speed.Y

        If bStuff(bindex).floorNum = 0 And ball(bindex).Left < 88 Then
            ball(bindex).Visible = False
            If isDancing = True Then
                isDancing = False
                throwTimer = 0
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobstanding.jpg")
            End If
        End If
    End Sub
    Private Sub throwBall()
        Dim index As Integer
        Dim index2 As Integer
        Dim doneThrowing As Boolean

        throwTimer = throwTimer + 1
        If throwTimer = 32 Then
            throwTimer = 0
            doneThrowing = False
            Do While doneThrowing = False
                If ball(index).Visible = False Then
                    doneThrowing = True
                    ball(index).Visible = True
                    ball(index).Top = 70
                    ball(index).Left = 203
                    bStuff(index).isFloating = False
                    bStuff(index).onLadder = False
                    bStuff(index).picNum = 1
                    bStuff(index).speed.X = 10
                    bStuff(index).speed.Y = 0
                    isDancing = True
                    For index2 = 0 To NUMBALL
                        If ball(index2).Visible = False Then
                            isDancing = False
                        End If
                    Next index2
                End If
                index = index + 1
                If index = 4 Then
                    doneThrowing = True
                End If
            Loop
        End If
    End Sub
    Private Function isTouching(ByVal thing1 As PictureBox, ByVal thing2 As PictureBox)
        If thing1.Right > thing2.Left And thing1.Left < thing2.Right Then
            If thing1.Bottom > thing2.Top And thing1.Top < thing2.Bottom Then
                Return True
            End If
        End If
        Return False
    End Function
    Private Function getFloorNumber(ByVal thing As Integer)
        If thing < 20 Then
            Return 6
        ElseIf thing < 73 Then
            Return 5
        ElseIf thing < 160 Then
            Return 4
        ElseIf thing < 240 Then
            Return 3
        ElseIf thing < 320 Then
            Return 2
        ElseIf thing < 393 Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Private Function checkLadder()
        Dim ladderOffset As Integer = 15
        Dim index As Integer

        For index = 0 To NUMLADDER
                If tStuff.speed.Y < 0 Then 'time going up adjust only left and right
                    If tim.Left > ladder(index).Left - ladderOffset And tim.Right < ladder(index).Right + ladderOffset Then
                        If tim.Top < ladder(index).Bottom And tim.Bottom > ladder(index).Top Then
                        Return True 'tim is touching the ladder
                    End If
                    End If
                ElseIf tStuff.speed.Y > 0 Then
                    ' tim Going Down adjust left right and tims bottom when on top
                    If tim.Left > ladder(index).Left - ladderOffset And tim.Right < ladder(index).Right + ladderOffset Then
                        If tim.Bottom < ladder(index).Bottom And tim.Bottom > ladder(index).Top - ladderOffset Then
                        Return True 'tim is touching the ladder
                    End If
                    End If
                ElseIf tStuff.speed.Y = 0 Then
                    ' tim Staying still adjust all
                    If tim.Left > ladder(index).Left - ladderOffset And tim.Right < ladder(index).Right + ladderOffset Then
                    If tim.Bottom < ladder(index).Bottom - ladderOffset And tim.Bottom > ladder(index).Top + ladderOffset Then
                        Return True 'tim is touching the ladder
                    End If
                End If
                End If
            Next index
            Return False
    End Function
    Private Function ballCheckLadder(ByVal bindex As Integer)
        Dim Index As Integer 'Counter variable used for For...Next loop 
        Dim ballLadderoffset As Integer

        ballLadderoffset = 13

        If Int(Rnd() * 5) = 1 Then
            For Index = 0 To NUMLADDER 'Checks to see if the ball is touching the ladder
                If ball(bindex).Left > ladder(Index).Left And ball(bindex).Right < ladder(Index).Right Then
                    If ball(bindex).Bottom + ballLadderoffset > ladder(Index).Top And ball(bindex).Bottom - ballLadderoffset < ladder(Index).Top Then
                        Return True
                    End If
                End If
            Next Index
        End If
        Return False
    End Function
    Private Sub moveAlongFloor(ByRef sprite As timTingz, ByRef thing As PictureBox)
        sprite.onFloor = True
        sprite.speed.Y = 0
        thing.Top = fStuff(sprite.floorNum).slope * (thing.Left - fStuff(sprite.floorNum).x) + fStuff(sprite.floorNum).y - thing.Height
        If thing.Left > fStuff(sprite.floorNum).rightEdge Then
            sprite.isFloating = True
            sprite.floatTime = 1
            sprite.speed.Y = 5
        ElseIf thing.Right < fStuff(sprite.floorNum).leftEdge Then
            sprite.isFloating = True
            sprite.floatTime = 1
            sprite.speed.Y = 5
        End If
    End Sub
    Private Sub jumping(ByRef sprite As timTingz)
        If sprite.floatTime = 5 Then 'if jumping
            If sprite.speed.Y = -5 Then
                sprite.speed.Y = 5
            Else
                sprite.speed.Y = 0
                sprite.isFloating = False
            End If
            sprite.floatTime = 0
            tStuff.doneJump = True
        Else
            tStuff.doneJump = False
            sprite.floatTime = sprite.floatTime + 1
        End If
    End Sub
    Private Sub animateBall(ByVal bindex As Integer)
        If bStuff(bindex).picNum = 1 Then
            ball(bindex).Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\ball1.jpg")
            bStuff(bindex).picNum = 2
        ElseIf bStuff(bindex).picNum = 2 Then
            ball(bindex).Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\ball2.jpg")
            bStuff(bindex).picNum = 3
        ElseIf bStuff(bindex).picNum = 3 Then
            ball(bindex).Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\ball3.jpg")
            bStuff(bindex).picNum = 4
        ElseIf bStuff(bindex).picNum = 4 Then
            ball(bindex).Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\ball4.jpg")
            bStuff(bindex).picNum = 1
        End If
    End Sub
    Private Sub animateRemy()
        If isDancing = False Then
            If throwTimer > 30 Then
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobrollingball.jpg")
            ElseIf throwTimer > 15
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobholdingball.jpg")
            ElseIf throwTimer > 7
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobgettingball.jpg")
            End If
        Else
            If throwTimer > 30 Then
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobsmiling.jpg")
            ElseIf throwTimer > 22 Then
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobdancing2.jpg")
            ElseIf throwTimer > 14 Then
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobdancing.jpg")
            ElseIf throwTimer > 8 Then
                remy.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\bobstanding.jpg")
            End If
        End If
    End Sub
    Private Sub animateTim()
        If tStuff.speed.Y <> 0 Then
            If tStuff.isFloating = False Then
                Call animateClimb()
            Else
                Call animateJump()
            End If
        Else
            If tStuff.speed.X > 0 Then
                Call animateRight()
            ElseIf tStuff.speed.X < 0 Then
                Call animatLeft()
            Else
                Call animateStand()
            End If
        End If
    End Sub
    Private Sub animateClimb()
        If tStuff.picNum = 1 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenclimb1.jpg")
            tStuff.picNum = 2
        ElseIf tStuff.picNum = 2 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenclimb2.jpg")
            tStuff.picNum = 3
        ElseIf tStuff.picNum = 3 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenclimb3.jpg")
            tStuff.picNum = 1
        End If
    End Sub
    Private Sub animateStand()
        If tStuff.isFacingRight = True Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenrightmove1.jpg")
        Else
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenleftmove1.jpg")
        End If
    End Sub
    Private Sub animateJump()
        If tStuff.speed.X > 0 And tStuff.isFacingRight = True Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\Nansenrightfloat.jpg")
        ElseIf tStuff.speed.X < 0 And tStuff.isFacingRight = False Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\Nansenleftfloat.jpg")
        End If
    End Sub
    Private Sub animateRight()
        If tStuff.picNum = 1 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenrightmove1.jpg")
            tStuff.picNum = 2
        ElseIf tStuff.picNum = 2 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenrightmove2.jpg")
            tStuff.picNum = 3
        ElseIf tStuff.picNum = 3 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenrightmove3.jpg")
            tStuff.picNum = 1
        End If
    End Sub
    Private Sub animatLeft()
        If tStuff.picNum = 1 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenleftmove1.jpg")
            tStuff.picNum = 2
        ElseIf tStuff.picNum = 2 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenleftmove2.jpg")
            tStuff.picNum = 3
        ElseIf tStuff.picNum = 3 Then
            tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenleftmove3.jpg")
            tStuff.picNum = 1
        End If
    End Sub
    Private Sub killTim()
        If livesLeft > 0 Then
            Timer1.Stop()
            lives(livesLeft).Visible = False
            livesLeft = livesLeft - 1
            deadForm.ShowDialog()
            Call reset()
            axe.Left = 580
            axe.Top = 545
            Timer1.Start()
        Else
            Timer1.Stop()
            livesLeft = 2
            gameOver.ShowDialog()
            Call reset()
            axe.Top = 545
            axe.Left = 580
            cash.Text = 0
            lives(0).Visible = True
            lives(1).Visible = True
            lives(2).Visible = True
            Timer1.Start()
        End If

    End Sub
    Private Sub winTim()
        If isTouching(tim, pingu) = True Then
            Timer1.Stop()
            level2.ShowDialog()
        End If
    End Sub
    Private Sub reset()
        Call floorSet()
        Call ballSet()
        Call ladderSet()
        Call timSet()
        Call coinSet()
        Call powerUpSet()
        isDancing = False
    End Sub
    Private Sub coinSet()
        coins(0) = coin0
        coins(1) = coin1
        coins(2) = coin2
        coins(3) = coin3
        coins(4) = coin4
        coins(5) = coin5
        coins(6) = coin6
        coins(7) = coin7

        coins(0).Top = 444
        coins(1).Top = 444
        coins(2).Top = 380
        coins(3).Top = 362
        coins(4).Top = 304
        coins(5).Top = 294
        coins(6).Top = 230
        coins(7).Top = 213

        coins(0).Left = 180
        coins(1).Left = 375
        coins(2).Left = 435
        coins(3).Left = 215
        coins(4).Left = 195
        coins(5).Left = 340
        coins(6).Left = 480
        coins(7).Left = 270
    End Sub
    Private Sub powerUpSet()
        Dim index As Integer

        speedBlast(0) = powerUp0
        speedBlast(1) = powerUp1
        speedBlast(2) = powerUp2
        speedBlast(3) = powerUp3
        speedBlast(4) = powerUp4

        For index = 0 To NUMBLAST
            speedBlast(index).Left = 270
            speedBlast(index).Top = 545
        Next index
    End Sub
    Private Sub ladderSet()
        ladder(0) = ladder0
        ladder(1) = ladder1
        ladder(2) = ladder2
        ladder(3) = ladder3
        ladder(4) = ladder4
        ladder(5) = ladder5
        ladder(6) = ladder6

        ladder(0).Top = 385
        ladder(1).Top = 400
        ladder(2).Top = 325
        ladder(3).Top = 245
        ladder(4).Top = 165
        ladder(5).Top = 90
        ladder(6).Top = 40

        ladder(0).Left = 270
        ladder(1).Left = 460
        ladder(2).Left = 130
        ladder(3).Left = 400
        ladder(4).Left = 175
        ladder(5).Left = 435
        ladder(6).Left = 270

    End Sub
    Private Sub timSet()
        tStuff.picNum = 1
        tStuff.isFacingRight = True
        tim.Image = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\pics\nansenrightmove1.jpg")
        tim.Left = 104
        tim.Top = 430
        tStuff.speed.X = 0
        tStuff.speed.Y = 0
        tStuff.onFloor = True
        tStuff.hasAxe = False

        lives(0) = live0
        lives(1) = live1
        lives(2) = live2
    End Sub
    Private Sub floorSet()
        fStuff(0).slope = -0.000000001
        fStuff(1).slope = 0.078
        fStuff(2).slope = -0.078
        fStuff(3).slope = 0.078
        fStuff(4).slope = -0.078
        fStuff(5).slope = 0
        fStuff(6).slope = 0

        fStuff(0).x = 137
        fStuff(1).x = 137
        fStuff(2).x = 137
        fStuff(3).x = 137
        fStuff(4).x = 137
        fStuff(5).x = 137
        fStuff(6).x = 137

        fStuff(0).y = 465
        fStuff(1).y = 377
        fStuff(2).y = 327
        fStuff(3).y = 226
        fStuff(4).y = 176
        fStuff(5).y = 92
        fStuff(6).y = 42

        fStuff(0).leftEdge = 0
        fStuff(1).leftEdge = 0
        fStuff(2).leftEdge = 133
        fStuff(3).leftEdge = 0
        fStuff(4).leftEdge = 133
        fStuff(5).leftEdge = 0
        fStuff(6).leftEdge = 200

        fStuff(0).rightEdge = 570
        fStuff(1).rightEdge = 507
        fStuff(2).rightEdge = 570
        fStuff(3).rightEdge = 507
        fStuff(4).rightEdge = 570
        fStuff(5).rightEdge = 507
        fStuff(6).rightEdge = 312
    End Sub
    Private Sub ballSet()
        Dim index As Integer

        ball(0) = ball0
        ball(1) = ball1
        ball(2) = ball2
        ball(3) = ball3

        For index = 0 To NUMBALL
            bStuff(index).isFloating = False
            bStuff(index).onLadder = False
            bStuff(index).speed.X = 10
            bStuff(index).speed.Y = 0
            bStuff(index).picNum = 1
            ball(index).Top = 70
            ball(index).Left = 203
            ball(index).Visible = False
        Next index
    End Sub
    Private Sub flashHelp()
        If helpSign.Visible = True And helpTimer > 3 Then
            helpSign.Visible = False
            helpTimer = 0
        ElseIf helpSign.Visible = False And helpTimer > 3 Then
            helpSign.Visible = True
            helpTimer = 0
        Else
            helpTimer = helpTimer + 1
        End If
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        direction = e.KeyCode

        If direction = Keys.Left Then
            notPress = False
            If tim.Left > 77 Then
                tStuff.speed.X = -7
            Else
                tStuff.speed.X = 0
            End If
            tStuff.isFacingRight = False
        End If
        If direction = Keys.Right Then
            notPress = False
            If tim.Left < 540 Then
                tStuff.speed.X = 7
            Else
                tStuff.speed.X = 0
            End If
            tStuff.isFacingRight = True
        End If
        If direction = Keys.Space And tStuff.speed.Y = 0 Then
            tStuff.speed.Y = -6
            tStuff.floatTime = -1
            tStuff.isFloating = True
            tStuff.onFloor = False
        End If
        If direction = Keys.Up And tStuff.isFloating = False Then
            tStuff.speed.Y = -5
            tStuff.isFloating = False
        End If
        If direction = Keys.Down And tStuff.isFloating = False Then
            tStuff.speed.Y = 5
        End If
    End Sub
    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If direction = Keys.Left Or direction = Keys.Right Then
            tStuff.speed.X = 0
            notPress = True
        End If
        If direction = Keys.Up Or direction = Keys.Down Then
            If tStuff.isFloating = False Then
                tStuff.speed.Y = 0
            End If
        End If
        If direction = Keys.Space And tStuff.doneJump = True Then
            tStuff.speed.X = 0
        End If
        If direction = Keys.ControlKey And tStuff.hasAxe = True Then
            tStuff.useAxe = False
            tStuff.speed.X = 0
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        My.Computer.Audio.Play(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Sounds\Hot_Dog_Song_5_times.wav", AudioPlayMode.BackgroundLoop)
        startMenu.ShowDialog()
        If start = True Then
            Timer1.Start()
            livesLeft = 2
            blastLeft = NUMBLAST
            powerLeft.Text = blastLeft + 1
            quit = False
            start = False
            Call reset()
        End If

    End Sub
    Private Sub buyPowerUp_Click(sender As Object, e As EventArgs) Handles buyPowerUp.Click
        If cash.Text >= POWERPRICE Then
            If blastLeft >= 0 Then
                cash.Text = cash.Text - POWERPRICE
                speedBlast(blastLeft).Left = tim.Left + 20
                speedBlast(blastLeft).Top = tim.Top + 5
                Call speedPower(blastLeft)
                blastLeft = blastLeft - 1
                powerLeft.Text = blastLeft + 1 'counts on label
            End If
        End If

    End Sub
End Class
