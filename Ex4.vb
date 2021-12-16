Public Class Ex4
    ' SETUP DO GDI
    Public G As Graphics
    Private BBG As Graphics
    Private BB As Bitmap

    ' SETUP DO JOGO
    Private MobList As New List(Of Mob)
    Private rnd As New Random
    Private ToonFont As New Font("Tahoma", 15.0F)
    Private ToonStatus As String
    Private GameRunning As Boolean
    Private BoardText As String
    Private BackGround As Image = Image.FromFile("map.png")

    ' NÍVEIS DO JOGO
    Private SurvivalTime As Stopwatch
    Private MobSpawner As Stopwatch
    Private LevelTime As Stopwatch
    Private Level As Integer
    Private LevelText As String
    Private LevelFont As New Font("Tahoma", 10.0F)

    ' SETUP DO TOON
    Private Toon As Image = Image.FromFile("rad.png")
    Public ToonRect As Rectangle
    Public ToonHP As Integer
    Public Score As Integer

    ' CONTROLO DO TOON
    Private MoveToon As Boolean

    Private Sub Ex4_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' INICIALIZAÇÃO DOS GRÁFICOS
        G = Me.CreateGraphics
        BB = New Bitmap(Me.Width, Me.Height)
        ToonRect = New Rectangle(Me.Width / 3, Me.Height / 3, 50, 50)

        ' INICIALIZAÇÃO DOS NÍVEIS
        SurvivalTime = New Stopwatch
        MobSpawner = New Stopwatch
        LevelTime = New Stopwatch
        Level = 1
        BoardText = "Move-me para começar..."

        ' ESTATÍSTICAS DO TOON
        ToonHP = 10
        ToonStatus = "VIDA: " & ToonHP

        ' DRAW TODOS
        Draw()
    End Sub

    Private Sub Ex4_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        ' COMEÇAR O JOGO
        If GameRunning = False Then
            GameRunning = True
            LevelTime.Start()
            SurvivalTime.Start()
            MobSpawner.Start()
            BoardText = ""
        End If

        ' MOVER O TOON COM O RATO
        If ToonRect.Contains(e.X, e.Y) Then MoveToon = True Else MoveToon = False

        ' CRIAR MOBS COM O CLICK DO RATO
        CreateMob()
    End Sub

    Private Sub Ex4_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        ' DAR UPDATE DO TOON QUANDO MOVIMENTADO
        If MoveToon = True Then
            Dim NewPos As New Rectangle(e.X - (ToonRect.Width / 2), e.Y - (ToonRect.Height / 2), ToonRect.Width, ToonRect.Height)
            ToonRect = NewPos
        End If
    End Sub

    Private Sub Ex4_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        MoveToon = False
    End Sub

    Private Sub CreateMob()
        ' TESTAR O MOB
        Dim myMob As New Mob(rnd.Next(1 + Level, 4 + Level))
        MobList.Add(myMob)
    End Sub

    Private Sub Draw()
        ' DRAW DO BACKGROUND
        G.DrawImage(BackGround, -5, -5, Me.Width, Me.Height)

        ' DRAW DO TOON
        G.DrawImage(Toon, ToonRect)

        ' DRAW DO MOBS
        MobList.ForEach(Sub(mob)
                            mob.DrawMob()
                        End Sub)

        ' DRAW DO PLACAR DE PONTOS
        G.FillRectangle(Brushes.LightSteelBlue, New Rectangle(5, 5, 120, 100))
        G.DrawRectangle(Pens.Black, New Rectangle(5, 5, 120, 100))
        G.DrawString(ToonStatus, ToonFont, Brushes.DarkRed, 8, 10)
        G.DrawString(LevelText, LevelFont, Brushes.Navy, 10, 45)
        G.DrawString(BoardText, ToonFont, Brushes.DarkRed, 170, 20)

        ' DRAW DO BACKBUFFER DO ECRÃ
        G = Graphics.FromImage(BB)
        BBG = Me.CreateGraphics
        BBG.DrawImage(BB, 0, 0, Me.Width, Me.Height)
        G.Clear(Color.Wheat)
    End Sub

    Private Sub LevelUpdates()
        ' UPDATE DO TEXTO DO NÍVEL
        LevelText = "NÍVEL: " & Level & vbCrLf &
                    "TEMPO: " & SurvivalTime.Elapsed.Seconds & vbCrLf &
                    "PONTOS: " & Score

        ' SPAWN DE NOVOS MOBS A CADA 2 SEGUNDOS
        If Not MoveToon = True AndAlso MobSpawner.Elapsed.Seconds >= 2 Then
            CreateMob()
            MobSpawner.Restart()
        End If

        ' NÍVEL AVANÇADO PASSADO 10 SEGUNDOS
        If Not MoveToon = True AndAlso LevelTime.Elapsed.Seconds >= 10 Then
            Level += 1
            LevelTime.Restart()
        End If

        ' NÍVEL AVANÇADO SE O TOON NÃO SE MEXER
        If GameRunning = True AndAlso Not MoveToon = True Then Score += 5
    End Sub

    Private Sub tmrUpdate_Tick(sender As System.Object, e As System.EventArgs) Handles tmrUpdate.Tick
        ' PROCESSAR O UPDATE DOS NÍVEIS
        LevelUpdates()

        ' UPDATE DOS MOBS ANTES DE DRAW
        MobList.ForEach(Sub(mob) mob.Update())

        ' DRAW TODOS
        Draw()

        ' VERIFICAR COLISÕES
        MobList.ToList.ForEach(Sub(mob)
                                   If ToonRect.IntersectsWith(mob.MobBB) Then
                                       ' TOMAR HIT E RETIRAR O MOB
                                       ToonHP -= 1
                                       ToonStatus = "VIDA: " & ToonHP
                                       MobList.Remove(mob)

                                       ' PERDER VIDA
                                       If ToonHP <= 0 Then
                                           ' DRAW FINAL
                                           BoardText = "FOSTE COMIDO PELOS DRAGÕES"
                                           Draw()

                                           ' FIM DO JOGO
                                           MobSpawner.Stop()
                                           SurvivalTime.Stop()
                                           LevelTime.Stop()
                                           tmrUpdate.Enabled = False
                                       End If
                                   End If
                               End Sub)

    End Sub
End Class
