<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Ex4
    Inherits System.Windows.Forms.Form

    'Disposição de substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTA: O seguinte procedimento é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.
    'Não o modifique usando o editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Enabled = True
        Me.tmrUpdate.Interval = 50
        '
        'Ex4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 430)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.Text = "DragonDrop"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmrUpdate As System.Windows.Forms.Timer

End Class
