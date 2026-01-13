<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_PgcPlan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxId = New System.Windows.Forms.TextBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxYearFrom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxYearTo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxLastPgcPlan = New System.Windows.Forms.ComboBox()
        Me.LabelLastPgc = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 407)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1069, 74)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(485, 10)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonCancel.TabIndex = 13
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(781, 10)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(277, 57)
        Me.ButtonOk.TabIndex = 12
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(16, 10)
        Me.ButtonDel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(101, 62)
        Me.Label1.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Id"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(291, 55)
        Me.TextBoxId.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.ReadOnly = True
        Me.TextBoxId.Size = New System.Drawing.Size(127, 38)
        Me.TextBoxId.TabIndex = 1
        Me.TextBoxId.TabStop = False
        Me.TextBoxId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(291, 117)
        Me.TextBoxNom.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(465, 38)
        Me.TextBoxNom.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(101, 124)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 32)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nom"
        '
        'TextBoxYearFrom
        '
        Me.TextBoxYearFrom.Location = New System.Drawing.Point(291, 244)
        Me.TextBoxYearFrom.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxYearFrom.Name = "TextBoxYearFrom"
        Me.TextBoxYearFrom.Size = New System.Drawing.Size(127, 38)
        Me.TextBoxYearFrom.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(101, 251)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(162, 32)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Desde l'any"
        '
        'TextBoxYearTo
        '
        Me.TextBoxYearTo.Location = New System.Drawing.Point(291, 306)
        Me.TextBoxYearTo.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxYearTo.Name = "TextBoxYearTo"
        Me.TextBoxYearTo.Size = New System.Drawing.Size(127, 38)
        Me.TextBoxYearTo.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(101, 313)
        Me.Label4.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(134, 32)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Fins l'any"
        '
        'ComboBoxLastPgcPlan
        '
        Me.ComboBoxLastPgcPlan.FormattingEnabled = True
        Me.ComboBoxLastPgcPlan.Location = New System.Drawing.Point(291, 179)
        Me.ComboBoxLastPgcPlan.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ComboBoxLastPgcPlan.Name = "ComboBoxLastPgcPlan"
        Me.ComboBoxLastPgcPlan.Size = New System.Drawing.Size(465, 39)
        Me.ComboBoxLastPgcPlan.TabIndex = 5
        '
        'LabelLastPgc
        '
        Me.LabelLastPgc.AutoSize = True
        Me.LabelLastPgc.Location = New System.Drawing.Point(101, 186)
        Me.LabelLastPgc.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.LabelLastPgc.Name = "LabelLastPgc"
        Me.LabelLastPgc.Size = New System.Drawing.Size(161, 32)
        Me.LabelLastPgc.TabIndex = 4
        Me.LabelLastPgc.Text = "Pla anterior"
        '
        'Frm_PgcPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1069, 481)
        Me.Controls.Add(Me.LabelLastPgc)
        Me.Controls.Add(Me.ComboBoxLastPgcPlan)
        Me.Controls.Add(Me.TextBoxYearTo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxYearFrom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_PgcPlan"
        Me.Text = "PLA DE COMPTABILITAT"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxId As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxYearFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxYearTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxLastPgcPlan As System.Windows.Forms.ComboBox
    Friend WithEvents LabelLastPgc As System.Windows.Forms.Label
End Class
