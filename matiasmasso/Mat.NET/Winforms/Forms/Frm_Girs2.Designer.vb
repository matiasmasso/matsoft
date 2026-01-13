<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Girs2
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.Xl_Gir_SelEfts21 = New Winforms.Xl_Gir_SelEfts2()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_AmtSelected = New Winforms.Xl_Amt()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_AmtDisponible = New Winforms.Xl_Amt()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmtDespeses = New Winforms.Xl_Amt()
        Me.Xl_Gir_SelBancs21 = New Winforms.Xl_Gir_SelBancs2()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_PercentEuribor = New Winforms.Xl_Percent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ButtonRefresca = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 375)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(639, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(531, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 14
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'Xl_Gir_SelEfts21
        '
        Me.Xl_Gir_SelEfts21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Gir_SelEfts21.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Gir_SelEfts21.Name = "Xl_Gir_SelEfts21"
        Me.Xl_Gir_SelEfts21.Size = New System.Drawing.Size(635, 205)
        Me.Xl_Gir_SelEfts21.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(103, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Sel.leccionat"
        '
        'Xl_AmtSelected
        '
        Me.Xl_AmtSelected.Value = Nothing
        Me.Xl_AmtSelected.Location = New System.Drawing.Point(93, 20)
        Me.Xl_AmtSelected.Name = "Xl_AmtSelected"
        Me.Xl_AmtSelected.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtSelected.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Disponible"
        '
        'Xl_AmtDisponible
        '
        Me.Xl_AmtDisponible.Value = Nothing
        Me.Xl_AmtDisponible.Enabled = False
        Me.Xl_AmtDisponible.Location = New System.Drawing.Point(9, 20)
        Me.Xl_AmtDisponible.Name = "Xl_AmtDisponible"
        Me.Xl_AmtDisponible.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtDisponible.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(249, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Despeses"
        '
        'Xl_AmtDespeses
        '
        Me.Xl_AmtDespeses.Value = Nothing
        Me.Xl_AmtDespeses.Location = New System.Drawing.Point(231, 20)
        Me.Xl_AmtDespeses.Name = "Xl_AmtDespeses"
        Me.Xl_AmtDespeses.Size = New System.Drawing.Size(72, 20)
        Me.Xl_AmtDespeses.TabIndex = 9
        '
        'Xl_Gir_SelBancs21
        '
        Me.Xl_Gir_SelBancs21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Gir_SelBancs21.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Gir_SelBancs21.Name = "Xl_Gir_SelBancs21"
        Me.Xl_Gir_SelBancs21.Size = New System.Drawing.Size(635, 118)
        Me.Xl_Gir_SelBancs21.TabIndex = 0
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(531, 20)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker1.TabIndex = 43
        '
        'Xl_PercentEuribor
        '
        Me.Xl_PercentEuribor.BackColor = System.Drawing.Color.LemonChiffon
        Me.Xl_PercentEuribor.Location = New System.Drawing.Point(177, 20)
        Me.Xl_PercentEuribor.Name = "Xl_PercentEuribor"
        Me.Xl_PercentEuribor.Size = New System.Drawing.Size(48, 20)
        Me.Xl_PercentEuribor.TabIndex = 13
        Me.Xl_PercentEuribor.Text = "0,00 %"
        Me.Xl_PercentEuribor.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(185, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Euribor"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 46)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Gir_SelEfts21)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Gir_SelBancs21)
        Me.SplitContainer1.Size = New System.Drawing.Size(635, 327)
        Me.SplitContainer1.SplitterDistance = 205
        Me.SplitContainer1.TabIndex = 44
        '
        'ButtonRefresca
        '
        Me.ButtonRefresca.Location = New System.Drawing.Point(325, 16)
        Me.ButtonRefresca.Name = "ButtonRefresca"
        Me.ButtonRefresca.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRefresca.TabIndex = 45
        Me.ButtonRefresca.Text = "refresca"
        Me.ButtonRefresca.UseVisualStyleBackColor = True
        '
        'Frm_Girs2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 406)
        Me.Controls.Add(Me.ButtonRefresca)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_PercentEuribor)
        Me.Controls.Add(Me.Xl_AmtSelected)
        Me.Controls.Add(Me.Xl_AmtDespeses)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_AmtDisponible)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Girs2"
        Me.Text = "Remesa d'Anticips de Crèdit"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Xl_Gir_SelEfts21 As Winforms.Xl_Gir_SelEfts2
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtSelected As Winforms.Xl_Amt
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtDisponible As Winforms.Xl_Amt
    Friend WithEvents Xl_PercentEuribor As Winforms.Xl_Percent
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_Gir_SelBancs21 As Winforms.Xl_Gir_SelBancs2
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtDespeses As Winforms.Xl_Amt
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ButtonRefresca As System.Windows.Forms.Button
End Class
