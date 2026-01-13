<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TpvLog
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
        Me.TextBoxExceptions = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxDsOrder = New System.Windows.Forms.TextBox()
        Me.TextBoxFchCreated = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxDsFch = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxDsAmt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxDsAuthorisation = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxConcept = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxTitular = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxExceptions
        '
        Me.TextBoxExceptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExceptions.Location = New System.Drawing.Point(8, 218)
        Me.TextBoxExceptions.Multiline = True
        Me.TextBoxExceptions.Name = "TextBoxExceptions"
        Me.TextBoxExceptions.Size = New System.Drawing.Size(372, 172)
        Me.TextBoxExceptions.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 202)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Incidencies:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 396)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(384, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(165, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(276, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Registre"
        '
        'TextBoxDsOrder
        '
        Me.TextBoxDsOrder.Location = New System.Drawing.Point(81, 13)
        Me.TextBoxDsOrder.Name = "TextBoxDsOrder"
        Me.TextBoxDsOrder.ReadOnly = True
        Me.TextBoxDsOrder.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxDsOrder.TabIndex = 54
        '
        'TextBoxFchCreated
        '
        Me.TextBoxFchCreated.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFchCreated.Location = New System.Drawing.Point(81, 39)
        Me.TextBoxFchCreated.Name = "TextBoxFchCreated"
        Me.TextBoxFchCreated.ReadOnly = True
        Me.TextBoxFchCreated.Size = New System.Drawing.Size(300, 20)
        Me.TextBoxFchCreated.TabIndex = 56
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Sol.licitut"
        '
        'TextBoxDsFch
        '
        Me.TextBoxDsFch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDsFch.Location = New System.Drawing.Point(81, 65)
        Me.TextBoxDsFch.Name = "TextBoxDsFch"
        Me.TextBoxDsFch.ReadOnly = True
        Me.TextBoxDsFch.Size = New System.Drawing.Size(300, 20)
        Me.TextBoxDsFch.TabIndex = 58
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Data operació"
        '
        'TextBoxDsAmt
        '
        Me.TextBoxDsAmt.Location = New System.Drawing.Point(81, 143)
        Me.TextBoxDsAmt.Name = "TextBoxDsAmt"
        Me.TextBoxDsAmt.ReadOnly = True
        Me.TextBoxDsAmt.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxDsAmt.TabIndex = 60
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Import"
        '
        'TextBoxDsAuthorisation
        '
        Me.TextBoxDsAuthorisation.Location = New System.Drawing.Point(81, 169)
        Me.TextBoxDsAuthorisation.Name = "TextBoxDsAuthorisation"
        Me.TextBoxDsAuthorisation.ReadOnly = True
        Me.TextBoxDsAuthorisation.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxDsAuthorisation.TabIndex = 62
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 172)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Autorització"
        '
        'TextBoxConcept
        '
        Me.TextBoxConcept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxConcept.Location = New System.Drawing.Point(81, 117)
        Me.TextBoxConcept.Name = "TextBoxConcept"
        Me.TextBoxConcept.ReadOnly = True
        Me.TextBoxConcept.Size = New System.Drawing.Size(300, 20)
        Me.TextBoxConcept.TabIndex = 64
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 120)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 63
        Me.Label7.Text = "Concepte"
        '
        'TextBoxTitular
        '
        Me.TextBoxTitular.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTitular.Location = New System.Drawing.Point(81, 91)
        Me.TextBoxTitular.Name = "TextBoxTitular"
        Me.TextBoxTitular.ReadOnly = True
        Me.TextBoxTitular.Size = New System.Drawing.Size(300, 20)
        Me.TextBoxTitular.TabIndex = 66
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 94)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 65
        Me.Label8.Text = "Titular"
        '
        'Frm_TpvLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 427)
        Me.Controls.Add(Me.TextBoxTitular)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxConcept)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxDsAuthorisation)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxDsAmt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxDsFch)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxFchCreated)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxDsOrder)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxExceptions)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_TpvLog"
        Me.Text = "Tpv Log"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxExceptions As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxDsOrder As TextBox
    Friend WithEvents TextBoxFchCreated As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxDsFch As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxDsAmt As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxDsAuthorisation As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxConcept As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxTitular As TextBox
    Friend WithEvents Label8 As Label
End Class
