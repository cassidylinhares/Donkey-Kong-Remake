<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class deadForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.quitter = New System.Windows.Forms.Button()
        Me.tryAgain = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'quitter
        '
        Me.quitter.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.quitter.Location = New System.Drawing.Point(62, 436)
        Me.quitter.Name = "quitter"
        Me.quitter.Size = New System.Drawing.Size(124, 104)
        Me.quitter.TabIndex = 0
        Me.quitter.Text = "QUIT"
        Me.quitter.UseVisualStyleBackColor = True
        '
        'tryAgain
        '
        Me.tryAgain.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tryAgain.Location = New System.Drawing.Point(528, 436)
        Me.tryAgain.Name = "tryAgain"
        Me.tryAgain.Size = New System.Drawing.Size(123, 104)
        Me.tryAgain.TabIndex = 1
        Me.tryAgain.Text = "TRY AGAIN"
        Me.tryAgain.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.pollo.My.Resources.Resources.nansenDead
        Me.PictureBox1.Location = New System.Drawing.Point(291, 196)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(142, 153)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Cooper Std Black", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(249, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(225, 65)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "YOU DIED"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'deadForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.MidnightBlue
        Me.ClientSize = New System.Drawing.Size(767, 645)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.tryAgain)
        Me.Controls.Add(Me.quitter)
        Me.Name = "deadForm"
        Me.Text = "deadForm"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents quitter As Button
    Friend WithEvents tryAgain As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
End Class
