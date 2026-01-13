<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Wz_Girs
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageCsbs = New System.Windows.Forms.TabPage()
        Me.TabPageCsas = New System.Windows.Forms.TabPage()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelTot = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_AmountCur1 = New Winforms.Xl_AmountCur()
        Me.Xl_Gir_SelEfts1 = New Winforms.Xl_Gir_SelEfts2()
        Me.Xl_Gir_SelBancs1 = New Winforms.Xl_Gir_SelBancs2()
        Me.TabControl1.SuspendLayout()
        Me.TabPageCsbs.SuspendLayout()
        Me.TabPageCsas.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageCsbs)
        Me.TabControl1.Controls.Add(Me.TabPageCsas)
        Me.TabControl1.Location = New System.Drawing.Point(0, 49)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(820, 321)
        Me.TabControl1.TabIndex = 1
        '
        'TabPageCsbs
        '
        Me.TabPageCsbs.Controls.Add(Me.Xl_Gir_SelEfts1)
        Me.TabPageCsbs.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCsbs.Name = "TabPageCsbs"
        Me.TabPageCsbs.Size = New System.Drawing.Size(812, 295)
        Me.TabPageCsbs.TabIndex = 1
        Me.TabPageCsbs.Text = "Efectes"
        '
        'TabPageCsas
        '
        Me.TabPageCsas.Controls.Add(Me.Xl_Gir_SelBancs1)
        Me.TabPageCsas.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCsas.Name = "TabPageCsas"
        Me.TabPageCsas.Size = New System.Drawing.Size(812, 295)
        Me.TabPageCsas.TabIndex = 2
        Me.TabPageCsas.Text = "Remeses"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(724, 375)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 12
        Me.ButtonEnd.Text = "Fi >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Location = New System.Drawing.Point(620, 375)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 11
        Me.ButtonNext.Text = "Següent >"
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(0, 375)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 10
        Me.ButtonPrevious.Text = "< Enrera"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(161, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Import a girar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(406, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Import sel·leccionat: "
        '
        'LabelTot
        '
        Me.LabelTot.AutoSize = True
        Me.LabelTot.Location = New System.Drawing.Point(507, 15)
        Me.LabelTot.Name = "LabelTot"
        Me.LabelTot.Size = New System.Drawing.Size(0, 13)
        Me.LabelTot.TabIndex = 16
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(713, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(103, 20)
        Me.DateTimePicker1.TabIndex = 17
        '
        'Xl_AmountCur1
        '
        Me.Xl_AmountCur1.Amt = Nothing
        Me.Xl_AmountCur1.Location = New System.Drawing.Point(235, 11)
        Me.Xl_AmountCur1.Name = "Xl_AmountCur1"
        Me.Xl_AmountCur1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmountCur1.TabIndex = 13
        '
        'Xl_Gir_SelEfts1
        '
        Me.Xl_Gir_SelEfts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Gir_SelEfts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Gir_SelEfts1.Name = "Xl_Gir_SelEfts1"
        Me.Xl_Gir_SelEfts1.Size = New System.Drawing.Size(812, 295)
        Me.Xl_Gir_SelEfts1.TabIndex = 0
        '
        'Xl_Gir_SelBancs1
        '
        Me.Xl_Gir_SelBancs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Gir_SelBancs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Gir_SelBancs1.Name = "Xl_Gir_SelBancs1"
        Me.Xl_Gir_SelBancs1.Size = New System.Drawing.Size(812, 295)
        Me.Xl_Gir_SelBancs1.TabIndex = 0
        '
        'Wz_Girs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 401)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.LabelTot)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_AmountCur1)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Wz_Girs"
        Me.Text = "DESCOMPTE DE EFECTES"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageCsbs.ResumeLayout(False)
        Me.TabPageCsas.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageCsbs As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCsas As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Gir_SelEfts1 As Xl_Gir_SelEfts2
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents Xl_Gir_SelBancs1 As Xl_Gir_SelBancs2
    Friend WithEvents Xl_AmountCur1 As Xl_AmountCur
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelTot As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
