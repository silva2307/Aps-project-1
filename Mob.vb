Public Class Mob
    ' IMAGEM
    Private MobGFX As Image
    Public MobRect As Rectangle
    Public MobBB As Rectangle ' Mob Bounding Box
    Private MobBBSize As Integer

    ' ESTATÍSTICAS DO MOB
    Private Speed As Integer

    Public Sub New(MoveSpeed As Integer)
        ' CRIAR UM NOVO MOB
        MobGFX = Image.FromFile("Dragon.png")
        Speed = MoveSpeed
        MobBBSize = 15

        MobRect = New Rectangle(CInt(Form1.Width / 2), CInt(Form1.Height / 2), MobGFX.Width, MobGFX.Height)
    End Sub

    Public Sub Update()
        Dim moveX As Integer = MobRect.X
        Dim moveY As Integer = MobRect.Y

        ' PERSEGUIR O TOON
        If Ex4.ToonRect.X > MobRect.X Then moveX += Speed
        If Ex4.ToonRect.X < MobRect.X Then moveX -= Speed
        If Ex4.ToonRect.Y > MobRect.Y Then moveY += Speed
        If Ex4.ToonRect.Y < MobRect.Y Then moveY -= Speed

        ' UPDATE NO MOB E NA BOUNDING BOX
        MobRect = New Rectangle(moveX, moveY, MobRect.Width, MobRect.Height)
        MobBB = New Rectangle(moveX + MobBBSize, moveY + MobBBSize, MobRect.Width - (MobBBSize * 2), MobRect.Height - (MobBBSize * 2))
    End Sub

    Public Sub DrawMob()
        Ex4.G.DrawImage(MobGFX, MobRect)
        'Ex4.G.DrawRectangle(Pens.Red, MobBB) ' DRAW BOUNDING BOX
    End Sub
End Class
